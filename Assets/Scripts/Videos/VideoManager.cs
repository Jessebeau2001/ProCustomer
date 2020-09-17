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

    private bool isMemory1PLaying = false;

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
        // if(isMemory1PLaying == false)
        // {
            // this.GetComponentInChildren<MeshRenderer>().enabled = true;//make the obejct visible
            // this.GetComponentInChildren<VideoPlayer>().Play();//play the video
            // isMemory1PLaying = true;
        // }
    }
}
