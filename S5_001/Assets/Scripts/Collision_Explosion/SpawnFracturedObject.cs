using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFracturedObject : MonoBehaviour
{
    public GameObject originalObject;
    public GameObject fracturedObject;

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
            GameObject fracObj = Instantiate(fracturedObject, originalObject.transform.position, Quaternion.identity) as GameObject;
        fracObj.GetComponent<Explosion>().Explode();
    }        }
}
