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
    //This script starts the dialogue
    //Based on conditions and recognized objects it is telling the DialogueTrigger to show the next dialogue


    RaycastHit hitInfo;
    public float interactionDistance = 300f;
    public int waitForSeconds = 5;

    private bool dialogueStarted = false;//to start the dialogue only once
    private bool npcRecognizedOnce = false;//to show go to the 2nd dialogue and do it only once
    private bool npcRecognizedTwice = false;
    private bool pictureFrameRecognizedOnce = false;
    private bool pictureFrameRecognizedTwice = false;

    //events for playing videos
    public static event Action playMemory1;

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
            //Debug.Log("Collider = " +hitInfo.collider.tag);
            //for dialogue
            if (hitInfo.collider.tag == "Blood")
            {
                if(dialogueStarted == false)//to start the dialogue only once
                {
                    GameObject.FindGameObjectWithTag("DialogueTrigger").GetComponent<DialogueTrigger>().TriggerDialogue();//start the dialogue
                    dialogueStarted = true;
                }
                
            }
            if (hitInfo.collider.tag == "NPC")
            {
                //NPC looked at 1st time
                if (npcRecognizedOnce == false && dialogueStarted && pictureFrameRecognizedOnce == false)
                {
                    GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().DisplayNextSentence();//display next dialogue (in this case dialogue 1)
                    GameObject.FindGameObjectWithTag("NPC").GetComponent<NPC>().SetDest(GameObject.FindGameObjectWithTag("NightStand").transform.position);//set the destination of the NPC to the position of the night stand
                    //MAKE NPC CRY SOUND

                    npcRecognizedOnce = true;
                }
                //NPC looked at 2nd time, after we looked at the pictureFrame
                else if (npcRecognizedTwice == false && npcRecognizedOnce && dialogueStarted && pictureFrameRecognizedOnce)
                {
                    //display this dialogue, then wait for some time and display the next one again
                    GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().DisplayNextSentence();//display next dialogue (in this case dialogue 1)
                    StartCoroutine(CountdownToStart());//wait then display the next dialogue

                    npcRecognizedTwice = true;
                }
            }
            
            if (hitInfo.collider.tag == "PictureFrame")
            {
                if (pictureFrameRecognizedOnce == false && npcRecognizedOnce)//didn't look at the pictureFrame before and the NPC was already recognized
                {
                    GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().DisplayNextSentence();//display next dialogue (in this case dialogue 1)

                    pictureFrameRecognizedOnce = true;
                }else if (pictureFrameRecognizedTwice == false && npcRecognizedTwice)//looked at the picture then at the npc, she was crying and now he realised hes dead
                {
                    StartCoroutine(CountdownToStart());//wait then display the next dialogue

                    //PLAY THE MEMORY 1 and then next dialogue
                    playMemory1();//fire this event so the VideoManager can play a video

                    pictureFrameRecognizedTwice = true;
                }
            }

            //For pathfinding
            if (hitInfo.collider.tag == "PickUp")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //fire event so the pathfinder can tell the npc to walk somewhere
        	        
                }
            }

        }
    }

    //Count down timer
    IEnumerator CountdownToStart()
    {
        while(waitForSeconds > 0)
        {
            yield return new WaitForSeconds(1f);//wait for x seconds and come to this code later

            waitForSeconds--;
        }
        Debug.Log("Waiting done.");
        
        //display another dialogue
        GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().DisplayNextSentence();
    }
}
