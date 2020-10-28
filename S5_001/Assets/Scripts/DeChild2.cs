using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeChild2 : MonoBehaviour
{
    public List<GameObject> Boosters;
    public List<Transform> newparentBoosters;
    public GameObject Corestage;
    public Transform newparentCorestage;
    public List<GameObject> Core;
    public List<Transform> newparentCore;
    public Vector3 rotation;
    public Rigidbody Sputnik;
    public GameObject sput;

    public Transform Earth;
    public Vector3 Axis = new Vector3(0, 1, 0);


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount > 163)
        {
            Boosters[0].transform.SetParent(newparentBoosters[0]);
            Boosters[1].transform.SetParent(newparentBoosters[1]);
            Boosters[2].transform.SetParent(newparentBoosters[2]);
            Boosters[3].transform.SetParent(newparentBoosters[3]);
            for (int i = 0; i < 4; i++)
            {
                Boosters[i].transform.Rotate(rotation);
                newparentBoosters[i].transform.RotateAround(Earth.position, Axis, Random.Range(3 * Time.deltaTime, 4 * Time.deltaTime));

            }
        }

        if (Time.frameCount > 180)
        {
            Corestage.transform.SetParent(newparentCorestage);
            Corestage.transform.Rotate(rotation);
            newparentCorestage.transform.RotateAround(Earth.position, Axis, Random.Range(3 * Time.deltaTime, 4 * Time.deltaTime));

        }

        if (Time.frameCount > 200)
        {
            Core[0].transform.SetParent(newparentCore[0]);
            Core[1].transform.SetParent(newparentCore[1]);

            for (int i = 0; i < 2; i++)
            {
                Core[i].transform.Rotate(rotation);
                newparentCore[i].transform.RotateAround(Earth.position, Axis, Random.Range(3 * Time.deltaTime, 4 * Time.deltaTime));
            }
        }

        if (Time.frameCount > 500)
        {
            Sputnik.useGravity = true;
            sput.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }
}
