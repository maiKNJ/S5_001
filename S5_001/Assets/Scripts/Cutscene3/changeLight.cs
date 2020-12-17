using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeLight : MonoBehaviour
{ 
    // Interpolate light color between two colors back and forth
    float duration = 10.0f;
    Color color0 = Color.green;
    Color color1 = Color.red;

    Light lt;

    void Start()
    {
        lt = GetComponent<Light>();
    }

    void Update()
    {
        // set light color
     
        lt.color = color1;
    }
}