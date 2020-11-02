using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeChildBoost : MonoBehaviour
{
    public List<GameObject> Boosters;
    public List<Transform> newparentBoosters;
    public Vector3 rotation;
    public Transform Earth;
    public Vector3 Axis = new Vector3(0, 1, 0);

    private void OnEnable()
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
}
