using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OptionsController : MonoBehaviour
{
	public Dropdown microphone;
	public GameObject settingsPanel;
	public GameObject openButton;

	private bool panelActive = false;

	// Use this for initialization
	void Start()
	{
		microphone.value = PlayerPrefsManager.GetMicrophone();
	}

	public void SaveAndExit()
	{
		PlayerPrefsManager.SetMicrophone(microphone.value);

		panelActive = !panelActive;
		settingsPanel.GetComponent<Animator>().SetBool("PanelActive", panelActive);
	}

	public void SetDefaults()
	{
		microphone.value = 0;
	}

	public void OpenSettings()
	{
		panelActive = !panelActive;
		settingsPanel.GetComponent<Animator>().SetBool("PanelActive", panelActive);
	}

	public void TogglePanel()
	{
		if (!panelActive)
		{
			OpenSettings();
		}
		else
		{
			SaveAndExit();
		}
	}
}
