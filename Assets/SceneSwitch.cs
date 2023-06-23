using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{



    public void Quit() 
    {
        Application.Quit();
    }

    public void SwitchScene(int value)
    {

        SceneManager.LoadScene(value);
    }


    public void Awake()
    {
    }
}
