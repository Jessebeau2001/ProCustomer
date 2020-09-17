using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterFloorTrigger : MonoBehaviour
{
    public static event Action playMemory3;//tell VideoManager to play M3

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            Debug.Log("NPC looking at the picture");
            playMemory3();//play the memory 3
        }
    }
}
