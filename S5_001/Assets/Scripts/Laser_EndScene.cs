using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_EndScene : MonoBehaviour, IPooledObject
{
    public float range1 = -1f;
    public float range2 = 1f;
    public float speed = 1;
   
    public void OnObjectSpawn()
    {
        Vector3 direction = new Vector3(Random.Range(range1, range2), Random.Range(range1, range2), Random.Range(range1, range2)); // .normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
