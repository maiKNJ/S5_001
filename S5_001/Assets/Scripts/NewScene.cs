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
            SceneManager.LoadScene("Cutscene3", LoadSceneMode.Additive);
        }

        if (SceneManager.GetActiveScene().name == "Cutscene3")
        {
            SceneManager.LoadScene("C4", LoadSceneMode.Additive);
        }

        if (SceneManager.GetActiveScene().name == "C4")
        {
            SceneManager.LoadScene("End_Scene", LoadSceneMode.Additive);
        }
    }
}
