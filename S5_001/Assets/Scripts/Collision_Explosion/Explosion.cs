using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosion;
    public Vector3 explosionOffset;

    public float minForce;
    public float maxForce;
    public float radius;

    public GameObject originalObject;


    public void Explode()
    {
        if (explosion != null)
        {
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
        if (Input.GetMouseButton(0))
        {
            if (originalObject != null)
            {
                originalObject.GetComponent<Explosion>().Explode();
                Destroy(originalObject);

            }
        }


    }
}
