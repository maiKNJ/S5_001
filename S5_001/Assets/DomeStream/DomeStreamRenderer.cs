////////////////////////////////////////////////////////////////////////////////////////
//
// COPYRIGHT (C) Evans & Sutherland Computer Corporation
// All rights reserved.
//
////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

namespace ES
{
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Evans & Sutherland/Dome Stream Renderer")]
	public class DomeStreamRenderer : MonoBehaviour
	{
		internal DomeStreamManager manager;

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (manager != null)
				manager.SendVideoFrame(source);

			Graphics.Blit(source, destination);
		}
	}
}
