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
    public static event Action m1Done;

    private void Awake()
    {
        //subscribe
        LookingAtRecognition.playMemory1 += playM1;
    }

    private void OnDestroy()
    {
        //Unsubscribing!
        LookingAtRecognition.playMemory1 -= playM1;
    }
    void Update()
    {
        
    }
    private void playM1()
    {
        memory1.Play();
        m1Playing();//event for DialogueManager to hide dialogueBox canvas
        
    }
}
