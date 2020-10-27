using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewScene : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("earth", LoadSceneMode.Additive);
    }
}
