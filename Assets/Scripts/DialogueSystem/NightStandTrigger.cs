using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightStandTrigger : MonoBehaviour
{
    //did the NPC walked to the night stand for the 1st time?
    private bool firstTimeCollision;
   private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            //AND THE PLAYER ALREADY LOOKED AT THE PICTURE FRAME
            //display the next dialogue
            //GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().DisplayNextSentence();//display next dialogue (in this case dialogue 2)
            //firstTimeCollision = true;
        }
    }
}
