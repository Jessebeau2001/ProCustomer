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
    //memory 2
    public static event Action m2Playing;
    public static event Action m2DonePlaying;

    //for checking if a memory was played -> to hide/display the dialogue box
    private bool wasM1StartedPlaying = false;
    private bool wasM2StartedPlaying = false;

    private bool canPlayMem2 = false;

    //private bool wasM1DialogueDisplayedAgain = false; //JESSE: Commented this for now duo to warning CS0414

    private void Awake()
    {
        //subscribe
        //M1
        LookingAtRecognition.playMemory1 += playM1;
        memory1.loopPointReached += CheckOverM1;
        //M2
        TableFloorTrigger.playMemory2 += playM2;
        memory2.loopPointReached += CheckOverM2;
        //Can Play Memory 2
        LookingAtRecognition.CanPlayMemory2 += EnableMemory2;
    }

    private void OnDestroy()
    {
        //Unsubscribing!
        //M1
        LookingAtRecognition.playMemory1 -= playM1;
        memory1.loopPointReached -= CheckOverM1;
        //M2
        TableFloorTrigger.playMemory2 -= playM2;
        memory2.loopPointReached -= CheckOverM2;
        LookingAtRecognition.CanPlayMemory2 -= EnableMemory2;
    }
    private void EnableMemory2() {
        canPlayMem2 = true;
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
    
    //MEMORY 2---------------------------------------------------
    private void playM2()
    {
        if (!canPlayMem2) return;
        if(wasM2StartedPlaying == false)
        {
            memory2.Play();
            wasM2StartedPlaying = true;

            m2Playing();
        }
    }
    //for showing the dialogue box again
    private void CheckOverM2(UnityEngine.Video.VideoPlayer memory2)
    {
        m2DonePlaying();
    }

    //MEMORY 3---------------------------------------------------
}
