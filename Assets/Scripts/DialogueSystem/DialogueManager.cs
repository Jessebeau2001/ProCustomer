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


    void Start()
    {
        sentences = new Queue<string>();
    }


    
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

        //display the sentences in the beginnig of the queue
        DisplayNextSentence();
    }

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

    void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
}
