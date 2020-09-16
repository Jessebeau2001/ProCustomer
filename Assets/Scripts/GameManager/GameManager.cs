using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ControlType { d1, d2, d3 } //to be able to change the way to controll the player
public class GameManager : MonoBehaviour
{
    //FOR PAUSE MENU
    private GameObject pauseMenu;//get the gameMenu obejct to be able to show/hide it
    private bool pauseMenuVisible = false;

    //Start of the game -> player cannot move
    public static event Action playerCanMove;//after you look at the NPC 2nd time

    //For dialogues
    public ControlType dialogueControl;//for switching based on what to control the character
    private bool d1NotDisplayed = true;//to display D1 only once and then never againprivate bool d2Displayed = false;
    private bool d2Displayed = false;

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
            LookingAtRecognition.npcRecognized += showDialogueNPC;
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
            LookingAtRecognition.npcRecognized -= showDialogueNPC;
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------
    //UPDATE
    //-----------------------------------------------------------------------------------------------------------------------
    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {

            //Pause menu logic
            //-------------------------------
            if (Input.GetKeyDown(KeyCode.P))
            {//if P was pressed & we are in the prototype scene

                createPauseMenu();
            }
            
            if (pauseMenuVisible)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PhysicsMovement>().enabled = false;//player cannot move
                //Buttons pressed logic
                //--------------------------------------
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("EXITING APPLICATION");
                    Application.Quit();//exit the game
                }
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
        //Debug.Log("in create pause menu");
        
        //get the pause menu object to be able to create it
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");//find the pause menu object in hierarchy

        //if there is no object called "PauseMenu"
        //create it x otherwise destroy it
        if (pauseMenuVisible == false)
        {
            //create it = show it
            //Debug.Log("show pause menu");
            pauseMenu.GetComponentInChildren<Canvas>().enabled = true;//enable canvas
            pauseMenuVisible = true;
        }
        else
        {
            //hide it
            //Debug.Log("hide pause menu");
            pauseMenu.GetComponentInChildren<Canvas>().enabled = false;//disable canvas
            pauseMenuVisible = false;
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
    //DIALOGUES
    //-----------------------------------------------------------------------------------------------------------------------
    private void showDialogueNPC()
    {
        //SHOW D2
        //for future features -> check if the audio 1 is not playing anymore then display this dialogue and delete the onld one
        if(!d2Displayed && !d1NotDisplayed)//was not displayed yet and the d1 was already displayed
        {
            GameObject.FindGameObjectWithTag("D2").GetComponentInChildren<Text>().enabled = true;
            playerCanMove();//player can move again -> player>physics movement

            //disable previous dialogues
            GameObject.FindGameObjectWithTag("D1").GetComponentInChildren<Text>().enabled = false;

            d2Displayed = true;//so next time you look at the NPC you will say another dialogue not this one again
        }
    }
}
