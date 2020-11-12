using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSignal : MonoBehaviour {


    // In this example we have a Particle System emitting aligned particles; we then emit and override the rotation every 2 seconds.

    private ParticleSystem system;

    public int speed;

    public Transform target;

    void Start()
    {
       
    }


    private void Update()



    {      var go = GameObject.Find("finalSignal");
 
            var shooterTransform = GameObject.Find("signal").transform;
        float step = speed * Time.deltaTime;

        Vector3 target = Vector3.RotateTowards(transform.forward, shooterTransform.position, step, 0.0f);
        go.transform.rotation = Quaternion.LookRotation(target);

  
    }

 
}