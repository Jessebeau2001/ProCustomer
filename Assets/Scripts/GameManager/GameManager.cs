using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;//get the gameMenu obejct to be able to show/hide it
    private bool pauseMenuVisible = false;

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

            //Subscribing! (example)
            //CountdownClock.OnCountdownFinish += GoToGameOverScene; //adding this GameOver function to the List of functions that will be called on this OnCountdownFinish event
        }
        else
        {
            Destroy(gameObject); //otherwise destroy this game object to prevent having multiple GameManagers
        }
    }

    private void OnDestroy()
    {
        if(currentManager == this)//if this is the current game manager
        {
            currentManager = null;//set it to null on destroy

            //Unsubscribing! (example)
            //ShootLaser.OnLaserPlayerTrigger -= ChangeSceneWithDelay;                
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------
    //CHANGING SCENES (example)
    //-----------------------------------------------------------------------------------------------------------------------
    /*
    public void ChangeSceneWithDelay()
    {
        Invoke("GoToGameOverScene", timeBeforeRespawn);
    }
    
    private void RestartCurrentScene()
    {
        Debug.Log("RESTARTING CURRENT SCENE");
        //Destroy(gameObject); //destroy this game manager from previous scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //restart current scene
    }*/
    //-----------------------------------------------------------------------------------------------------------------------
    //UPDATE
    //-----------------------------------------------------------------------------------------------------------------------
    public void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            //display pause menu
            if (Input.GetKeyDown(KeyCode.P))
            {//if ESC was pressed & we are in the prototype scene

                createPauseMenu();
            }
            //if pause menu player cannot move
            if (pauseMenuVisible)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PhysicsMovement>().enabled = false;//player cannot move
            }
            else
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PhysicsMovement>().enabled = true;//player can move
            }
        }

        //check if should exit application (pause menu -> exit)
        buttonPressedAction();
    }
    //-----------------------------------------------------------------------------------------------------------------------
    //PAUSE MENU
    //-----------------------------------------------------------------------------------------------------------------------

    //Display/Hide it
    //-------------------------------
    private void createPauseMenu()
    {
        Debug.Log("in create pause menu");
        
        //get the pause menu object to be able to create it
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");

        //if there is no object called "PauseMenu"
        //create it
        //otherwise destroy it
        if (pauseMenuVisible == false)
        {
            //create it = show it
            Debug.Log("show pause menu");
            pauseMenu.GetComponentInChildren<Canvas>().enabled = true;
            pauseMenuVisible = true;

            //display the mouse coursor
            Cursor.visible = true;
        }
        else
        {
            //hide it
            Debug.Log("hide pause menu");
            pauseMenu.GetComponentInChildren<Canvas>().enabled = false;
            pauseMenuVisible = false;

            //hide the mouse coursor
            Cursor.visible = false;
        }
    }
    //Button functionality (can't actually select buttons, just press something)
    //-------------------------------
    private void buttonPressedAction()
    {
        if (pauseMenuVisible)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("EXITING APPLICATION");
                Application.Quit();
            }
        }
    }
}
