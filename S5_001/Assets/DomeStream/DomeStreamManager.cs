////////////////////////////////////////////////////////////////////////////////////////
//
// COPYRIGHT (C) Evans & Sutherland Computer Corporation
// All rights reserved.
//
////////////////////////////////////////////////////////////////////////////////////////

using System;
using UnityEngine;
using NewTek;
using NewTek.NDI;
using UnityEngine.Rendering;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.SceneManagement;

//#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
//using Microsoft.Win32;
//#endif

namespace ES
{
	[DisallowMultipleComponent]
	[AddComponentMenu("Evans & Sutherland/Dome Stream Manager")]
	public class DomeStreamManager : MonoBehaviour
	{
		private const string ConfigPath = @".\streamconfig.json";

		public enum StreamFPS : int
		{
			_30,
			_60
		};
		public enum YCbCrFormat
		{
			// STDV
			BT_601 = 0,
			// HDTV
			BT_709 = 1,
			// UHDTV
			BT_2020 = 2,
		};
		private class ColorConversionInfo : IDisposable
		{
			internal readonly ComputeShader shader;
			internal readonly int[] kernelIds;
			internal readonly int widthId;
			internal readonly int heightId;
			internal readonly int rgbDataId;
			internal readonly int ycbcrDataId;

			internal int kernel;
			internal ComputeBuffer outputBuffer;
			internal int frameWidth, frameHeight;
			internal int bufferSize, bufferStride;
			internal int blocksX, blocksY;
			internal const int threadsPerBlock = 32;

			internal ColorConversionInfo(string shaderResourceName)
			{
				shader = (ComputeShader)Resources.Load("RGBtoYCbCr");

				// can colorspace be changed at runtime? if so, we 
				// will need to load all of these kernels regardless
				if (QualitySettings.activeColorSpace == ColorSpace.Linear)
				{
					kernelIds = new int[3]
					{
						shader.FindKernel("Convert_BT601_linear"),
						shader.FindKernel("Convert_BT709_linear"),
						shader.FindKernel("Convert_BT2020_linear")
					};
				}
				else
				{
					kernelIds = new int[3]
					{
						shader.FindKernel("Convert_BT601"),
						shader.FindKernel("Convert_BT709"),
						shader.FindKernel("Convert_BT2020")
					};
				}

				widthId = Shader.PropertyToID("width");
				heightId = Shader.PropertyToID("height");
				rgbDataId = Shader.PropertyToID("RGBData");
				ycbcrDataId = Shader.PropertyToID("YCbCrData");
			}

			public void Dispose()
			{
				outputBuffer?.Dispose();
			}
		}

		// serialized (inspector) fields
		[SerializeField, Tooltip("The stream name that will be visible on the network.")]
		private string streamName = "unity";
		[SerializeField, Tooltip("The FPS to render the stream at.")]
		private StreamFPS streamFps = StreamFPS._30;
		[SerializeField, Tooltip("The color format to stream video frames at. BT.601: SDTV, BT.709: HDTV, BT.2020: UHDTV.")]
		private YCbCrFormat ycbcrFormat = YCbCrFormat.BT_2020;

		[SerializeField, Tooltip("The scene camera to stream the output of.")]
		private DomeStreamRenderer sourceRenderer;
		[SerializeField, Tooltip("The scene audio listener to stream the output of.")]
		private DomeStreamListener sourceListener;
		[SerializeField, Tooltip("Automatically try to find a source camera and audio listener when a scene is loaded.")]
		private bool autoFindSources = true;
		[SerializeField, Tooltip("Don't destroy this GameObject when the scene changes (You most likely want this on, so the stream persists between scenes).")]
		private bool dontDestroy = true;

		// private fields
		private IntPtr senderPtr = IntPtr.Zero;
		private ColorConversionInfo cc;
		private int audioSampleRate;

		#region External Interface

		public void SetSourceCamera(Camera target)
		{
			// Get the DomeStreamRenderer on the target camera (or create a new one)
			var newRenderer = target?.gameObject?.GetOrAddComponent<DomeStreamRenderer>();

			// Disable the current source renderer if needed
			if (sourceRenderer != null && sourceRenderer != newRenderer)
			{
				sourceRenderer.enabled = false;
				sourceRenderer.manager = null;
			}

			// set source renderer to the new target
			if (newRenderer != null)
			{
				sourceRenderer = newRenderer;
				sourceRenderer.manager = this;
			}
		}

		public void SetSourceListener(AudioListener target)
		{
			// Get the DomeStreamListener on the target listener (or create a new one)
			var newListener = target?.gameObject?.GetOrAddComponent<DomeStreamListener>();

			// Disable the current source listener if needed
			if (sourceListener != null && sourceListener != newListener)
			{
				sourceListener.enabled = false;
				sourceListener.manager = null;
			}

			// set source listener to the new target
			if (newListener != null)
			{
				sourceListener = newListener;
				sourceListener.manager = this;
			}
		}

		[ContextMenu("Find Sources")]
		public void FindSources()
		{
			// find a camera source if needed
			if (sourceRenderer == null)
			{
				if (Utilities.TryFindObjectOfType(out sourceRenderer))
				{
					// if the scene already has a 'DomeStreamRenderer' component active, use that
					sourceRenderer.manager = this;
				}
				else if (Camera.main != null)
				{
					// otherwise add a 'DomeStreamRenderer' component to the scene's main camera
					sourceRenderer = Camera.main.gameObject.AddComponent<DomeStreamRenderer>();
					sourceRenderer.manager = this;
				}
				else if (Utilities.TryFindObjectOfType<Camera>(out var found))
				{
					sourceRenderer = found.gameObject.AddComponent<DomeStreamRenderer>();
					sourceRenderer.manager = this;
				}
			}

			// find audiolistener source if needed
			if (sourceListener == null)
			{
				if (Utilities.TryFindObjectOfType(out sourceListener))
				{
					// if the scene already has a 'DomeStreamListener' component active, use that
					sourceListener.manager = this;
				}
				else if (Camera.main.gameObject.HasComponent<AudioListener>())
				{
					// if the scene's main camera has audio listener, use that
					sourceListener = Camera.main.gameObject.AddComponent<DomeStreamListener>();
					sourceListener.manager = this;
				}
				else if (Utilities.TryFindObjectOfType<AudioListener>(out var found))
				{
					// otherwise find the first audiolistener in the scene and use that
					sourceListener = found.gameObject.AddComponent<DomeStreamListener>();
					sourceListener.manager = this;
				}
			}
		}

		#endregion

		#region Rendering Handling

		private void InitializeColorConversion()
		{
			cc = new ColorConversionInfo("RGBtoYCbCr");
		}

		private void ShutdownColorConversion()
		{
			if (cc != null)
			{
				if (cc.outputBuffer != null)
					cc.outputBuffer.Release();
				cc = null;
			}
		}

		private void UpdateFrameDimensions(RenderTexture source)
		{
			// check if the source rendertexture has different 
			// dimensions than the current output buffer. if it 
			// does, then we need to reallocate the output.

			if (cc.frameWidth == source.width &&
				cc.frameHeight == source.height &&
				cc.outputBuffer != null &&
				cc.outputBuffer.IsValid())
				return;

			cc.frameWidth = source.width;
			cc.frameHeight = source.height;
			cc.blocksX = ((source.width / 2) + ColorConversionInfo.threadsPerBlock - 1) / ColorConversionInfo.threadsPerBlock;
			cc.blocksY = (source.height + ColorConversionInfo.threadsPerBlock - 1) / ColorConversionInfo.threadsPerBlock;
			cc.bufferStride = cc.blocksX * ColorConversionInfo.threadsPerBlock;
			cc.bufferSize = cc.bufferStride * cc.blocksY * ColorConversionInfo.threadsPerBlock;

			if (cc.outputBuffer == null ||
				!cc.outputBuffer.IsValid() ||
				cc.outputBuffer.count < cc.bufferSize)
			{
				if (cc.outputBuffer != null)
					cc.outputBuffer.Release();

				cc.outputBuffer = new ComputeBuffer(cc.bufferSize, sizeof(uint), ComputeBufferType.Default);
			}
		}

		private void ColorConvertFrame(RenderTexture source, Action<AsyncGPUReadbackRequest> onConverted)
		{
			// updates shader constants in case the 
			// source dimensions have changed
			UpdateFrameDimensions(source);

			cc.kernel = cc.kernelIds[(int)ycbcrFormat];
			cc.shader.SetInt(cc.widthId, cc.bufferStride);
			cc.shader.SetInt(cc.heightId, cc.frameHeight);
			cc.shader.SetTexture(cc.kernel, cc.rgbDataId, source);
			cc.shader.SetBuffer(cc.kernel, cc.ycbcrDataId, cc.outputBuffer);

			cc.shader.Dispatch(cc.kernel, cc.blocksX, cc.blocksY, 1);

			AsyncGPUReadback.Request(cc.outputBuffer, onConverted);
		}

		#endregion

		#region NDI Stream Handling

		private void VerifyNDISystemSettings()
		{
			#if !(UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
			// don't bother verifying anything for unsupported platforms
			return;
			#endif

			#region Check Multicast

			{
				try
				{
					var verifyProcess = new System.Diagnostics.Process();
					verifyProcess.StartInfo.FileName = System.IO.Path.Combine(Application.streamingAssetsPath, "checkndimulticast.bat");
					verifyProcess.StartInfo.CreateNoWindow = true;
					verifyProcess.StartInfo.UseShellExecute = false;
					verifyProcess.Start();
					verifyProcess.WaitForExit();

					var multicastEnabled = verifyProcess.ExitCode == 0;

					if (!multicastEnabled)
					{
						string errorMessage = @"This system is not correctly configured to use multicast with NDI. This will significantly degrade performance when trying to stream to a Digistar system! Please run the 'ndimulticast.reg' registry file to enable multicast.";

						// report that performance in Digistar may be 
						// greatly degraded due to lack of multicast
						Debug.LogError(errorMessage, gameObject);
						
						if (!Application.isEditor)
						{
							// on standalone builds, copy the .reg file next to the .exe so it's easy to find
							System.IO.File.Copy(System.IO.Path.Combine(Application.streamingAssetsPath, "ndimulticast.reg"), 
								System.IO.Path.Combine(Environment.CurrentDirectory, "ndimulticast.reg"));
						}

						// and show a message in-game explaining the performance degredation for 20 seconds
						var displayObject = new GameObject("NDI Multicast Error");
						DontDestroyOnLoad(displayObject);
						var displayMessage = displayObject.AddComponent<DisplayMessage>();
						displayMessage.Message = errorMessage;
						Destroy(displayObject, 20.0f);
					}
					else
					{
						Debug.Log("This system is configured to use multicast with NDI");
					}
				}
				catch
				{
					Debug.LogWarning("Unable to determine if this system has been configured to use multicast with NDI.");
				}
			}

			#endregion
		}

		private bool InitializeSender()
		{
			if (!NDIlib.initialize())
			{
				Debug.LogError("Failed to initialize NDI.");
				return false;
			}

			IntPtr nameUtf8 = UTF.StringToUtf8(streamName);
			var sendDesc = new NDIlib.send_create_t();
			sendDesc.p_ndi_name = nameUtf8;
			sendDesc.p_groups = IntPtr.Zero;
			sendDesc.clock_video = true;
			sendDesc.clock_audio = false;
			senderPtr = NDIlib.send_create(ref sendDesc);

			if (senderPtr == IntPtr.Zero)
			{
				Debug.LogError("Failed to create NDI sender");
				return false;
			}

			audioSampleRate = AudioSettings.outputSampleRate;

			return true;
		}

		private void ShutdownSender()
		{
			if (senderPtr != IntPtr.Zero)
			{
				NDIlib.send_destroy(senderPtr);
				senderPtr = IntPtr.Zero;
			}

			NDIlib.destroy();
		}

		unsafe internal void SendVideoFrame(RenderTexture source)
		{
			if (senderPtr == IntPtr.Zero || cc == null)
				return;

			ColorConvertFrame(source, (readback) =>
			{
				if (senderPtr == IntPtr.Zero || cc == null)
					return;

				var dataPtr = (IntPtr)readback.GetData<byte>().GetUnsafeReadOnlyPtr();
				var frame = new NDIlib.video_frame_v2_t();
				frame.FourCC = NDIlib.FourCC_type_e.FourCC_type_UYVY;
				frame.frame_format_type = NDIlib.frame_format_type_e.frame_format_type_progressive;
				frame.frame_rate_N = (streamFps == StreamFPS._60) ? 60 : 30;
				frame.frame_rate_D = 1;
				frame.line_stride_in_bytes = cc.bufferStride * sizeof(uint);
				frame.p_data = dataPtr;
				frame.picture_aspect_ratio = 0;
				frame.timecode = NDIlib.send_timecode_synthesize;
				frame.timestamp = 0;
				frame.xres = cc.frameWidth;
				frame.yres = cc.frameHeight;
				NDIlib.send_send_video_v2(senderPtr, ref frame);
			});
		}

		unsafe internal void SendAudioFrame(float[] data, int channels)
		{
			if (senderPtr == IntPtr.Zero)
				return;

			// pin audio data so unmanaged code can 
			// safely use it without worrying about GC
			fixed (float* dataPtr = data)
			{
				var frame = new NDIlib.audio_frame_interleaved_32f_t();
				frame.sample_rate = audioSampleRate;
				frame.no_channels = channels;
				frame.no_samples = data.Length / channels;
				frame.timecode = NDIlib.send_timecode_synthesize;
				frame.p_data = (IntPtr)dataPtr;
				NDIlib.util_send_send_audio_interleaved_32f(senderPtr, ref frame);
			}
		}

#endregion

#region MonoBehaviour Implementation

		private void Awake()
		{
			// check local system configuration
			VerifyNDISystemSettings();

			// try to initialize the NDI sender instance
			if (!InitializeSender())
			{
				// if it failed, disable this component. the most
				// likely reason for failure is that another NDI
				// source with the same name already exists on the
				// network.
				enabled = false;
				return;
			}

			// if configured to automatically find camera/listener
			// sources, look for them now
			if (autoFindSources)
				FindSources();
			if (sourceRenderer != null)
				sourceRenderer.manager = this;
			if (sourceListener != null)
				sourceListener.manager = this;

			// if this is configured to persist between scenes
			// set it as don't destroy on load, and register
			// a callback for when a new scene is loaded
			if (dontDestroy)
			{
				DontDestroyOnLoad(gameObject);
				SceneManager.sceneLoaded += OnSceneLoaded;
			}

			// initialize the resources used for the color 
			// conversion
			InitializeColorConversion();
		}

		private void OnDestroy()
		{
			// clear the source camera/listener
			SetSourceCamera(null);
			SetSourceListener(null);

			// cleanup resources from color conversion
			ShutdownColorConversion();

			// cleanup resources from NDI sender
			ShutdownSender();
		}

		private void Reset()
		{
			VerifyNDISystemSettings();
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			// each time a scene is loaded, if this is configured
			// to automatically find sources, do so
			if (autoFindSources)
				FindSources();
		}

#endregion
	}
}
