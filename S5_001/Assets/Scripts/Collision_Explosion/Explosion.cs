using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour
{
    public GameObject explosion;
    public Vector3 explosionOffset;

    public float minForce;
    public float maxForce;
    public float radius;

    public GameObject originalObject;
    public AudioSource soundfx;

    private Collider m_Collider;
    public float timeBeforeExpl;

    public Transform earth;


    public void Start()
    {
        soundfx = Instantiate(soundfx);
        m_Collider = GetComponent<Collider>();
        m_Collider.enabled = !m_Collider.enabled;
       // Debug.Log("Collider.enabled in start = " + m_Collider.enabled);
    }

    public void Explode()
    {
        if (explosion != null)
        {
            soundfx.Play();
            GameObject explosionFX = Instantiate(explosion, transform.position + explosionOffset, Quaternion.identity) as GameObject;
            

            Destroy(explosionFX, 5);
        }

        foreach (Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }
        }

    }

    void Update()
    {
        /*if (Input.GetMouseButton(0))
        {
            if (originalObject != null)
            {
                originalObject.GetComponent<Explosion>().Explode();
                originalObject.SetActive(false);

            }
        }*/
        //Debug.Log("time is " + Time.time);
        float dist = Vector3.Distance(earth.position, transform.position);
        //Debug.Log("Distance = " + dist);

        if (object_spawner.numOfSat >= 46 && dist >= 6)//Time.time >= timeBeforeExpl)
        {
            m_Collider.enabled = !m_Collider.enabled;
            //Debug.Log("Collider.enabled in update = " + m_Collider.enabled);
        }

        
    }

    void OnCollisionEnter(Collision col)
    {
        //collisions += 1;
        //Debug.Log("collsions" + collisions);
        if (col.gameObject.tag == "Finish" || col.gameObject.layer == 9) { //layer 9 is "SatLight"
            

                if (originalObject != null)
                {
                    originalObject.GetComponent<Explosion>().Explode();
                    originalObject.SetActive(false);
                    
                }
        }
    }
}
