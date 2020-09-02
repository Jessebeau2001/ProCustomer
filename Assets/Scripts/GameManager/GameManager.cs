using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

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
            //CountdownClock.OnCountdownFinish += GoToGameOverScene; //adding this GameOver function to the List of functions that will be called on this OnCountdownFinish event
            //CountPaintings.OnPaintingsCollected += GoToWinScene;
            //ShootLaser.OnLaserPlayerTrigger += ChangeSceneWithDelay;
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
            //CountdownClock.OnCountdownFinish -= GoToGameOverScene; //Unsubscribing this function from the event function list
            //CountPaintings.OnPaintingsCollected -= GoToWinScene;
            //ShootLaser.OnLaserPlayerTrigger -= ChangeSceneWithDelay;                
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------
    //CHANGING SCENES
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
    //FOR DEBUG
    //-----------------------------------------------------------------------------------------------------------------------
    public void Update()
    {
        //LoadScene();
    }
    public void LoadScene()
    {
        //menu
        if (Input.GetKeyDown(KeyCode.M))
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
        //game
        if (Input.GetKeyDown(KeyCode.G))
        {
            Destroy(gameObject);
            SceneManager.LoadScene(1);
        }
        //game over
        if (Input.GetKeyDown(KeyCode.O))
        {
            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }
        //win
        if (Input.GetKeyDown(KeyCode.V))
        {
            Destroy(gameObject);
            SceneManager.LoadScene(3);
        }
    }
}
