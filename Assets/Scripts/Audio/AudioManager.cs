using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource gameMusic;
    public AudioSource doorKnob;
    public AudioSource allyCry;
    public AudioSource menuMusic;

    //checking if music is already playing
    private bool isGameMusicPlaying = false;
    private bool isMenuMusicPlaying = false;

    //public getter-------------------------------------------------------------
    //singleton, we can access this game manager by this method
    //--------------------------------------------------------------------------
    public static AudioManager GetManager()
    {
        return currentAudioManager;
    }
    static AudioManager currentAudioManager = null;//making sure there is always only one game manager in the scene

    //--------------------------------------------------------------------------
    //ONLY ONE GAME MANAGER IN THE GAME
    //--------------------------------------------------------------------------
    private void Awake()
    {
        //if there is no GameManager yet
        if (currentAudioManager == null)
        {
            currentAudioManager = this; //this is the current (and only one) GameManager
            DontDestroyOnLoad(gameObject);

            //listen to events to play audioSources
            LookingAtRecognition.playAudioDoorKnob += playDoorKnob;
            LookingAtRecognition.playAllyCry += playAllyCry;
        }
        else
        {
            Destroy(gameObject); //otherwise destroy this game object to prevent having multiple GameManagers
        }
    }
    private void OnDestroy()
    {
        if (currentAudioManager == this)//if this is the current game manager
        {
            currentAudioManager = null;//set it to null on destroy

            LookingAtRecognition.playAudioDoorKnob -= playDoorKnob;
            LookingAtRecognition.playAllyCry -= playAllyCry;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Game music-----------------------------------------
        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            if (!isGameMusicPlaying)
            {
                gameMusic.Play();
                isGameMusicPlaying = true;
            }

            if (isMenuMusicPlaying)
            {
                menuMusic.Stop();//just in case make menuMusic stop playing
            }
        }
        //Menu music-----------------------------------------
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (!isMenuMusicPlaying)
            {
                menuMusic.Play();
                isMenuMusicPlaying = true;
            }
        }
    }

    private void playDoorKnob()
    {
        doorKnob.Play();
    }
    private void playAllyCry()
    {
        allyCry.Play();
    }
}
