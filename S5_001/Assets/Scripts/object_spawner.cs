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
    public Transform parent;

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
            Vector3 center = transform.position;
            Vector3 pos = RandomCircle(center, 5.0f);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);

            poolManager.spawnFromPool("red", pos, rot);

           
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

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}
//}
