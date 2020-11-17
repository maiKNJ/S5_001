using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkle : MonoBehaviour
{
    public GameObject sparkleEffect;
    public float minForce;
    public float maxForce;
    public float radius;
    public AudioSource soundfx;


    
    public void sparkles()
    {
        if(sparkleEffect != null)
        {
            soundfx.Play();
            GameObject sparkleFX = Instantiate(sparkleEffect, transform.position, Quaternion.identity) as GameObject;
            Destroy(sparkleFX, 2);
        }

        foreach(Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        soundfx = Instantiate(soundfx);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
