using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LookingAtRecognition : MonoBehaviour
{
    //What is happening?
    //This script starts the dialogue
    //Based on conditions and recognized objects it is telling the DialogueTrigger to show the next dialogue

    //recognition condition
    private bool _canRecognizeDoor = false;

    #pragma warning disable CS0067
    public NPC npc;
    public DialogueManager dManage;
    public Transform nightStand;

    RaycastHit hitInfo;
    public float interactionDistance = 300f;
    public int waitForSeconds = 5;

    private bool _dialogueStarted = false;//to start the dialogue only once
    private bool npcRecognizedOnce = false;//to show go to the 2nd dialogue and do it only once
    private bool npcRecognizedTwice = false;
    private bool pictureFrameRecognizedOnce = false;
    private bool pictureFrameRecognizedTwice = false;
    //after memory 1
    private bool wasAfterM1ConversationPlayed = false;
    private bool canAfterM1ConversationStart = false;
    private bool canPenBeFound = false;//for after dialogue num6 find the pen (look at it)
    private bool wasDialogue7Displayed = false;
    //After memory 2
    private bool wasDialogue9Display = false;
    private bool canDisplay11 = false;
    private bool isActionsBeforeMemory3Done = false;


    //events for playing videos
    public static event Action playMemory1;
    public static event Action playMemory2;
    public static event Action playMemory3;

    //events for audio
    public static event Action playAudioDoorKnob;
    public static event Action playAllyCry;

    //other events
    public static event Action changeToEndScene;

    #pragma warning restore CS0067

    private void Awake()
    {
        VideoManager.m1DonePlaying += afterM1ConversationCanStart;
        TableFloorTrigger.penOnFloor += makeAllyFindFirstLetterPiece;//make Ally walk to the table and find the first letter piece
        VideoManager.m2DonePlaying += afterM2PlayedDialogues;//for dialogue num 9
    }
    private void OnDestroy()
    {
        VideoManager.m1DonePlaying -= afterM1ConversationCanStart;
        TableFloorTrigger.penOnFloor -= makeAllyFindFirstLetterPiece;
        VideoManager.m2DonePlaying -= afterM2PlayedDialogues;//for dialogue num 9
    }
    void Update()
    {
        //raycast to recognize objects
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.green);
        if (Physics.Raycast(this.transform.position, transform.forward, out hitInfo, interactionDistance, -1, QueryTriggerInteraction.Ignore))
        {
            //JESSE: for opening and closing curtain
            GameObject obj = hitInfo.collider.gameObject;
            //Debug.Log("Looking at tag: " + obj.tag);

            switch (obj.tag) {
                case "InteractableCurtain":
                    ((Curtain) obj.GetComponent(typeof(Curtain))).enableText = true;
                    if (Input.GetKeyDown(KeyCode.F))
                        ((Curtain) obj.GetComponent(typeof(Curtain))).SwapState();
                    break;

                case "LightSwitch":
                    ((Lightswitch) obj.GetComponent(typeof(Lightswitch))).showText = true;
                    if (Input.GetKeyDown(KeyCode.F))
                        ((Lightswitch) obj.GetComponent(typeof(Lightswitch))).SwapState();
                    break;

                case "Blood":
                    //Look at Blood, start the dialogue system
                    if (_dialogueStarted == false) { //to start the dialogue only once
                        GameObject.FindGameObjectWithTag("DialogueTrigger").GetComponent<DialogueTrigger>().TriggerDialogue();//start the dialogue
                        playAudioDoorKnob(); //SOUND
                        _dialogueStarted = true;
                    }
                    break;

                case "NPC":
                    //1st time looing at NPC
                    if (npcRecognizedOnce == false && _dialogueStarted && pictureFrameRecognizedOnce == false) {
                        Debug.Log("First time looking at NPC");
                        dManage.DisplayNextSentence();
                        npc.SetDest(nightStand.position);
                        playAllyCry();//SOUND
                        npcRecognizedOnce = true;
                    }
                    //2nd time looking at NPC (After PicFrame)
                    else if (npcRecognizedTwice == false && npcRecognizedOnce && _dialogueStarted && pictureFrameRecognizedOnce) {
                        Debug.Log("Second time looking at NPC");
                        //display this dialogue, then wait for some time and display the next one again
                        dManage.DisplayNextSentence(); //display next dialogue (in this case dialogue 1)
                        //StartCoroutine(CountdownToStart()); //wait then display the next dialogue
                        npcRecognizedTwice = true;
                    }
                    break;

                case "PictureFrame":
                    //2 - Looking at PictureFrame 1st time
                    if (pictureFrameRecognizedOnce == false && npcRecognizedOnce) { //didn't look at the pictureFrame before and the NPC was already recognized
                        Debug.Log("First PictureFrame Check");
                        dManage.DisplayNextSentence();//display next dialogue (in this case dialogue 1)
                        pictureFrameRecognizedOnce = true;
                    } else //4 - Looking at PictureFrame 2nd time
                    if (pictureFrameRecognizedTwice == false && npcRecognizedTwice) { //looked at the picture then at the npc, she was crying and now he realised hes dead
                        Debug.Log("Second PictureFrame Check");
                        StartCoroutine(CountdownToStart(waitForSeconds, true));//wait then display the next dialogue
                        //PLAY THE MEMORY 1 and then next dialogue
                        playMemory1();//fire this event so the VideoManager can play a video
                        pictureFrameRecognizedTwice = true;
                    }
                    break;
                    
                case "Pen":
                    //7---------------------Look at the pen to make display dialogue -> then make the pen drop on the floor
                    if (canPenBeFound && wasAfterM1ConversationPlayed && !wasDialogue7Displayed)//after dialogue num 6
                    {
                        Debug.Log("First time pen was dropped");
                        dManage.DisplayNextSentence();
                        wasDialogue7Displayed = true;
                        playMemory2();
                        //Throw down the pen -> event...
                        //makeAllyFindFirstLetterPiece method below
                    }
                    break;
            }
        }


        //5,6---------------------Memory 1 played now switch between dialogues
        if (pictureFrameRecognizedTwice && !wasAfterM1ConversationPlayed && canAfterM1ConversationStart)
        {
            Debug.Log("Attempting to run pen Code");

            StartCoroutine(CountdownToStart(waitForSeconds * 2, true));//num 5
            wasAfterM1ConversationPlayed = true;

            StartCoroutine(CountdownToStart(waitForSeconds * 2, true));//num 6
            canPenBeFound = true;//now go and find the pen
        }

        //for 11------------------------ check if light switch = off, curtain = closed
        if (canDisplay11)
        {
            if (dManage.curtainClosed && dManage.lightsOut)
            {
                actionsBeforeMenory3();//walk NPC, change dialogue
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }


    //-------------------------------------------------------------------------------------------------
    //Wait then next dialogue
    //-------------------------------------------------------------------------------------------------
    IEnumerator CountdownToStart(int time, bool nextDialogue)
    {
        while(time > 0)//Count down timer
        {
            yield return new WaitForSeconds(1f);//wait for x seconds and come to this code later

            time--;
            Debug.Log(time);
        }
        Debug.Log("Waiting done.");

        if (nextDialogue)
        {
            //display another dialogue
            dManage.DisplayNextSentence();
        }
    }

    //------------------------------------------------------------------------------------------------
    //What to do before M3 should be played
    //------------------------------------------------------------------------------------------------
    private void actionsBeforeMenory3()
    {
        if (!isActionsBeforeMemory3Done)
        {
            Debug.Log("Dialogue 11");

            //11--------------------Memory 3 was played
            GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().DisplayNextSentence();
            StartCoroutine(CountdownToStart(waitForSeconds, false));
            //make NPC walk to poster
            GameObject.FindGameObjectWithTag("NPC").GetComponent<NPC>().SetDest(GameObject.FindGameObjectWithTag("Poster").transform.position);
            StartCoroutine(CountdownToStart(waitForSeconds, false));

            //OBJECT TRIGGER TO TELL VIDEOmanager TO PLAY M3
            //tell VideoManager to play memory 3 -> event

            isActionsBeforeMemory3Done = true;
        }
    }

    //-------------------------------------------------------------------------------------------------
    //BASED ON EVENTS
    //-------------------------------------------------------------------------------------------------
    //---------------------------------------------------------
    //Displaying dialogues after M1 was played
    private void afterM1ConversationCanStart()
    {
        canAfterM1ConversationStart = true;
        Debug.Log("canAfterM1ConversationStart " + canAfterM1ConversationStart);
    }
    //---------------------------------------------------------
    //NPC walks to pen/table
    private void makeAllyFindFirstLetterPiece()
    {
        if (wasDialogue7Displayed)
        {
            //8--------------------Make the Npc walk to the table and find the first letter piece
            GameObject.FindGameObjectWithTag("NPC").GetComponent<NPC>().SetDest(GameObject.FindGameObjectWithTag("Pen").transform.position);
            //display dialogue 8 once npc is there
            GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().DisplayNextSentence();
        }
    }
    //---------------------------------------------------------
    //Displaying dialogues after M2 was played
    private void afterM2PlayedDialogues()
    {
        //9--------------------Ally foun the first letter piece
        if (!wasDialogue9Display)
        {
            GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().DisplayNextSentence();
            wasDialogue9Display = true;
            
            //10--------------------Specter replies to Ally about the second piece of the letter
            StartCoroutine(CountdownToStart(waitForSeconds, true));//wait then display the next dialogue
            //then you have to turn off the light + close curtain

            //dialogue 11 can be displayed now -> Update
            canDisplay11 = true;
        }
    }
}
