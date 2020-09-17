using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableFloorTrigger : MonoBehaviour
{
    //Used for 
    public static event Action penOnFloor;
    public static event Action playMemory2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pen")
        {
            Debug.Log("Pen on floor. sending event to LookAtRecognition.");
            penOnFloor();//so in the 2nd part of the game the NPC walks to the table and finds the first letter piece
        }
        if (other.gameObject.tag == "NPC")
        {
            Debug.Log("Pen on floor. sending event to LookAtRecognition.");
            playMemory2();//play the memory 2
        }
    }
        
}
