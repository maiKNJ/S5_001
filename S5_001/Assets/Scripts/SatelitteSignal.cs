using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SatelitteSignal : MonoBehaviour
{
    public float spawnTime;
    public float spawnDelay;
    public float speed = 1;
    public float time = 10;

    //public GameObject signal;
   //ect target;
   // public GameObject signalSender;



    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    // Update is called once per frame
    public void SpawnObject()
    {


        // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - transform.position;

         float singleStep = speed * Time.deltaTime;
        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red, time);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);


        // GameObject newSignal = Instantiate(signal, signalSender.transform.position, signalSender.transform.rotation);
        //Rigidbody newSignalRid = newSignal.GetComponent<Rigidbody>();
        //newSignalRid.AddForce(newDirection* speed) ;

        // newSignalRid.velocity = (GameObject.Find("Earth").transform.position) * speed * Time.deltaTime;
        // newSignalRid.AddForce(Vector3.MoveTowards(signalSender, target, speed);
        //newSignalRid.AddForce((GameObject.Find("Earth").transform.position.forward)*speed);

        //Rigidbody newSignalRid = newSignal.GetComponent<Rigidbody>().velocity == (GameObject.Find("target").transform.position) * speed;
    }

}
    