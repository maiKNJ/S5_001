using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    WebCamTexture webCam;
    int whitePixels = 0;

    // Start is called before the first frame update
    void Start()
    {
        webCam = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = webCam;
        webCam.Play();

    }

    void Update()
    {

        Debug.Log("Webcam " + webCam.GetPixels());
        Color[] pixelColor = webCam.GetPixels(0, 0, 10, 10);

        foreach (Color color in pixelColor)
        {
            if (color.r == 1 && color.g == 1 && color.b == 1 && color.a == 1)
            {
                whitePixels++;
                //Debug.Log("whitePixels " + whitePixels);

                if (whitePixels == 100) { whitePixels = 0; }

                if (whitePixels >= 50) { Debug.Log("Do what we want"); }

            }

        }

        /*for (int i = 0; i < 100; i++)
        {
            if (pixelColor[i].r == 1 && pixelColor[i].g == 1 && pixelColor[i].b == 1 && pixelColor[i].a == 1)
            {
                Debug.Log("PIXELCOLOR FIND COLOR");
            }

        }*/



        /*Color color = webCam.GetPixel(0, 0);
        Debug.Log("color " + color);

        if (color.r == 1 && color.g == 1 && color.b == 1 && color.a == 1)
        {
            Debug.Log("SUCCESS");
        }*/

    }
}
