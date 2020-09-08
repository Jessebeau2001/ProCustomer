using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    //public method because we need to call it from outside in the inspector when setting up the button

    //----------------------------------------------------------------
    //LOAD SCENE
    //----------------------------------------------------------------
    public void LoadScene(int buildIndex)
    {
        Destroy(gameObject);
        SceneManager.LoadScene(buildIndex); //load a scene based on the scene name
    }
    //----------------------------------------------------------------
    //EXIT GAME
    //----------------------------------------------------------------
    public void ExitGame()
    {
        Debug.Log("EXITING APPLICATION");
        Application.Quit();
    }
}
