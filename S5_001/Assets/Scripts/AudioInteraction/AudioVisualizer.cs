using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioVisualizer : MonoBehaviour
{
    public Transform audioObject;
    SpectralFluxAnalyzer realTimeSpectralFluxAnalyzer;
    SpectralFluxInfo info;
    //public float x;
    //public float y;
    //public float z;
    public FFTWindow FFTWindow;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //audioObject.transform.position = new Vector3(x, y, z);
        audioSource = GetComponent<AudioSource>();
        realTimeSpectralFluxAnalyzer = new SpectralFluxAnalyzer();
        info = new SpectralFluxInfo();

    }


    // Update is called once per frame
    void Update()
    {
        float[] spectrum = new float[1024];

        //GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow);
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow);
        //for (int i = 0; i < spectrum.Length; i++)
        //{
        float hertzPerBin = (float)AudioSettings.outputSampleRate / 2f / 1024;
        //Debug.Log("spectrum is" + hertzPerBin);

        realTimeSpectralFluxAnalyzer.analyzeSpectrum(spectrum, audioSource.time);
        //realTimeSpectralFluxAnalyzer.logSample(0);


        /*if (spectrum[0]> 0.0003)
            {
                float intensity = spectrum[0] * 250;
                float lerpY = Mathf.Lerp(audioObject.localScale.y, intensity, 1);
                Vector3 newScale = new Vector3(audioObject.localScale.x, lerpY, audioObject.localScale.z);
                audioObject.localScale = newScale;
            }*/

        if( realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux() > 0.5)
        {
            float intensity = spectrum[0] * 250;
            float lerpY = Mathf.Lerp(audioObject.localScale.y, intensity, 1);
            Vector3 newScale = new Vector3(audioObject.localScale.x, lerpY, audioObject.localScale.z);
            audioObject.localScale = newScale;
        }
        

        //}
            
             


    }
}
