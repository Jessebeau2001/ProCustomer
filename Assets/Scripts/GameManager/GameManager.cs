using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    //FOR PAUSE MENU
    private GameObject pauseMenu;//get the gameMenu obejct to be able to show/hide it
    private bool pauseMenuVisible = false;

    //Start of the game -> player cannot move
    #pragma warning disable CS0067
    public static event Action playerCanMove;//after you look at the NPC 2nd time
    #pragma warning restore CS0067 //JESSE: Disables/enables warning that says that the event is unused even though it is used

    //public getter-------------------------------------------------------------
    //singleton, we can access this game manager by this method
    //--------------------------------------------------------------------------
    public static GameManager GetManager()
    {
        return currentManager;
    }
    static GameManager currentManager = null;//making sure there is always only one game manager in the scene

    //--------------------------------------------------------------------------
    //ONLY ONE GAME MANAGER IN THE GAME
    //--------------------------------------------------------------------------
    private void Awake()
    {
        //if there is no GameManager yet
        if(currentManager == null)
        {
            currentManager = this; //this is the current (and only one) GameManager
            DontDestroyOnLoad(gameObject);

            //Subscribing!
        }
        else
        {
            Destroy(gameObject); //otherwise destroy this game object to prevent having multiple GameManagers
        }
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "EndScene")
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(0);//menu
            }
        }
    }
    private void OnDestroy()
    {
        if(currentManager == this)//if this is the current game manager
        {
            currentManager = null;//set it to null on destroy

            //Unsubscribing!
        }
    }
}
