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


    // Use this for initialization
    void Start()

    {
        nextTime = 0;
       
    }

    public void Update()
    {


        modifier = Random.Range(1, 10);


        nextTime = Time.time + modifier;
        if(Time.time > nextTime)
        {
            Instantiate(spawnee, transform.position, transform.rotation);
        }
       

        
    }

    public void SpawnObject()
    {

        Instantiate(spawnee, transform.position, transform.rotation);
      
    }
}