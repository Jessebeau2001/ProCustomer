using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //FOR PAUSE MENU
    private GameObject pauseMenu;//get the gameMenu obejct to be able to show/hide it
    private bool pauseMenuVisible = false;
    //FOR TUTORIAL UI
    private GameObject tutorialUI;
    private bool tutorialUIDisplayed = false;

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
            PickUpRecognition.PickUpRecognized += showTutorialUI;
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

            //Unsubscribing!
            PickUpRecognition.PickUpRecognized -= showTutorialUI;              
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
        //to destoroy the tutorial object once it was displayed & right mouse button pressed
        destroyTutorialUI();
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

    //-----------------------------------------------------------------------------------------------------------------------
    //SHOW UI
    //-----------------------------------------------------------------------------------------------------------------------

    //Tutorial UI - display
    //-------------------------------
    private void showTutorialUI()
    {
        Debug.Log("show tutorial UI 1/2");

        if (tutorialUIDisplayed == false)
        {
            Debug.Log("show tutorial UI 2/2");

            //find the tutorial UI in the hierarchy
            tutorialUI = GameObject.FindGameObjectWithTag("TutorialUI");
            //make it visible
            tutorialUI.GetComponentInChildren<Canvas>().enabled = true;
            
            tutorialUIDisplayed = true;//the tutorial was onced displayed
        }
    }
    //Tutorial UI - destroy to not show again
    //-------------------------------
    private void destroyTutorialUI()
    {

        if (tutorialUIDisplayed == true)
        {
            Debug.Log("destroy tutorial UI 1/3 - tutorial UI displayed");

            if (Input.GetMouseButtonDown(0))//left mouse button
            {
                Debug.Log("destroy tutorial UI 2/3 - mouse button pressed");

                if (tutorialUI != null)
                {
                    Debug.Log("destroy tutorial UI 3/3 - destroy");

                    Destroy(tutorialUI);
                }
            }
        }
    }
}
