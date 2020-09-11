using System;//for events
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LookingAtRecognition : MonoBehaviour
{
    
    RaycastHit hitInfo;
    public float interactionDistance = 300f;
    public static event Action bloodRecognized;//for dialogue
    public static event Action npcRecognized;//for dialogue
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
                //.Log("Blood recognized");
                bloodRecognized();//fire this event -> game manager listening
            }else
            if (hitInfo.collider.tag == "NPC")
            {
                //Debug.Log("NPC recognized");
                npcRecognized();//this event -> game manager method, event > physics movement bool
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
