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
    public AudioSource soundfx;

    private bool soundInput = true; //CHANGE THIS WHEN SHIFTING BETWEEN SOUND AND KEY INPUT
    bool fire = false;
    bool fireSecond = false;

    private double timer;
    private int objects = 0;
    private int objects1 = 0;
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

        if (soundInput == true)
        {
            //GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow);
            audioSource.GetSpectrumData(spectrum, 0, FFTWindow);
            realTimeSpectralFluxAnalyzer.analyzeSpectrum(spectrum, audioSource.time);

            if (SceneManager.GetActiveScene().name == "earth") //first game scene
            {
                if (realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux() > 0.02 && realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux() < 0.04)
                {
                    //Debug.Log("check " + realTimeSpectralFluxAnalyzer.calculateRectifiedSpectralFlux());
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
                    soundfx.Play();

                }
                if (timer < Time.time)
                {
                    poolManager.spawnFromPool("Satellite", transform.position, transform.rotation);
                    timer = Time.time + 2;
                }

            }

            if (SceneManager.GetActiveScene().name == "Rocket_Launch")
            {
                if (timer < Time.time && objects > 0)
                {
                    poolManager.spawnFromPool("Sat", transform.position, Quaternion.identity);
                    timer = Time.time + 0.7;
                }



            }

            if (SceneManager.GetActiveScene().name == "Cutscene3")
            {
                if (timer < Time.time)
                {
                    poolManager.spawnFromPool("Sat", transform.position, Quaternion.identity);
                    timer = Time.time + 0.7;
                    Debug.Log("shoot");
                }
            }

            if (SceneManager.GetActiveScene().name == "C4")
            {
                poolManager.spawnFromPool("lines", transform.position, Quaternion.identity);
            }
        }

        if (soundInput == false)
        {
            if (SceneManager.GetActiveScene().name == "earth")
            {
                if (fire == true)
                {
                    Debug.Log("inside fire");
                    poolManager.spawnFromPool("FirstOrbit", transform.position, Quaternion.identity);
                    fire = false;
                }

                if (fireSecond == true)
                {
                    poolManager.spawnFromPool("SecondOrbit", transform.position, Quaternion.identity);
                    fireSecond = false;
                }
            }


            if (SceneManager.GetActiveScene().name == "End_Scene")
            {
                if (fire == true)
                {
                    poolManager.spawnFromPool("Laser", LaserBeamOrigin.transform.position, LaserBeamOrigin.transform.rotation);
                    soundfx.Play();
                    fire = false;
                }

            }



                if (SceneManager.GetActiveScene().name == "Rocket_Launch")
            {
                if (timer < Time.time && objects > 0)
                {
                    poolManager.spawnFromPool("Sat", transform.position, Quaternion.identity);
                    timer = Time.time + 0.7;
                }



            }

            if (SceneManager.GetActiveScene().name == "Cutscene3")
            {
                if (timer < Time.time )
                {
                    poolManager.spawnFromPool("Sat", transform.position, Quaternion.identity);
                    timer = Time.time + 0.7;
                    Debug.Log("shoot2");
                }
            }

            if (SceneManager.GetActiveScene().name == "C4")
            {
                poolManager.spawnFromPool("lines", transform.position, Quaternion.identity);
            }
        }

    }

    private void Update()
    {
        
        if (soundInput == false)
        {
            if (SceneManager.GetActiveScene().name == "earth") //first game scene
            {
                if (Input.GetKeyDown(KeyCode.N))
                {
                    fire = true;
                }
                if (Input.GetKeyDown(KeyCode.M))
                {
                    fireSecond = true;
                }
            }

            if (SceneManager.GetActiveScene().name == "End_Scene") //last game scene
            {
                if (Input.anyKeyDown)
                {
                    fire = true;
                }
                if (timer < Time.time)
                {
                    poolManager.spawnFromPool("Satellite", transform.position, transform.rotation);
                    timer = Time.time + 2;
                }

            }
        }
        
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "Rocket_Launch"  )
        {
            //if (timer < Time.time)
            //{
                //poolManager.spawnFromPool("Sat", transform.position, Quaternion.identity);
            objects += 1;
                //timer = Time.time + 0.3;

            //}

        }

        if (SceneManager.GetActiveScene().name == "Cutscene3")
        {
            objects1 += 1;
        }
    }
}
