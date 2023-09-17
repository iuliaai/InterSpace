using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioVisualizer : MonoBehaviour {
		
	public Transform[] audioSpectrumObjects;
	[Range(1, 400)] public float heightMultiplier;
	[Range(64, 8192)] public int numberOfSamples = 128; //step by 2
	public FFTWindow fftWindow;
	public float lerpTime = 1;
	public Slider sensitivitySlider;

	/*
	 * The intensity of the frequencies found between 0 and 44100 will be
	 * grouped into 1024 elements. So each element will contain a range of about 43.06 Hz.
	 * The average human voice spans from about 60 hz to 9k Hz
	 * we need a way to assign a range to each object that gets animated. that would be the best way to control and modify animatoins.
	*/

	void Start(){

		heightMultiplier = PlayerPrefsManager.GetSensitivity ();

		sensitivitySlider.onValueChanged.AddListener(delegate {
			SensitivityValueChangedHandler(sensitivitySlider);
		});
	}

	void Update() {

		// initialize our float array
		float[] spectrum = new float[numberOfSamples];

		// populate array with fequency spectrum data
		GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, fftWindow);


		// loop over audioSpectrumObjects and modify according to fequency spectrum data
		// this loop matches the Array element to an object on a One-to-One basis.
		for(int i = 0; i < audioSpectrumObjects.Length; i++)
		{

			// apply height multiplier to intensity
			float intensity = spectrum[i] * heightMultiplier;

			// calculate object's scale
			float lerpY = Mathf.Lerp(audioSpectrumObjects[i].localScale.y,intensity,lerpTime);
            float lerpX = Mathf.Lerp(audioSpectrumObjects[i].localScale.x, intensity, lerpTime);
            float lerpZ = Mathf.Lerp(audioSpectrumObjects[i].localScale.z, intensity, lerpTime);

            Vector3 newScale = new Vector3( lerpX, lerpY, lerpZ);

			// appply new scale to object
			audioSpectrumObjects[i].localScale = newScale;

		}
	}

	public void SensitivityValueChangedHandler(Slider sensitivitySlider){
		heightMultiplier = sensitivitySlider.value;
	}

}
