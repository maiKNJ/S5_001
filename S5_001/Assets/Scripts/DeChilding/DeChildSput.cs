using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeChildSput : MonoBehaviour
{
    public Rigidbody Sputnik;
    public GameObject sput;
    private void OnEnable()
    {
        Sputnik.useGravity = true;
        sput.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }
}
