using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Rendering;

public class Laser_EndScene : MonoBehaviour, IPooledObject
{
    //public float dir = -1f;
    //public float range2 = 1f;
    public float speed = 10;
    public GameObject LaserBeamOrigin;
    //public GameObject satellite;
    public ParticleSystem particles;

    void Update()
    {

    }

    public void OnObjectSpawn()
    {
        //Quaternion dir = LaserBeamOrigin.transform.rotation;
        //Debug.Log("direction: " + dir);
        /*float euler_z = LaserBeamOrigin.transform.eulerAngles.z;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, euler_z);

        Vector3 direction = new Vector3(-1+transform.eulerAngles.z, 0, 0).normalized;
        transform.position += direction * speed * Time.deltaTime;*/
        //transform.rotation = dir;
        // Vector3 direction = Vector3.Scale(Vector3.one, LaserBeamOrigin.transform.position);
        
        Rigidbody instBulletRid = GetComponent<Rigidbody>();
        instBulletRid.AddForce(Vector3.up * speed);

        //transform.position += (speed * transform.forward).normalized;
        // transform.Translate(Vector3.forward * speed * Time.deltaTime);

        /*
        Vector3 direction = new Vector3(range2, 0,0);
        direction.Normalize();
        Vector3 newVelocity = speed * direction;
        transform.position += newVelocity * Time.deltaTime;*/


    }

    void OnCollisionEnter(Collision col)
    {
        /*if (col.gameObject.tag == "IgnoreLowOrbit")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), col.gameObject.GetComponent<Collider>(), true);
        }*/
    }


    void OnParticleCollision(GameObject other)
    {
        other = gameObject;
        Rigidbody rb = other.GetComponent<Rigidbody>();
        
        if (other != null) {
           
            if (rb)
            {
                rb.gameObject.GetComponent<Sparkle>().sparkles();
                rb.gameObject.SetActive(false); //remove laser beam
            }
        }
    }

}
