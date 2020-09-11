using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class PhysicsMovement : MonoBehaviour
{
    Rigidbody rb; //for Force, Velocity, position control

    public float movementSpeed = 3f;
    public float sprintSpeed = 6f;

    public float movementForce = 100f;
    public float sprintForce = 200f;

    public float jumpHeight = 5f;
    public float maxDistanceGroundRay = 30f; //Length of the ray for ground check

    private float _speed;
    private float _force;
    private bool _isJumpPressed;

    private bool _canSprint = true; //to be able to sprint before we pickup a box for the first time

    public bool canJump = true;
    public bool sprintEnabled = true;
    public bool canSprintWithPickUp = false;

    //START OF GAME restrictions
    public bool canMove = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();//rigidbody connected to this GameObject

        //PickUp-----------------------------------------------------------
        //Subscribing!
        PickUp.OnHoldingPickup += sprintDisable; //check out if a pickup up was picked up
        PickUp.OnNoPickup += sprintEnable;
        GameManager.playerCanMove += movementEnabled;
    }
    private void OnDestroy()
    {
        //PickUp-----------------------------------------------------------
        //Unsubscribing!
        PickUp.OnHoldingPickup -= sprintEnable;
        PickUp.OnNoPickup -= sprintEnable;
        GameManager.playerCanMove -= movementEnabled;
    }

    private void Update()
    {
        //Jumping-----------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            _isJumpPressed = true;
        }
        //*?
        //Jumping in Update to prevent different nonintended "double" jump because of fixed update frames would make rb jump
        //more times because there can be more fixedUpdate frames within one (normal) Update frame, or non fixedUpdate in one Update.
    }

    void FixedUpdate() //Fixed = we are operating with Unity physics engine
    {
        //for checking if key pressed to move
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * maxDistanceGroundRay, Color.magenta);
       
        //Jumping-----------------------------------------------------------
        if (_isJumpPressed && checkGrounded()) //if not already jumping & on the ground
        {
            rb.velocity += new Vector3(0, jumpHeight, 0);
            Debug.Log("Jumping");
        }

        //Sprinting---------------------------------------------------------
        if (Input.GetKey(KeyCode.LeftShift) && _canSprint == true && sprintEnabled == true)
        {
            Debug.Log("sprinting");
            _speed = sprintSpeed;
            _force = sprintForce;
        }
        else
        {
            _speed = movementSpeed;
            _force = movementForce;
        }

        //Walking-----------------------------------------------------------
        if (canMove)//from LookAtRecognition event > GameManager method, event > Physics movement bool
        {
            transform.Translate(moveVector * _speed * Time.deltaTime);
        }
       

        //reseting variables back at the end of the FixedUpdate
        _isJumpPressed = false;
    }

    //----------------------------------------------------------------------
    //CAN SPRINT?
    //based on EVENTS we are subscribed to -> holding pickup?
    //----------------------------------------------------------------------
    private void sprintEnable()//when dropping pickup
    {
        _canSprint = true;         
    }
    private void sprintDisable()//when holding an object
    {
        if(canSprintWithPickUp == false)
        {
            _canSprint = false;
        }        
    }

    //----------------------------------------------------------------------
    //GROUND CHECK FOR JUMPING (velocity case)
    //check if a player is on the ground by raycast hit info
    //----------------------------------------------------------------------
    private bool checkGrounded()
    {
        Vector3 origin = transform.position; //the origin of the ray
        Vector3 direction = new Vector3(0, -1, 0); //poiting the ray down
        RaycastHit hitInfo; //to know what/who is "hitting" the ray
        
        Debug.DrawRay(origin, direction * maxDistanceGroundRay, Color.magenta);

        if(Physics.Raycast(origin, direction, out hitInfo, maxDistanceGroundRay))
        {
            //what to do when the ray is hitting something
            Debug.Log("CheckGrounded = true");
            return true; //if it is touching something return true
        }
        Debug.Log("CheckGrounded = false");
        return false;
    }
    //----------------------------------------------------------------------
    //Can player move? (start of the game player cannot move, just look around)
    //----------------------------------------------------------------------
    private void movementEnabled()
    {
        canMove = true;
    }
}
