using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeSceneChange : MonoBehaviour
{
    // Start is called before the first frame update

    private float timer = 0.0f;
    private float waitTime = 9000;

    public Animator transistion;
    public float transTime = 1f;
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
            //NewScene();
            LoadScene();
        }

    }

    public void NewScene()
    {
       // StartCoroutine(LoadScene());
    }

    public void LoadScene()
    {
        //Play animation
        transistion.SetTrigger("start");

        //Wait
        //yield return new WaitForSeconds(transTime);

        //New scene
        //SceneManager.LoadScene("Rocket_Launch", LoadSceneMode.Single);
    }

    public void complete()
    {
        SceneManager.LoadScene("Rocket_Launch", LoadSceneMode.Single);
    }
}
