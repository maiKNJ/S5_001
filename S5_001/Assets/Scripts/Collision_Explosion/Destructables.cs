using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{

    public GameObject destroyedVersion; //variable to choose the shattered version to replace the solid
    //private void OnMouseDown()
    //{
    //    Instantiate(destroyedVersion, transform.position, transform.rotation); //put in the shattered version chosen in same position and rotation as the solid
    //    Destroy(gameObject);    //destroy the solid object
    //}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "SolidSpaceship")
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation); //put in the shattered version chosen in same position and rotation as the solid
            Destroy(gameObject);    //destroy the solid object
        }
    }

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
}
  
