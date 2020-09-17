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

    //for playing memory 3
    public DialogueManager Dmanag;

    //to tell if the memories are playing or are done playing
    public static event Action m1Playing;
    public static event Action m1DonePlaying;
    //memory 2
    public static event Action m2Playing;
    public static event Action m2DonePlaying;
    //memory 3
    public static event Action m3Playing;
    public static event Action m3DonePlaying;

    //for checking if a memory was played -> to hide/display the dialogue box
    private bool didM1StartPlaying = false;
    private bool didM2StartPlaying = false;
    private bool didM3StartPlaying = false;

    private bool canPlayMem2 = false;
    private bool canPlayMem3 = false;

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
        LookingAtRecognition.playMemory2 += EnableMemory2;
        //M3
        memory3.loopPointReached += CheckOverM3;
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
        LookingAtRecognition.playMemory2 -= EnableMemory2;
        //M3
        memory3.loopPointReached -= CheckOverM3;
        LookingAtRecognition.playMemory3 -= playM3;
    }
    private void EnableMemory2() {
        canPlayMem2 = true;
    }
    //--------------------------------------------------------------------------------------
    //EVENTS to Display/Hide dialogue box when video is playing
    //--------------------------------------------------------------------------------------
    private void playM1()
    {
        if(didM1StartPlaying == false)
        {
            memory1.Play();
            didM1StartPlaying = true;

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
        if(didM2StartPlaying == false)
        {
            memory2.Play();
            didM2StartPlaying = true;

            m2Playing();
        }
    }
    //for showing the dialogue box again
    private void CheckOverM2(UnityEngine.Video.VideoPlayer memory2)
    {
        m2DonePlaying();
    }

    //MEMORY 3---------------------------------------------------
    //if both lightsOut and curtainClosed then play it -> LookAtRecognition event that everything happened before M3 should be played
    private void playM3()
    {
        Debug.Log("VideoManager M3 - play");

        if (!canPlayMem3) return;
        if (didM3StartPlaying == false)
        {   
            memory3.Play();//play the video
            didM3StartPlaying = true;

            m3Playing();//event
        }
    }

    private void CheckOverM3(UnityEngine.Video.VideoPlayer memory3)
    {
        Debug.Log("VideoManager M3 - done");
        m3DonePlaying();//event
    }
}
