using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Object : MonoBehaviour, IPooledObject
{
    //public float upForce = 1f;
    //public float sideForce = .1f;

    public float range1 = -1f;
    public float range2 = 1f;
    public float speed = 1;
    public GameObject orbit;
    //public string tag;

    public Transform Center;
    public Vector3 Axis = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        /*float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);*/

        Vector3 direction = new Vector3(Random.Range(range1, range2), Random.Range(range1, range2), Random.Range(range1, range2)).normalized;
        //Vector3 direction = new Vector3(range1, range1, range1);
        //direction.Normalize();
        //Vector3 newVelocity = speed * direction;

        //float speed = GetComponent<Rigidbody>().velocity.magnitude;
        //transform.GetComponent<Rigidbody>().velocity = direction * speed;

        //transform.GetComponent<Rigidbody>().velocity = newVelocity;
        transform.position += direction * speed * (Time.deltaTime/4);
        if (SceneManager.GetActiveScene().name == "C4")
        {
            transform.RotateAround(Center.position, Axis, 80 * Time.deltaTime);
        }

        //MISSING SPEED CONTROL!

        
    }

    void OnCollisionEnter(Collision col)
    {
       
        if (col.gameObject == orbit) 
        {
            transform.RotateAround(Center.position, Axis, 80 * Time.deltaTime);
        }
    }
}
