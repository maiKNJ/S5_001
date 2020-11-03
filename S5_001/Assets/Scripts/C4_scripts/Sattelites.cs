using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sattelites : MonoBehaviour
{
    public GameObject circles;
    public int number = 20;
    public Transform parent;
    public Transform Center;
    public Vector3 Axis = new Vector3(0, 1, 0);
    // Start is called before the first frame update
    void Start()
    {
        Vector3 center = transform.position;
        for (int i = 0; i < number; i++)
        {
            Vector3 pos = RandomCircle(center, 5.0f);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            Instantiate(circles, pos, rot);
            circles.transform.SetParent(parent);

        }
    }

    private void Update()
    {
        transform.RotateAround(Center.position, Axis, 80 * Time.deltaTime);

    }

    // Update is called once per frame
    Vector3 RandomCircle(Vector3 center, float radius)
        {
            float ang = Random.value * 360;
            Vector3 pos;
            pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = center.y;
            pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            return pos;
        }
    }
