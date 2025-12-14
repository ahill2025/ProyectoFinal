using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed, rotationSpeed, height, sprint;     // Variables containing velocities (m/s) and forces (N) 
    private Vector2 movementValue;      // Vector2 variable for x-z movement of 'Player 1' gameObject
    private bool contact;       // Boolean that will signal whether 'Player 1' is in contact with a surface or not (colliders)
    private float sprintValue = 1;      // Multiplier applied to movementValue to increase 'Player 1' movement speed
    private float lookValue;        // Value for rotation sensitivity

    // Task 5
    private Rigidbody _rigidbody;   // Used to obtain linearVelocity influenced by gravity 
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;      
        _rigidbody = GetComponent<Rigidbody>();         
    }
    public void OnJump(InputValue button)
    { if (contact) { _rigidbody.AddForce(0, height, 0); } }     // if 'contact' is 'true', apply a force with magnitude 'height' in y-direction
    
    private void OnCollisionStay(Collision collision)   // Continuously detects if two colliders are in contact.  
    { contact = true; } // Assigns 'true' to 'contact', signaling player is in contact

    private void OnCollisionExit(Collision collision)   // Triggered when objects colliding are not anymore.  Player and surfaces after jumping, falling off, etc. 
    { contact = false; }    // Assigns 'false' to 'contact', signaling player is not in contact to collider

    public void OnMove(InputValue value)
    { movementValue = value.Get<Vector2>() * Speed; }   // 
    
    public void OnSprint(InputValue button)     // sprintValue with value 1 is being multiplied every frame in Update().  When sprint button is pressed, the value is modified to increase speed.
    {
        if (button.isPressed){ sprintValue = sprint;}       // if OnSprint() assigned button is pressed, 'sprint' is assigned to 'sprintValue' and is multiplied in Update()
        else{sprintValue = 1;}      // When button is released, sprintValue returns to 1
    }
    public void OnLook(InputValue value)

    { lookValue = value.Get<Vector2>().x * rotationSpeed; }
    void Update()
    {
        Debug.Log("Player Update Running");
        transform.Translate(movementValue.x * sprintValue * Time.deltaTime,0,movementValue.y * sprintValue * Time.deltaTime);
        transform.Rotate(0, lookValue * Time.deltaTime, 0);
        // If using transform.Translate, have 'Interpolate' to None and 'Collision' to Discrete
        // _rigidbody.AddRelativeForce(movementValue.x * Time.deltaTime,0, movementValue.y * Time.deltaTime);
        // _rigidbody.AddRelativeTorque(0, lookValue * Time.deltaTime, 0);
        // If using AddRelativeForce/Torque, use Interpolate and Continuous Dynamic
    }
}






















    // public void OnSprint(InputValue value)
    // {
    //     if (value.Get<float>() == 1)   // Activated when 'Shift' key is pressed
    //     {
    //         sprintValue = sprint;
    //     }

    //     if (value.Get<float>() == 0)    // Activated when 'Shift' key is not pressed
    //     {
    //         sprintValue = 1;
    //     }
    // }