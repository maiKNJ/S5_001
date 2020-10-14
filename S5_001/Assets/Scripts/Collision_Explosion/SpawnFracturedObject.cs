using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFracturedObject : MonoBehaviour
{
    
    public GameObject originalObject;
    public GameObject fracturedObject;

    //public GameObject fracObj;

    public float moveSpeed = 1000;

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetMouseButton(0))
        {
            SpawnFracturedObj();
        }
    }

    public void SpawnFracturedObj()
    {
        if (originalObject != null)
        {

        Destroy(originalObject);
            //  GameObject fracObj = Instantiate(fracturedObject, originalObject.transform.position, Quaternion.identity) as GameObject;

            var position = originalObject.transform.position += transform.forward * Time.deltaTime * moveSpeed;

            GameObject fracObj = Instantiate(fracturedObject, position, Quaternion.identity) as GameObject;


            // fracObj.transform.position == originalObject.transform.position;

            //transform.position += transform.forward * Time.deltaTime * moveSpeed;



            fracObj.GetComponent<Explosion>().Explode();
    }
    }
}
