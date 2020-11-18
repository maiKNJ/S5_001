using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewScene : MonoBehaviour
{
    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "Rocket_Launch")
        {
            SceneManager.LoadScene("Cutscene3", LoadSceneMode.Single);
        }

        if (SceneManager.GetActiveScene().name == "Cutscene3")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }

        if (SceneManager.GetActiveScene().name == "C4")
        {
            SceneManager.LoadScene("End_Scene", LoadSceneMode.Single);
        }

        if (SceneManager.GetActiveScene().name == "End_Scene")
        {
            SceneManager.LoadScene("End_end", LoadSceneMode.Single);
        }
    }
}
