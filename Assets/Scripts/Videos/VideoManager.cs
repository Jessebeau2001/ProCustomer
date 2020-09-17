using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    //get the videos to play
    public VideoPlayer memory1;
    public VideoPlayer memory2;
    public VideoPlayer memory3;

    //to tell if the memories are playing or are done playing
    public static event Action m1Playing;
    public static event Action m1DonePlaying;

    //for checking if a memory was played -> to hide/display the dialogue box
    private bool wasM1StartedPlaying = false;
    private bool wasM1DialogueDisplayedAgain = false;

    private void Awake()
    {
        //subscribe
        LookingAtRecognition.playMemory1 += playM1;
        memory1.loopPointReached += CheckOverM1;
    }

    private void OnDestroy()
    {
        //Unsubscribing!
        LookingAtRecognition.playMemory1 -= playM1;
        memory1.loopPointReached -= CheckOverM1;
    }
    void Update()
    {
        
    }
    //--------------------------------------------------------------------------------------
    //EVENTS to Display/Hide dialogue box when video is playing
    //--------------------------------------------------------------------------------------
    private void playM1()
    {
        if(wasM1StartedPlaying == false)
        {
            memory1.Play();
            wasM1StartedPlaying = true;

            m1Playing();//event for DialogueManager to hide dialogueBox canvas
        }
    }
    //for showing the dialogue box again
    private void CheckOverM1(UnityEngine.Video.VideoPlayer memory1)
    {
        m1DonePlaying();
    }
}
