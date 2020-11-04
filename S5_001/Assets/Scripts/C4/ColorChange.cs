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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        ParticleSystem.MainModule psmain = ps.main;
        if (time > timer)
        {
            /*particles = new ParticleSystem.Particle[ps.particleCount];
            ps.GetParticles(particles);*/
            

            psmain.startColor = Color.red;

        }
        if (time > timer+20)
        {
            psmain.startColor = Color.green;
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
