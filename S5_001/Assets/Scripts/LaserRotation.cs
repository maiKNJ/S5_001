using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRotation : MonoBehaviour
{
    public float speed = 2f;
    public float maxRotation = 45f;

    void Update()
    {
        float rotationZ = maxRotation * Mathf.Sin(Time.time * speed);
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

}
