using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRecognition : MonoBehaviour
{
    public int rayDistance;
    RaycastHit hitInfo;
    public static event Action PickUpRecognized;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * rayDistance, Color.green);
        
        //create ray to recognize the pickUps
        if(Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo, rayDistance))
        {
            if(hitInfo.collider.tag == "PickUp")
            {
                Debug.Log("pickup recognized!");
                PickUpRecognized();//fire this event
            }
        }
    }
}
