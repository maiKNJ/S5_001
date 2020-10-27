using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeSceneChange : MonoBehaviour
{
    // Start is called before the first frame update

    private float timer = 0.0f;
    private float waitTime = 9000;
    void Start()
    {
        Application.targetFrameRate = 30;

    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.frameCount;
        Debug.Log("time" + timer);

        if (timer >= waitTime)
        {
            Debug.Log("inside 5 collsions");
            SceneManager.LoadScene("Rocket_Launch", LoadSceneMode.Single);
        }

    }
}
