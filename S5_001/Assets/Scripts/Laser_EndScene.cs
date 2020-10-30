﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_EndScene : MonoBehaviour, IPooledObject
{
    //public float dir = -1f;
    //public float range2 = 1f;
    public float speed = 1;
    public GameObject LaserBeamOrigin;
   
    public void OnObjectSpawn()
    {
        //Quaternion dir = LaserBeamOrigin.transform.rotation;
        //Debug.Log("direction: " + dir);
        /*float euler_z = LaserBeamOrigin.transform.eulerAngles.z;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, euler_z);

        Vector3 direction = new Vector3(-1+transform.eulerAngles.z, 0, 0).normalized;
        transform.position += direction * speed * Time.deltaTime;*/
        //transform.rotation = dir;

        Rigidbody instBulletRid = GetComponent<Rigidbody>();
        instBulletRid.AddForce(Vector3.left * speed);
        //transform.position += (speed * transform.forward).normalized;
        // transform.Translate(Vector3.forward * speed * Time.deltaTime);

        /*
        Vector3 direction = new Vector3(range2, 0,0);
        direction.Normalize();
        Vector3 newVelocity = speed * direction;
        transform.position += newVelocity * Time.deltaTime;*/
    }

}
