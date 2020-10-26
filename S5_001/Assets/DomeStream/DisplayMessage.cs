using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMessage : MonoBehaviour
{
	public string Message;// { get; set; }
	
	private GUIStyle MessageStyle { get; set; }

	private void Start()
	{
		MessageStyle = new GUIStyle
		{
			alignment = TextAnchor.MiddleCenter,
			stretchHeight = true,
			stretchWidth = true,
			fontStyle = FontStyle.Bold,
			fontSize = 42,
			wordWrap = true,
			normal = new GUIStyleState
			{
				textColor = Color.red
			}
		};
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(0, 0, Screen.width, Screen.height), Message, MessageStyle);
	}
}
