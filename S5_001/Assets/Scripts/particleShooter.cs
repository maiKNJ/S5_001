using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleShooter : MonoBehaviour
{

    public GameObject spawnee;
    public float spawnTime;
    public float spawnDelay;

    public GameObject signal;
    public float shootSpeed ;
    public Transform shooterTransform;
    public float speed;
    void Start()
    {

        InvokeRepeating("shootBullet", spawnTime, spawnDelay);
    }

    void Update()
    {


    }
    void shootBullet()
    {

        shooterTransform = GameObject.Find("Earth").transform;


        GameObject tempObj = Instantiate(signal, transform.position, transform.rotation) as GameObject;

      //  tempObj.transform.position = transform.position + shooterTransform.forward;

        Rigidbody projectile = tempObj.GetComponent<Rigidbody>();


       // float step = speed * Time.deltaTime;
        float step = speed; // calculate distance to move
        tempObj.transform.position = Vector3.MoveTowards(transform.position, shooterTransform.position, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, shooterTransform.position) < 0.001f)
        {
            // Swap the position of the cylinder.
            shooterTransform.position *= -1.0f;


        }
projectile.AddForce(transform.position * shootSpeed);

    } // 
}