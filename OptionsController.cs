using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public Dropdown microphone;
	public Slider sensitivitySlider, thresholdSlider;


	// Use this for initialization
	void Start () {
		microphone.value = PlayerPrefsManager.GetMicrophone ();
		sensitivitySlider.value = PlayerPrefsManager.GetSensitivity ();
		thresholdSlider.value = PlayerPrefsManager.GetThreshold ();

        microphone.value = 0;
        sensitivitySlider.value = 70f;
        thresholdSlider.value = 0.004f;
    }

	public void SaveAndExit (){
		PlayerPrefsManager.SetMicrophone (microphone.value);
		PlayerPrefsManager.SetSensitivity (sensitivitySlider.value);
		PlayerPrefsManager.SetThreshold (thresholdSlider.value);

	}

	public void SetDefaults(){
		microphone.value = 0;
		sensitivitySlider.value = 70f;
		thresholdSlider.value = 0.0019f;
	}

}
