using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class object_spawner : MonoBehaviour
{
    PoolManager poolManager;
    SpectralFluxAnalyzer realTimeSpectralFluxAnalyzer;
    AudioSource audioSource;
    public FFTWindow FFTWindow;
    public GameObject LaserBeamOrigin;

   // public Collider IgnoreCollision;
    //public Collider objectForCollision;
    void Start()
    {
        poolManager = PoolManager.Instance;
        audioSource = GetComponent<AudioSource>();
        realTimeSpectralFluxAnalyzer = new SpectralFluxAnalyzer();
        Scene scene = SceneManager.GetActiveScene();
        //   Physics.IgnoreCollision(IgnoreCollision, objectForCollision, true);
    }

    void FixedUpdate()
    {
        float[] spectrum = new float[1024];


        //GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow);
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow);
        realTimeSpectralFluxAnalyzer.analyzeSpectrum(spectrum, audioSource.time);

        if (SceneManager.GetActiveScene().name == "Earth") //first game scene
        {
            if (realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux() > 0.02 && realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux() < 0.04)
            {
                Debug.Log("check " + realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux());
                poolManager.spawnFromPool("FirstOrbit", transform.position, Quaternion.identity);
            }
            if (realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux() > 0.04 && realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux() < 0.06)
            {
                poolManager.spawnFromPool("SecondOrbit", transform.position, Quaternion.identity);
            }
        }

        if (SceneManager.GetActiveScene().name == "End_Scene") //last game scene
        {
            if (realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux() > 0.04 && realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux() < 0.06)
            {
                poolManager.spawnFromPool("Laser", LaserBeamOrigin.transform.position, Quaternion.identity);
            }
        }
    }
}
