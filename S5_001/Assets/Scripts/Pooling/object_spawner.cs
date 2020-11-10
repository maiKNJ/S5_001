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

    private double timer;
    private int objects = 0;
   // public Collider IgnoreCollision;
    //public Collider objectForCollision;
    void Start()
    {
        poolManager = PoolManager.Instance;
        audioSource = GetComponent<AudioSource>();
        realTimeSpectralFluxAnalyzer = new SpectralFluxAnalyzer();
        Scene scene = SceneManager.GetActiveScene();
        //   Physics.IgnoreCollision(IgnoreCollision, objectForCollision, true);

        timer = Time.time + 0.5;
    }


    void FixedUpdate()
    {
        float[] spectrum = new float[1024];


        //GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow);
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow);
        realTimeSpectralFluxAnalyzer.analyzeSpectrum(spectrum, audioSource.time);

        if (SceneManager.GetActiveScene().name == "earth") //first game scene
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
            
            if (Time.time <= 10 && realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux() > 0.04 && realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux() < 0.06)
            {
                poolManager.spawnFromPool("Laser", LaserBeamOrigin.transform.position, LaserBeamOrigin.transform.rotation);

            }
        }

        if (SceneManager.GetActiveScene().name == "Rocket_Launch")
        {
            if (timer < Time.time && objects> 0)
            {
                poolManager.spawnFromPool("Sat", transform.position, Quaternion.identity);
                timer = Time.time + 0.7;
            }



        }

        if (SceneManager.GetActiveScene().name == "C4")
        {
            poolManager.spawnFromPool("lines", transform.position, Quaternion.identity);
        }

    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "Rocket_Launch")
        {
            //if (timer < Time.time)
            //{
                poolManager.spawnFromPool("Sat", transform.position, Quaternion.identity);
            objects += 1;
                //timer = Time.time + 0.3;

            //}

        }
    }
}
