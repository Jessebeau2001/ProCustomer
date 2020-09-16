using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//to show it in the inspector
public class Dialogue
{
    //Dialogue class
    //used as an object that we can pass into the DialogueManager whenever we want to start a new dialogue
    //This class will host all information that we need about a single dialogue

    [TextArea(3,20)]//to edit the text area of the sentences (in the inspector) how many lines min, max there can be
    public string[] sentences;//sentences we will load in our Queue

}
