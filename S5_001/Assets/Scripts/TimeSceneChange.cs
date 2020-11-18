using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeSceneChange : MonoBehaviour
{
    // Start is called before the first frame update

    private float timer = 0.0f;
    private float waitTime = 9000;

    public Animator transistion;
    public float transTime = 10f;
    public float outroTime = 3f;
    public AudioSource audioclip;
    void Start()
    {
        Application.targetFrameRate = 30;

    }

    // Update is called once per frame
    void Update()
    {

        /* timer += Time.frameCount;
         Debug.Log("time" + timer);

         if (timer >= waitTime)
         {
             //Debug.Log("inside 5 collsions");
             //NewScene();

             //LoadScene();

             transistion.SetTrigger("start");
         }
        */
        
        if (Time.time >= transTime)
        {
            transistion.SetTrigger("start");
        }
        //Debug.Log("time is " + Time.time);
        if (Time.time >= transTime + outroTime)
        {
            if (SceneManager.GetActiveScene().name == "IntroScene")
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                SceneManager.LoadScene("earth", LoadSceneMode.Single);
                audioclip.Stop();
            }
            if (SceneManager.GetActiveScene().name == "earth")
            {
                SceneManager.LoadScene("Rocket_Launch", LoadSceneMode.Single);
            }

            if (SceneManager.GetActiveScene().name == "End_Scene")
            {
                SceneManager.LoadScene("End_end", LoadSceneMode.Single);
            }

        }

    }

    //NOT USING THE CODE BELOW!
    public void NewScene()
    {
       // StartCoroutine(LoadScene());
    }

    /*IEnumerator LoadScene()
    {
        transistion.SetTrigger("start");
        yield return new WaitForSeconds(transTime);
        complete();
    }*/
    public void LoadScene()
    {
        //Play animation
        transistion.SetTrigger("start");

        //Wait
        //yield return new WaitForSeconds(transTime);

        //New scene
        //SceneManager.LoadScene("Rocket_Launch", LoadSceneMode.Single);
        //Debug.Log("inside loadScene");
        complete();
        
    }

    public void complete()
    {
        SceneManager.LoadScene("Rocket_Launch", LoadSceneMode.Single);
       
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        /*if (SceneManager.sceneCountInBuildSettings <= 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }*/
    }
}
