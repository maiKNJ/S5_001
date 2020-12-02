using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Years : MonoBehaviour
{

    Text years;
    // Start is called before the first frame update
    void Start()
    {
        years = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (object_spawner.numOfSat < 5)
        {
            years.text = "1940's";
        }
        if (object_spawner.numOfSat >= 5 && object_spawner.numOfSat <= 10)
        {
            years.text = "1950's";
        }
        if (object_spawner.numOfSat >= 11 && object_spawner.numOfSat <= 25)
        {
            years.text = "1960's";
        }
        if (object_spawner.numOfSat >= 26 && object_spawner.numOfSat <= 30)
        {
            years.text = "1970's";
        }
        if (object_spawner.numOfSat >= 31 && object_spawner.numOfSat <= 35)
        {
            years.text = "1980's";
        }
        if (object_spawner.numOfSat >= 36 && object_spawner.numOfSat <= 40)
        {
            years.text = "1990's";
        }
        if (object_spawner.numOfSat >= 41 && object_spawner.numOfSat <= 45)
        {
            years.text = "2000's";
        }
        if (object_spawner.numOfSat >= 46 && object_spawner.numOfSat <= 50)
        {
            years.text = "2010's";
        }
        if (object_spawner.numOfSat >= 51 && object_spawner.numOfSat <= 55)
        {
            years.text = "2020's";
        }
        if (object_spawner.numOfSat >= 56)
        {
            years.text = "Future";
        }
    }
}
