using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //Has to be on an object
    //Will allow us to trigger a new dialogue

    public Dialogue dialogue;//creating a Dialogue object that has the name and senteces in it

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }
}
