using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using System;


public class PickUp : MonoBehaviour
{
    public Transform theDestination; //the empty PickUpDestination object of a player to be able to pickUp objects
    public static event Action OnHoldinPickup;
    public static event Action OnNoPickup;
    public float maxPickupDistance = 2;//pickupDestination x pickup

    //--------------------------------------------------------------------------------------------
    // PICK UP object
    //--------------------------------------------------------------------------------------------
    private void OnMouseDown()
    {
        float pickupDistance = Vector3.Distance(theDestination.transform.position, transform.position);
        if (pickupDistance < maxPickupDistance)//check if close enough to the pickup
        {
            //turn off the pickUp collider
            GetComponent<BoxCollider>().enabled = false;
            //turn of the gravity of the rigidbody
            GetComponent<Rigidbody>().useGravity = false;
            
            //change the position of the PickUp object
            //to the position of the PickUpDestination (empty) object player has in front of it
            this.transform.position = theDestination.position;

            if(this.gameObject.tag != "Box")//to prevent bug with bigger and bigger boxes
            {
                this.transform.localScale /= 2;//make the pickup smaller
            }
                                           
            this.transform.parent = GameObject.Find("PickUpDestination").transform;//make this PickedUp object a child of the PickupDestination empty object

            OnHoldinPickup?.Invoke();//Fireing this event -> PhysicsMovement > can not sprint anymore
        }
    }

    //--------------------------------------------------------------------------------------------
    // DROP object
    //--------------------------------------------------------------------------------------------
    private void OnMouseUp()
    {
        float pickupDistance = Vector3.Distance(theDestination.transform.position, transform.position);
        if (pickupDistance < maxPickupDistance)
        {
            //turn on the box collider back on again
            GetComponent<BoxCollider>().enabled = true;
            
            //make the PickUp an independent object again, without a parent
            this.transform.parent = null;

            if (this.gameObject.tag != "Box")
            {
                this.transform.localScale *= 2;//make the pickup the original size
            }
            
            GetComponent<Rigidbody>().useGravity = true;//turn gravity back on

            //Fireing this event -> PhysicsMovement > can sprint again
            OnNoPickup?.Invoke();
        }
    }

    //Why turning off the Box Collider?
    //to prevent deformation of the box shape, when for example you position a wall between a player and the box when having the pickup.
}
