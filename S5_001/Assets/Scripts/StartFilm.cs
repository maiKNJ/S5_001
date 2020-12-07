using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFilm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown("p"))
        {
            Time.timeScale = 1;
        }
    }
}
