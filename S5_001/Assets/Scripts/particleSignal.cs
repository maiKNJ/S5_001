using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSignal : MonoBehaviour {


    // In this example we have a Particle System emitting aligned particles; we then emit and override the rotation every 2 seconds.

    private ParticleSystem system;

    public int speed;

    public Transform target;


    float nextTime;
    float modifier;



    
    public float timeRemaining = 3;
    public bool timerIsRunning = false;



    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    void Update()
    {

        var go = GameObject.Find("finalSignal");
        if (timerIsRunning) {
            if (timeRemaining > 0){
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                var shooterTransform = GameObject.Find("signal").transform;
                float step = speed * Time.deltaTime;

                Vector3 target = Vector3.RotateTowards(transform.forward, shooterTransform.position, step, 0.0f);
                go.transform.rotation = Quaternion.LookRotation(target);

                Debug.Log("Time has run out!");
               // finalSignal.startLifetime = 2;

                timeRemaining = 3;
               
            }
        }
    }
}
/*
void Start()
    {
     
    }


    private void Update()
    {
   nextTime = 0;
        modifier = Random.Range(1, 10);


        nextTime = Time.time + modifier;
        if (Time.time > nextTime)
        {
          

            var go = GameObject.Find("finalSignal");

            var shooterTransform = GameObject.Find("signal").transform;
            float step = speed * Time.deltaTime;

            Vector3 target = Vector3.RotateTowards(transform.forward, shooterTransform.position, step, 0.0f);
            go.transform.rotation = Quaternion.LookRotation(target);
        }
  
    }

 
}
*/
