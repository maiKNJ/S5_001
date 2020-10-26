using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

namespace ES.Editor
{
	[InitializeOnLoad]
	public class VerifyConfig
	{
		static VerifyConfig()
		{
			SetNeededConfiguration();
		}

		[MenuItem(@"Evans & Sutherland/Configure Player Settings")]
		private static void SetNeededConfiguration()
		{
			Debug.Log("Verifying player settings");

			if (!PlayerSettings.allowUnsafeCode)
			{
				Debug.Log("Allowing 'Unsafe' code in C# scripts. This is needed by the E&S Dome Stream plugin.");
				PlayerSettings.allowUnsafeCode = true;
			}

			var win32Graphics = PlayerSettings.GetGraphicsAPIs(BuildTarget.StandaloneWindows);
			if (win32Graphics.Length != 1 || win32Graphics[0] != UnityEngine.Rendering.GraphicsDeviceType.Direct3D11)
			{
				Debug.Log("Setting win32 graphics target to D3D11");
				PlayerSettings.SetGraphicsAPIs(BuildTarget.StandaloneWindows, new[] { UnityEngine.Rendering.GraphicsDeviceType.Direct3D11 });
			}

			var win64Graphics = PlayerSettings.GetGraphicsAPIs(BuildTarget.StandaloneWindows64);
			if (win64Graphics.Length != 1 || win64Graphics[0] != UnityEngine.Rendering.GraphicsDeviceType.Direct3D11)
			{
				Debug.Log("Setting win64 graphics target to D3D11");
				PlayerSettings.SetGraphicsAPIs(BuildTarget.StandaloneLinux64, new[] { UnityEngine.Rendering.GraphicsDeviceType.Direct3D11 });
			}
		}

		[PostProcessBuild(1)]
		private static void CopyNativeDLL(BuildTarget target, string pathToBuiltProject)
		{
			// only support Windows 64 bit builds
			if (target != BuildTarget.StandaloneWindows64)
			{
				Debug.LogWarning("Project was not built to target Windows x64. The dome stream will not function in this build");
				return;
			}

			{
				// copy all native dlls to the managed directory in the built application data
				// to workaround a bug in Unity that prevents the native dlls from being loaded.
				// see: https://forum.unity.com/threads/dll-not-found-with-standalone-app-but-works-fine-in-editor.389392/
				var buildDir = new DirectoryInfo(Path.GetDirectoryName(pathToBuiltProject));
				var nativeDLLDir = new DirectoryInfo(Path.Combine(buildDir.FullName, $"{buildDir.Name}_Data", "Plugins"));
				var managedDLLDir = new DirectoryInfo(Path.Combine(buildDir.FullName, $"{buildDir.Name}_Data", "Managed"));
				foreach (var nativeDLL in nativeDLLDir.GetFiles("*.dll"))
				{
					Debug.Log($"Copying native dll {nativeDLL.Name} to managed directory");
					File.Copy(nativeDLL.FullName, Path.Combine(managedDLLDir.FullName, $"{nativeDLL.Name}.{nativeDLL.Extension}"));
				}
			}
		}
	}
}
