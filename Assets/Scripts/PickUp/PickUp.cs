using System;
using UnityEngine;


public class PickUp : MonoBehaviour
{
    public NPC dude;
    public Transform theDestination; //the empty PickUpDestination object of a player to be able to pickUp objects
    public static event Action OnHoldingPickup;
    public static event Action OnNoPickup;
    public float maxPickupDistance = 2;//pickupDestination x pickup
    public bool shouldItemShrink = true;//if an item should get smaller when picked up

    //player cant pick up stuff
    public bool canBePickeUp = false;

    //--------------------------------------------------------------------------------------------
    // PICK UP object
    //--------------------------------------------------------------------------------------------
    private void OnMouseDown()
    {
        float pickupDistance = Vector3.Distance(theDestination.transform.position, transform.position);
        if (pickupDistance < maxPickupDistance)//check if close enough to the pickup
        {
            //npc walking 
            if (canBePickeUp)//no picking up anymore
            {
                //turn off the pickUp collider
                GetComponent<BoxCollider>().enabled = false;
                //turn of the gravity of the rigidbody
                GetComponent<Rigidbody>().useGravity = false;

                //change the position of the PickUp object
                //to the position of the PickUpDestination (empty) object player has in front of it
                this.transform.position = theDestination.position;

                if (this.gameObject.tag != "Box" && shouldItemShrink == true)//to prevent bug with bigger and bigger boxes
                {
                    this.transform.localScale /= 2;//make the pickup smaller
                }

                this.transform.parent = GameObject.Find("PickUpDestination").transform;//make this PickedUp object a child of the PickupDestination empty object

                OnHoldingPickup?.Invoke();//Fireing this event -> PhysicsMovement > can not sprint anymore
                
                
                //OnObjectClicked();
                //objectClicked?.Invoke();//fire event
            }
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
            if (canBePickeUp)//no picking up anymore
            {
                //turn on the box collider back on again
                GetComponent<BoxCollider>().enabled = true;

                //make the PickUp an independent object again, without a parent
                this.transform.parent = null;

                if (this.gameObject.tag != "Box" && shouldItemShrink == true)
                {
                    this.transform.localScale *= 2;//make the pickup the original size
                }

                GetComponent<Rigidbody>().useGravity = true;//turn gravity back on
                OnPickup();

                //Fireing this event -> PhysicsMovement > can sprint again
                OnNoPickup?.Invoke();
            }
        }
    }

    void OnPickup() {
        //dude.Transl(new Vector3(0, 1, 0));
        Debug.Log("Function on question was called");
        dude.SetDest(transform.position);
    }

    //Why turning off the Box Collider?
    //to prevent deformation of the box shape, when for example you position a wall between a player and the box when having the pickup.
}
