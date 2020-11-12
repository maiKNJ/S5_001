using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    float timer = 10f;
    float time;
    public GameObject earth;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject lines = GameObject.Find("LineObj");
        time += Time.deltaTime;
        ParticleSystem.MainModule psmain = ps.main;
        if (time > timer && time < timer+15)
        {
            /*particles = new ParticleSystem.Particle[ps.particleCount];
            ps.GetParticles(particles);*/
            

            psmain.startColor = Color.red;
            Destroy(earth.transform.GetChild(1).gameObject,0.008f);
            Destroy(earth.transform.GetChild(2).gameObject, 0.008f);
            Destroy(earth.transform.GetChild(3).gameObject, 0.008f);
            Debug.Log("red");

        }
        if (time > timer+20)
        {
            Debug.Log("green");
            psmain.startColor = Color.green;
            //Destroy(earth.transform.GetChild(1).gameObject, 5);
            //Destroy(earth.transform.GetChild(2).gameObject, 5);
            //Destroy(earth.transform.GetChild(3).gameObject, 5);
        }
        /*if (particles != null)
        {
            Debug.Log("some particles" + particles.Length);
            for (int i = 0; i < particles.Length; i++)
            {
                Color color = particles[i].startColor;
                color = Color.red;
                particles[i].startColor = color;
            }
        }*/
    }
}
