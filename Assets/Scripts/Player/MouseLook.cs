using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;//reference to player object to be able to rotate it around Y axis (this script in MainCamera)
    float xRotation;//reference to main camera to rotate it around Y axis

    void Start()
    {        
        Cursor.lockState = CursorLockMode.Locked;//to hide the cursor so we won't move it out of the window (= annoying)
    }

    
    void Update()
    {
        //getting input from mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;//the amount of time that passed, since the last time we called the Update method
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //ROTATION Y -> right/left
        playerBody.transform.Rotate(Vector3.up * mouseX);//playerBody around Y axis (up) with the mouseX input

        //ROTATION X -> up/down
        xRotation -= mouseY;//minus to not have the rotation flipped upside down
        
        //restricting the value of this variable from-to
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //to make sure we can't rotate the whole player (restrict the movement to just loop up and down, not behind = 180 deg)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);//actually rotate it
    }
}
