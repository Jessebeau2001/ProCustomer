using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    //public method because we need to call it from outside in the inspector when setting up the button
    public void LoadNextScene(string sceneName)
    {
        Destroy(gameObject);
        SceneManager.LoadScene(sceneName); //load a scene based on the scene name
    }
    public void ExitGame()
    {
        Debug.Log("EXITING APPLICATION");
        Application.Quit();
    }
}
