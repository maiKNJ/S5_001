using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeChildCore : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject core;
    public Transform coreParent;
    public Vector3 rotation;
    public Transform earth;
    public Vector3 axis = new Vector3(0, 1, 0);

    private void OnEnable()
    {
        core.transform.SetParent(coreParent);
        core.transform.Rotate(rotation);
        coreParent.transform.RotateAround(earth.position, axis, Random.Range(3 * Time.deltaTime, 4 * Time.deltaTime));
    }
}
