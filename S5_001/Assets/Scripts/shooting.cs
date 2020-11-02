using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject bullet;
    public float speed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //attackpoint for rotation 
            GameObject instBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            Rigidbody instBulletRid = instBullet.GetComponent<Rigidbody>();
            instBulletRid.AddForce(Vector3.forward * speed);
          //  instBullet.GetComponent<Rigidbody>().velocity = 

        }
    }
}
