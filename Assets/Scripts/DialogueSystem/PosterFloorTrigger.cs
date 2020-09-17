using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterFloorTrigger : MonoBehaviour
{
    public static event Action playMemory3;//tell VideoManager to play M3

    private bool _playM3Enabled = false;
    // Start is called before the first frame update
    void Start()
    {
        VideoManager.m3DonePlaying += playM3Enabled;
    }
    private void OnDestroy()
    {
        VideoManager.m3DonePlaying -= playM3Enabled;
    }
    private void playM3Enabled()
    {
        _playM3Enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC" && _playM3Enabled)
        {
            Debug.Log("NPC looking at the picture");
            playMemory3();//play the memory 3
        }
    }
}
