using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndText : MonoBehaviour
{

    public Animator transition;
    public float timeBeforeEndText = 10f; //REMEMBER TO CHANGE TIME IN OBJECT_SPAWNER ALSO AS IT STOPS SPAWNING AFTER SAME AMOUNT OF TIME!

    // Update is called once per frame
    void Update()
    {
        Debug.Log("time is " + Time.time);

        if (Time.time >= timeBeforeEndText)
        {
            transition.SetTrigger("Start");
        }
    }
}
