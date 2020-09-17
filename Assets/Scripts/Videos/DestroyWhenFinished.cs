using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DestroyWhenFinished : MonoBehaviour
{
    [SerializeField] VideoPlayer VidPlayer;
    public bool isPlayerStarted = false;

    void Start() {
        VidPlayer = GetComponent<VideoPlayer>();
    }

    void Update() {
        if (isPlayerStarted == false && VidPlayer.isPlaying == true) {
            // When the player is started, set this information
            isPlayerStarted = true;
        }
        if (isPlayerStarted == true && VidPlayer.isPlaying == false ) {
            // Wehen the player stopped playing, hide it
            VidPlayer.gameObject.SetActive(false);
        }
    }  
}
