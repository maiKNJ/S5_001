////////////////////////////////////////////////////////////////////////////////////////
//
// COPYRIGHT (C) Evans & Sutherland Computer Corporation
// All rights reserved.
//
////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ES
{
	[RequireComponent(typeof(AudioListener))]
	[AddComponentMenu("Evans & Sutherland/Dome Stream Listener")]
	public class DomeStreamListener : MonoBehaviour
	{
		internal DomeStreamManager manager;

		private void OnAudioFilterRead(float[] data, int channels)
		{
			if (manager != null)
				manager.SendAudioFrame(data, channels);
		}
	}
}
