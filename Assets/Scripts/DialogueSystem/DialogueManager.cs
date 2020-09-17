using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //For displaying dialogue text as UI elements on the screen
    public Text dialogueText;

    //FIFO collection of sentences
    private Queue<string> sentences;//keeping track of all of the sentences in our current dialogue

    //----------------------------------------------------------------------------------------
    //Events listener
    //----------------------------------------------------------------------------------------
    private void Awake()
    {
        VideoManager.m1Playing += HideDialogue;
        VideoManager.m1DonePlaying += DisplayDialogue;
    }
    private void OnDestroy()
    {
        VideoManager.m1Playing -= HideDialogue;
        VideoManager.m1DonePlaying -= DisplayDialogue;
    }


    void Start()
    {
        sentences = new Queue<string>();
    }
    //----------------------------------------------------------------------------------------
    //Start dialogue
    //----------------------------------------------------------------------------------------
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with ");

        //clear all the sentences from pervious conversations
        sentences.Clear();
        //go throught all of the strings in our Dialogue.sentences array
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);//put the sentences in the queue
        }

        //Make the canvas of DialogueSystem visibel
        GameObject.FindGameObjectWithTag("DialogueSystem").GetComponentInChildren<Canvas>().enabled = true;

        //display the sentences in the beginnig of the queue
        DisplayNextSentence();
    }

    //----------------------------------------------------------------------------------------
    //Next dialogue
    //----------------------------------------------------------------------------------------
    public void DisplayNextSentence()
    {
        //TRIGGER HERE---------------------------------------------------

        //check if there are anymore sentences in the queue
        if (sentences.Count == 0)//the end of queue reached
        {
            EndDialogue();
            return;//return out of the rest of this function
        }
        //in case we have more sentences to say
        //get the first sentence in the queue
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;//display the current sentence on the UI
    }

    //----------------------------------------------------------------------------------------
    //End dialogue
    //----------------------------------------------------------------------------------------
    void EndDialogue()
    {
        Debug.Log("End of conversation");
    }

    //----------------------------------------------------------------------------------------
    //Hide dialogue canvas
    //----------------------------------------------------------------------------------------
    public void HideDialogue()
    {
        GameObject.FindGameObjectWithTag("DialogueSystem").GetComponentInChildren<Canvas>().enabled = false;
    }
    //----------------------------------------------------------------------------------------
    //Display dialogue canvas
    //----------------------------------------------------------------------------------------
    public void DisplayDialogue()
    {
        GameObject.FindGameObjectWithTag("DialogueSystem").GetComponentInChildren<Canvas>().enabled = true;
    }
}
