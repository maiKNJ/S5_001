using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeChildNose : MonoBehaviour
{
    // Start is called before the first frame update
    //use two elements for the parent

    public List<GameObject> Nose;
    public List<Transform> NoseParent;
    public Vector3 rotation;
    public Transform earth;
    public Vector3 Axis = new Vector3(0, 1, 0);


    private void OnEnable()
    {
        Nose[0].transform.SetParent(NoseParent[0]);
        Nose[1].transform.SetParent(NoseParent[1]);

        for (int i = 0; i < 2; i++)
        {
            Nose[i].transform.Rotate(rotation);
            NoseParent[i].transform.RotateAround(earth.position, Axis, Random.Range(3 * Time.deltaTime, 4 * Time.deltaTime));
        }
        
    }
}
