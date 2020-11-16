using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DeChild : MonoBehaviour
{
    //public TimelineAsset timeline;
    public GameObject child;
    //public GameObject child1;
    //public GameObject child2;
    //public GameObject child3;
    //public Vector3 childV;
    public Transform newparent;
    public Vector3 r;
    public Transform Center;
    public Vector3 Axis = new Vector3(0, 1, 0);



    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //childV = child.transform.position;
        //Vector3 child1V = transform.TransformPoint(childV);

        //Debug.Log(Time.frameCount);

        //childV = child.transform.position;
        if (Time.frameCount > 107)
        {
            
            //child.transform.parent = null;
            child.transform.SetParent(newparent);
            transform.Rotate(r);
            //child.transform.position = childV;
            newparent.transform.RotateAround(Center.position, Axis, Random.Range(3 * Time.deltaTime, 4 * Time.deltaTime));

            //child1.transform.parent = null;

            //child2.transform.parent = null;
            //child3.transform.parent = null;

            //Debug.Log("okay");
            
        }

    }
}
