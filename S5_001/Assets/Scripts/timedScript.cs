using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedScript : MonoBehaviour
{

    public GameObject spawnee;
//    public float spawnTime;

    //public float spawnDelay;
  //  int timerNoise;

    float nextTime;
    float modifier;

    public float timeRemaining = 5;
    public bool timerIsRunning = false;


    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;

        modifier = Random.Range(0, 3);

    }

    public void Update()
    {
        

  



        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
          
                timeRemaining -= Time.deltaTime;


            }
            else
            {

                Instantiate(spawnee, transform.position, transform.rotation);
                timeRemaining = 1 + modifier;
            }
        }


    }

    public void SpawnObject()
    {

        Instantiate(spawnee, transform.position, transform.rotation);
      
    }
}