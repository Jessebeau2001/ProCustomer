using System;//for events
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LookingAtRecognition : MonoBehaviour
{
    //What is happening?
    //This script starts the dialogue -> triggers displaying the first dialogue 0
    //Triggers displaying the dialogue 1
    //Triggers displating the dialogue 2 + tells the NPC to walk to night stand


    RaycastHit hitInfo;
    public float interactionDistance = 300f;
    public static event Action npcRecognized;//for dialogue
    public int waitForSeconds;//how long should we wait between dialogue 2 and 3

    private bool dialogueStarted = false;//to start the dialogue only once
    private bool npcRecognizedOnce = false;//to show go to the 2nd dialogue and do it only once

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //raycast to recognize objects
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.green);
        if (Physics.Raycast(this.transform.position, transform.forward, out hitInfo, interactionDistance))
        {            
            //for dialogue
            if (hitInfo.collider.tag == "Blood")
            {
                if(dialogueStarted == false)//to start the dialogue only once
                {
                    GameObject.FindGameObjectWithTag("DialogueTrigger").GetComponent<DialogueTrigger>().TriggerDialogue();//start the dialogue
                    dialogueStarted = true;
                }
                
            }else
            if (hitInfo.collider.tag == "NPC")
            {
                if(npcRecognizedOnce == false)
                {
                    GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().DisplayNextSentence();//display next dialogue (in this case dialogue 1)
                    GameObject.FindGameObjectWithTag("NPC").GetComponent<NPC>().SetDest(GameObject.FindGameObjectWithTag("NightStand").transform.position);//set the destination of the NPC to the position of the night stand
                    //MAKE NPC CRY SOUND

                    npcRecognizedOnce = true;
                }
            }

            //For pathfinding
            if(hitInfo.collider.tag == "PickUp")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //fire event so the pathfinder can tell the npc to walk somewhere
        	        
                }
            }

        }
    }
}
