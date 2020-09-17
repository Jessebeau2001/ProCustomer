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

    //events for audio
    public static event Action playAudioDoorKnob;
    public static event Action playAllyCry;

    void Update()
    {
        //raycast to recognize objects
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.green);
        if (Physics.Raycast(this.transform.position, transform.forward, out hitInfo, interactionDistance))
        {
            //JESSE: for opening and closing curtain
            if (hitInfo.collider.tag == "InteractableCurtain") {
                ((Curtain) hitInfo.collider.gameObject.GetComponent(typeof(Curtain))).EnableText();
                if (Input.GetKeyDown(KeyCode.F))
                    ((Curtain) hitInfo.collider.gameObject.GetComponent(typeof(Curtain))).SwapState();
            }

            //---------------------------------------------------------------------------------------
            //BLOOD
            //---------------------------------------------------------------------------------------
            if (hitInfo.collider.tag == "Blood")
            {
                //0---------------------Look at Blood, start the dialogue system
                if (dialogueStarted == false)//to start the dialogue only once
                {
                    GameObject.FindGameObjectWithTag("DialogueTrigger").GetComponent<DialogueTrigger>().TriggerDialogue();//start the dialogue

                    playAudioDoorKnob();//SOUND

                    dialogueStarted = true;
                }
                
            }

            //---------------------------------------------------------------------------------------
            //NPC
            //---------------------------------------------------------------------------------------
            if (hitInfo.collider.tag == "NPC")
            {
                //1---------------------NPC looked at 1st time
                if (npcRecognizedOnce == false && dialogueStarted && pictureFrameRecognizedOnce == false)
                {
                    GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().DisplayNextSentence();//display next dialogue (in this case dialogue 1)
                    GameObject.FindGameObjectWithTag("NPC").GetComponent<NPC>().SetDest(GameObject.FindGameObjectWithTag("NightStand").transform.position);//set the destination of the NPC to the position of the night stand

                    playAllyCry();//SOUND

                    npcRecognizedOnce = true;
                }
                //3---------------------NPC looked at 2nd time, after we looked at the pictureFrame
                else if (npcRecognizedTwice == false && npcRecognizedOnce && dialogueStarted && pictureFrameRecognizedOnce)
                {
                    //display this dialogue, then wait for some time and display the next one again
                    GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().DisplayNextSentence();//display next dialogue (in this case dialogue 1)
                    //StartCoroutine(CountdownToStart());//wait then display the next dialogue

                    npcRecognizedTwice = true;
                }
            }

            //---------------------------------------------------------------------------------------
            //PICTURE FRAME
            //---------------------------------------------------------------------------------------
            if (hitInfo.collider.tag == "PictureFrame")
            {
                //2---------------------Looking at PictureFrame 1st time
                if (pictureFrameRecognizedOnce == false && npcRecognizedOnce)//didn't look at the pictureFrame before and the NPC was already recognized
                {
                    GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().DisplayNextSentence();//display next dialogue (in this case dialogue 1)

                    pictureFrameRecognizedOnce = true;
                }else
                //4---------------------Looking at PictureFrame 2nd time
                if (pictureFrameRecognizedTwice == false && npcRecognizedTwice)//looked at the picture then at the npc, she was crying and now he realised hes dead
                {
                    Debug.Log("second frame.");

                    StartCoroutine(CountdownToStart());//wait then display the next dialogue

                    //PLAY THE MEMORY 1 and then next dialogue
                    playMemory1();//fire this event so the VideoManager can play a video

                    pictureFrameRecognizedTwice = true;
                }
            }
        }
    }


    //-------------------------------------------------------------------------------------------------
    //Wait then next dialogue
    //-------------------------------------------------------------------------------------------------
    IEnumerator CountdownToStart()
    {
        while(waitForSeconds > 0)//Count down timer
        {
            yield return new WaitForSeconds(1f);//wait for x seconds and come to this code later

            waitForSeconds--;
        }
        Debug.Log("Waiting done.");
        
        //display another dialogue
        GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().DisplayNextSentence();
    }
}
