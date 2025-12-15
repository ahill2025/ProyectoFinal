// using System;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.Rendering;
// using UnityEngine.UI;

// public class PlayerMovement : MonoBehaviour
// {
//     [SerializeField]
//     private float Speed, rotationSpeed, height, sprint;     // Variables containing velocities (m/s) and forces (N) 
//     private Vector2 movementValue;      // Vector2 variable for x-z movement of 'Player 1' gameObject
//     private bool contact;       // Boolean that will signal whether 'Player 1' is in contact with a surface or not (colliders)
//     private float sprintValue = 1;      // Multiplier applied to movementValue to increase 'Player 1' movement speed
//     private float lookValue;        // Value for rotation sensitivity

//     // Task 5
//     private Rigidbody _rigidbody;   // Used to obtain linearVelocity influenced by gravity 
//     private void Awake()
//     {
//         Cursor.visible = false;
//         Cursor.lockState = CursorLockMode.Locked;      
//         _rigidbody = GetComponent<Rigidbody>();         
//     }
//     public void OnJump(InputValue button)
//     { 
//         if (contact) 
//         { 
//             _rigidbody.AddForce(0, height, 0); 
//         } 
//     }     // if 'contact' is 'true', apply a force with magnitude 'height' in y-direction

    
//     private void OnCollisionStay(Collision collision)   // Continuously detects if two colliders are in contact.  
//     { contact = true; } // Assigns 'true' to 'contact', signaling player is in contact

//     private void OnCollisionExit(Collision collision)   // Triggered when objects colliding are not anymore.  Player and surfaces after jumping, falling off, etc. 
//     { contact = false; }    // Assigns 'false' to 'contact', signaling player is not in contact to collider

//     public void OnMove(InputValue value)
//     { movementValue = value.Get<Vector2>() * Speed; }   // 
    
//     public void OnSprint(InputValue button)     // sprintValue with value 1 is being multiplied every frame in Update().  When sprint button is pressed, the value is modified to increase speed.
//     {
//         if (button.isPressed){ sprintValue = sprint;}       // if OnSprint() assigned button is pressed, 'sprint' is assigned to 'sprintValue' and is multiplied in Update()
//         else{sprintValue = 1;}      // When button is released, sprintValue returns to 1
//     }
//     public void OnLook(InputValue value)

//     { lookValue = value.Get<Vector2>().x * rotationSpeed; }
//     void Update()
//     {
//         Debug.Log("Player Update Running");
//         transform.Translate(movementValue.x * sprintValue * Time.deltaTime,0,movementValue.y * sprintValue * Time.deltaTime);
//         transform.Rotate(0, lookValue * Time.deltaTime, 0);
//         // If using transform.Translate, have 'Interpolate' to None and 'Collision' to Discrete
//         // _rigidbody.AddRelativeForce(movementValue.x * Time.deltaTime,0, movementValue.y * Time.deltaTime);
//         // _rigidbody.AddRelativeTorque(0, lookValue * Time.deltaTime, 0);
//         // If using AddRelativeForce/Torque, use Interpolate and Continuous Dynamic
//     }
// }



// using System;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.Rendering;
// using UnityEngine.UI;

// public class PlayerMovement : MonoBehaviour
// {
//     [SerializeField]
//     private float Speed, rotationSpeed, height, sprint;     // Variables containing velocities (m/s) and forces (N)
//     private Vector2 movementValue;      // Vector2 variable for x-z movement of 'Player 1' gameObject
//     private bool contact;       // Boolean that will signal whether 'Player 1' is in contact with a surface or not (colliders)
//     private float sprintValue = 1;      // Multiplier applied to movementValue to increase 'Player 1' movement speed
//     private float lookValue;        // Value for rotation sensitivity

//     // Animator
//     [SerializeField] private Animator animator;  // Drag your Player Animator here (Or leave empty to auto-find)
//     private bool isSprinting;                   // Tracks sprint state for Animator
//     private Vector2 movementInputRaw;           // Raw input (-1..1) for Animator blend tree

//     // Task 5
//     private Rigidbody _rigidbody;   // Used to obtain linearVelocity influenced by gravity
//     private void Awake()
//     {
//         Cursor.visible = false;
//         Cursor.lockState = CursorLockMode.Locked;
//         _rigidbody = GetComponent<Rigidbody>();

//         if (animator == null)
//         {
//             animator = GetComponentInChildren<Animator>();
//         }
//     }

//     public void OnJump(InputValue button)
//     {
//         if (contact)
//         {
//             _rigidbody.AddForce(0, height, 0);

//             // Animator: Jump trigger
//             if (animator != null)
//             {
//                 animator.SetTrigger("Jump");
//             }
//         }
//     }     // if 'contact' is 'true', apply a force with magnitude 'height' in y-direction

//     private void OnCollisionStay(Collision collision)   // Continuously detects if two colliders are in contact.
//     { contact = true; } // Assigns 'true' to 'contact', signaling player is in contact

//     private void OnCollisionExit(Collision collision)   // Triggered when objects colliding are not anymore.  Player and surfaces after jumping, falling off, etc.
//     { contact = false; }    // Assigns 'false' to 'contact', signaling player is not in contact to collider

//     public void OnMove(InputValue value)
//     {
//         movementInputRaw = value.Get<Vector2>();        // Raw input for Animator (MoveX/MoveY)
//         movementValue = movementInputRaw * Speed;       //
//     }   //

//     public void OnSprint(InputValue button)     // sprintValue with value 1 is being multiplied every frame in Update().  When sprint button is pressed, the value is modified to increase speed.
//     {
//         if (button.isPressed)
//         {
//             sprintValue = sprint;      // if OnSprint() assigned button is pressed, 'sprint' is assigned to 'sprintValue' and is multiplied in Update()
//             isSprinting = true;       // Animator sprint bool
//         }
//         else
//         {
//             sprintValue = 1;          // When button is released, sprintValue returns to 1
//             isSprinting = false;      // Animator sprint bool
//         }
//     }

//     public void OnLook(InputValue value)
//     { lookValue = value.Get<Vector2>().x * rotationSpeed; }

//     void Update()
//     {
//         Debug.Log("Player Update Running");

//         transform.Translate(movementValue.x * sprintValue * Time.deltaTime, 0, movementValue.y * sprintValue * Time.deltaTime);
//         transform.Rotate(0, lookValue * Time.deltaTime, 0);

//         // Animator: Drive locomotion parameters (X = strafe, Y = forward in your 2D blend tree)
//         if (animator != null)
//         {
//             animator.SetFloat("MoveX", movementInputRaw.x);
//             animator.SetFloat("MoveY", movementInputRaw.y);

//             animator.SetBool("IsSprinting", isSprinting);
//             animator.SetBool("IsGrounded", contact);
//         }

//         // If using transform.Translate, have 'Interpolate' to None and 'Collision' to Discrete
//         // _rigidbody.AddRelativeForce(movementValue.x * Time.deltaTime,0, movementValue.y * Time.deltaTime);
//         // _rigidbody.AddRelativeTorque(0, lookValue * Time.deltaTime, 0);
//         // If using AddRelativeForce/Torque, use Interpolate and Continuous Dynamic
//     }
// }








// using System;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.Rendering;
// using UnityEngine.UI;

// public class PlayerMovement : MonoBehaviour
// {
//     [SerializeField]
//     private float Speed, rotationSpeed, height, sprint;     // Variables containing velocities (m/s) and forces (N)
//     private Vector2 movementValue;      // Vector2 variable for x-z movement of 'Player 1' gameObject
//     private bool contact;       // Boolean that will signal whether 'Player 1' is in contact with a surface or not (colliders)
//     private float sprintValue = 1;      // Multiplier applied to movementValue to increase 'Player 1' movement speed
//     private float lookValue;        // Value for rotation sensitivity

//     // Animator
//     [SerializeField] private Animator animator;  // Drag your Player Animator here (Or leave empty to auto-find)
//     private bool isSprinting;                   // Tracks sprint state for Animator
//     private Vector2 movementInputRaw;           // Raw input (-1..1) for Animator blend tree

//     // Task 5
//     private Rigidbody _rigidbody;   // Used to obtain linearVelocity influenced by gravity
//     private void Awake()
//     {
//         Cursor.visible = false;
//         Cursor.lockState = CursorLockMode.Locked;
//         _rigidbody = GetComponent<Rigidbody>();

//         if (animator == null)
//         {
//             animator = GetComponentInChildren<Animator>();
//         }
//     }

//     public void OnJump(InputValue button)
//     {
//         if (contact)
//         {
//             _rigidbody.AddForce(0, height, 0);

//             // Animator: Jump trigger
//             if (animator != null)
//             {
//                 animator.SetTrigger("Jump");
//             }
//         }
//     }     // if 'contact' is 'true', apply a force with magnitude 'height' in y-direction

//     private void OnCollisionStay(Collision collision)   // Continuously detects if two colliders are in contact.
//     { contact = true; } // Assigns 'true' to 'contact', signaling player is in contact

//     private void OnCollisionExit(Collision collision)   // Triggered when objects colliding are not anymore.  Player and surfaces after jumping, falling off, etc.
//     { contact = false; }    // Assigns 'false' to 'contact', signaling player is not in contact to collider

//     public void OnMove(InputValue value)
//     {
//         movementInputRaw = value.Get<Vector2>();        // Raw input for Animator (MoveX/MoveY)
//         movementValue = movementInputRaw * Speed;       //
//     }   //

//     public void OnSprint(InputValue button)     // sprintValue with value 1 is being multiplied every frame in Update().  When sprint button is pressed, the value is modified to increase speed.
//     {
//         if (button.isPressed)
//         {
//             sprintValue = sprint;      // if OnSprint() assigned button is pressed, 'sprint' is assigned to 'sprintValue' and is multiplied in Update()
//             isSprinting = true;       // Animator sprint bool
//         }
//         else
//         {
//             sprintValue = 1;          // When button is released, sprintValue returns to 1
//             isSprinting = false;      // Animator sprint bool
//         }
//     }

//     public void OnLook(InputValue value)
//     { lookValue = value.Get<Vector2>().x * rotationSpeed; }

//     void Update()
//     {
//         Debug.Log("Player Update Running");

//         transform.Translate(movementValue.x * sprintValue * Time.deltaTime, 0, movementValue.y * sprintValue * Time.deltaTime);
//         transform.Rotate(0, lookValue * Time.deltaTime, 0);

//         // Sprint should only be "true" if you're sprinting AND actually moving
//         bool isMoving = movementInputRaw.sqrMagnitude > 0.01f;

//         // Animator: Drive locomotion parameters (X = strafe, Y = forward in your 2D blend tree)
//         if (animator != null)
//         {
//             animator.SetFloat("MoveX", movementInputRaw.x);
//             animator.SetFloat("MoveY", movementInputRaw.y);

//             animator.SetBool("IsSprinting", isSprinting && isMoving);
//             animator.SetBool("IsGrounded", contact);
//         }

//         // If using transform.Translate, have 'Interpolate' to None and 'Collision' to Discrete
//         // _rigidbody.AddRelativeForce(movementValue.x * Time.deltaTime,0, movementValue.y * Time.deltaTime);
//         // _rigidbody.AddRelativeTorque(0, lookValue * Time.deltaTime, 0);
//         // If using AddRelativeForce/Torque, use Interpolate and Continuous Dynamic
//     }
// }



// using System;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.Rendering;
// using UnityEngine.UI;

// public class PlayerMovement : MonoBehaviour
// {
//     [SerializeField]
//     private float Speed, rotationSpeed, height, sprint;     // Variables containing velocities (m/s) and forces (N)
//     private Vector2 movementValue;      // Vector2 variable for x-z movement of 'Player 1' gameObject
//     private bool contact;       // Boolean that will signal whether 'Player 1' is in contact with a surface or not (colliders)
//     private float sprintValue = 1;      // Multiplier applied to movementValue to increase 'Player 1' movement speed
//     private float lookValue;        // Value for rotation sensitivity

//     // Animator
//     [SerializeField] private Animator animator;  // Drag your Player Animator here (Or leave empty to auto-find)
//     private bool isSprinting;                   // Tracks sprint state for Animator
//     private Vector2 movementInputRaw;           // Raw input (-1..1) for Animator blend tree

//     // Task 5
//     private Rigidbody _rigidbody;   // Used to obtain linearVelocity influenced by gravity
//     private void Awake()
//     {
//         Cursor.visible = false;
//         Cursor.lockState = CursorLockMode.Locked;
//         _rigidbody = GetComponent<Rigidbody>();

//         if (animator == null)
//         {
//             animator = GetComponentInChildren<Animator>();
//         }
//     }

//     public void OnJump(InputValue button)
//     {
//         if (contact)
//         {
//             contact = false; // NEW: Immediately mark airborne so Animator can switch to Jump right away

//             _rigidbody.AddForce(0, height, 0);

//             // Animator: Jump trigger
//             if (animator != null)
//             {
//                 animator.SetTrigger("Jump");
//             }
//         }
//     }     // if 'contact' is 'true', apply a force with magnitude 'height' in y-direction

//     private void OnCollisionStay(Collision collision)   // Continuously detects if two colliders are in contact.
//     { contact = true; } // Assigns 'true' to 'contact', signaling player is in contact

//     private void OnCollisionExit(Collision collision)   // Triggered when objects colliding are not anymore.  Player and surfaces after jumping, falling off, etc.
//     { contact = false; }    // Assigns 'false' to 'contact', signaling player is not in contact to collider

//     public void OnMove(InputValue value)
//     {
//         movementInputRaw = value.Get<Vector2>();        // Raw input for Animator (MoveX/MoveY)
//         movementValue = movementInputRaw * Speed;       //
//     }   //

//     public void OnSprint(InputValue button)     // sprintValue with value 1 is being multiplied every frame in Update().  When sprint button is pressed, the value is modified to increase speed.
//     {
//         if (button.isPressed)
//         {
//             sprintValue = sprint;      // if OnSprint() assigned button is pressed, 'sprint' is assigned to 'sprintValue' and is multiplied in Update()
//             isSprinting = true;       // Animator sprint bool
//         }
//         else
//         {
//             sprintValue = 1;          // When button is released, sprintValue returns to 1
//             isSprinting = false;      // Animator sprint bool
//         }
//     }

//     public void OnLook(InputValue value)
//     { lookValue = value.Get<Vector2>().x * rotationSpeed; }

//     void Update()
//     {
//         Debug.Log("Player Update Running");

//         transform.Translate(movementValue.x * sprintValue * Time.deltaTime, 0, movementValue.y * sprintValue * Time.deltaTime);
//         transform.Rotate(0, lookValue * Time.deltaTime, 0);

//         // Sprint should only be "true" if you're sprinting AND actually moving
//         bool isMoving = movementInputRaw.sqrMagnitude > 0.01f;

//         // Animator: Drive locomotion parameters (X = strafe, Y = forward in your 2D blend tree)
//         if (animator != null)
//         {
//             animator.SetFloat("MoveX", movementInputRaw.x);
//             animator.SetFloat("MoveY", movementInputRaw.y);

//             animator.SetBool("IsSprinting", isSprinting && isMoving);
//             animator.SetBool("IsGrounded", contact);
//         }

//         // If using transform.Translate, have 'Interpolate' to None and 'Collision' to Discrete
//         // _rigidbody.AddRelativeForce(movementValue.x * Time.deltaTime,0, movementValue.y * Time.deltaTime);
//         // _rigidbody.AddRelativeTorque(0, lookValue * Time.deltaTime, 0);
//         // If using AddRelativeForce/Torque, use Interpolate and Continuous Dynamic
//     }
// }



// using System;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.Rendering;
// using UnityEngine.UI;

// public class PlayerMovement : MonoBehaviour
// {
//     [SerializeField]
//     private float Speed, rotationSpeed, height, sprint;     // Variables containing velocities (m/s) and forces (N)
//     private Vector2 movementValue;      // Vector2 variable for x-z movement of 'Player 1' gameObject
//     private bool contact;       // Boolean that will signal whether 'Player 1' is in contact with a surface or not (colliders)
//     private float sprintValue = 1;      // Multiplier applied to movementValue to increase 'Player 1' movement speed
//     private float lookValue;        // Value for rotation sensitivity

//     // Animator
//     [SerializeField] private Animator animator;  // Drag your Player Animator here (Or leave empty to auto-find)
//     private bool isSprinting;                   // Tracks sprint state for Animator
//     private Vector2 movementInputRaw;           // Raw input (-1..1) for Animator blend tree

//     // Task 5
//     private Rigidbody _rigidbody;   // Used to obtain linearVelocity influenced by gravity
//     private void Awake()
//     {
//         Cursor.visible = false;
//         Cursor.lockState = CursorLockMode.Locked;
//         _rigidbody = GetComponent<Rigidbody>();

//         if (animator == null)
//         {
//             animator = GetComponentInChildren<Animator>();
//         }
//     }

//     public void OnJump(InputValue button)
//     {
//         if (contact)
//         {
//             _rigidbody.AddForce(0, height, 0, ForceMode.Impulse);

//             // Animator: Jump trigger
//             if (animator != null)
//             {
//                 animator.SetTrigger("Jump");
//             }
//         }
//     }     // if 'contact' is 'true', apply a force with magnitude 'height' in y-direction

//     private void OnCollisionStay(Collision collision)   // Continuously detects if two colliders are in contact.
//     { contact = true; } // Assigns 'true' to 'contact', signaling player is in contact

//     private void OnCollisionExit(Collision collision)   // Triggered when objects colliding are not anymore.  Player and surfaces after jumping, falling off, etc.
//     { contact = false; }    // Assigns 'false' to 'contact', signaling player is not in contact to collider

//     public void OnMove(InputValue value)
//     {
//         movementInputRaw = value.Get<Vector2>();        // Raw input for Animator (MoveX/MoveY)
//         movementValue = movementInputRaw * Speed;       //
//     }   //

//     public void OnSprint(InputValue button)     // sprintValue with value 1 is being multiplied every frame in Update().  When sprint button is pressed, the value is modified to increase speed.
//     {
//         if (button.isPressed)
//         {
//             sprintValue = sprint;      // if OnSprint() assigned button is pressed, 'sprint' is assigned to 'sprintValue' and is multiplied in Update()
//             isSprinting = true;       // Animator sprint bool
//         }
//         else
//         {
//             sprintValue = 1;          // When button is released, sprintValue returns to 1
//             isSprinting = false;      // Animator sprint bool
//         }
//     }

//     public void OnLook(InputValue value)
//     { lookValue = value.Get<Vector2>().x * rotationSpeed; }

//     void Update()
//     {
//         Debug.Log("Player Update Running");

//         transform.Translate(movementValue.x * sprintValue * Time.deltaTime, 0, movementValue.y * sprintValue * Time.deltaTime);
//         transform.Rotate(0, lookValue * Time.deltaTime, 0);

//         // Sprint should only be "true" if you're sprinting AND actually moving
//         bool isMoving = movementInputRaw.sqrMagnitude > 0.01f;

//         // Animator: Drive locomotion parameters (X = strafe, Y = forward in your 2D blend tree)
//         if (animator != null)
//         {
//             animator.SetFloat("MoveX", movementInputRaw.x);
//             animator.SetFloat("MoveY", movementInputRaw.y);

//             animator.SetBool("IsSprinting", isSprinting && isMoving);
//             animator.SetBool("IsGrounded", contact);
//         }

//         // If using transform.Translate, have 'Interpolate' to None and 'Collision' to Discrete
//         // _rigidbody.AddRelativeForce(movementValue.x * Time.deltaTime,0, movementValue.y * Time.deltaTime);
//         // _rigidbody.AddRelativeTorque(0, lookValue * Time.deltaTime, 0);
//         // If using AddRelativeForce/Torque, use Interpolate and Continuous Dynamic
//     }
// }





using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed, rotationSpeed, height, sprint;

    private Vector2 movementValue;
    private bool contact;
    private float sprintValue = 1;
    private float lookValue;

    // Animator
    [SerializeField] private Animator animator;
    private bool isSprinting;
    private Vector2 movementInputRaw;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _rigidbody = GetComponent<Rigidbody>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    // ORIGINAL jump logic + animation trigger
    public void OnJump(InputValue button)
    {
        if (contact)
        {
            _rigidbody.AddForce(0, height, 0);

            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        contact = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        contact = false;
    }

    public void OnMove(InputValue value)
    {
        movementInputRaw = value.Get<Vector2>();
        movementValue = movementInputRaw * Speed;
    }

    public void OnSprint(InputValue button)
    {
        if (button.isPressed)
        {
            sprintValue = sprint;
            isSprinting = true;
        }
        else
        {
            sprintValue = 1;
            isSprinting = false;
        }
    }

    public void OnLook(InputValue value)
    {
        lookValue = value.Get<Vector2>().x * rotationSpeed;
    }

    void Update()
    {
        transform.Translate(
            movementValue.x * sprintValue * Time.deltaTime,
            0,
            movementValue.y * sprintValue * Time.deltaTime
        );

        transform.Rotate(0, lookValue * Time.deltaTime, 0);

        bool isMoving = movementInputRaw.sqrMagnitude > 0.01f;

        if (animator != null)
        {
            animator.SetFloat("MoveX", movementInputRaw.x);
            animator.SetFloat("MoveY", movementInputRaw.y);
            animator.SetBool("IsSprinting", isSprinting && isMoving);
            animator.SetBool("IsGrounded", contact);
        }
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





//     using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerMovement : MonoBehaviour
// {
//     [SerializeField] private float Speed, rotationSpeed, height, sprint;

//     // Ground Check
//     [SerializeField] private float groundCheckRadius = 0.25f;
//     [SerializeField] private float groundCheckDistance = 0.35f;
//     [SerializeField] private LayerMask groundMask = ~0; // Everything by default

//     private Vector2 movementInputRaw;
//     private Vector2 movementValue;
//     private float sprintValue = 1f;
//     private float lookValue;

//     private bool contact;
//     private bool isSprinting;

//     [SerializeField] private Animator animator;

//     private Rigidbody _rigidbody;

//     private void Awake()
//     {
//         Cursor.visible = false;
//         Cursor.lockState = CursorLockMode.Locked;

//         _rigidbody = GetComponent<Rigidbody>();

//         if (animator == null)
//         {
//             animator = GetComponentInChildren<Animator>();
//         }
//     }

//     public void OnMove(InputValue value)
//     {
//         movementInputRaw = value.Get<Vector2>();
//         movementValue = movementInputRaw * Speed;
//     }

//     public void OnSprint(InputValue button)
//     {
//         if (button.isPressed)
//         {
//             sprintValue = sprint;
//             isSprinting = true;
//         }
//         else
//         {
//             sprintValue = 1f;
//             isSprinting = false;
//         }
//     }

//     public void OnLook(InputValue value)
//     {
//         lookValue = value.Get<Vector2>().x * rotationSpeed;
//     }

//     public void OnJump(InputValue button)
//     {
//         if (contact)
//         {
//             Vector3 v = _rigidbody.linearVelocity;
//             _rigidbody.linearVelocity = new Vector3(v.x, 0f, v.z);

//             _rigidbody.AddForce(Vector3.up * height, ForceMode.VelocityChange);

//             if (animator != null)
//             {
//                 animator.SetTrigger("Jump");
//             }
//         }
//     }

//     private void Update()
//     {
//         transform.Rotate(0f, lookValue * Time.deltaTime, 0f);

//         contact = Physics.SphereCast(
//             origin: transform.position + Vector3.up * 0.1f,
//             radius: groundCheckRadius,
//             direction: Vector3.down,
//             hitInfo: out _,
//             maxDistance: groundCheckDistance,
//             layerMask: groundMask,
//             queryTriggerInteraction: QueryTriggerInteraction.Ignore
//         );

//         bool isMoving = movementInputRaw.sqrMagnitude > 0.01f;

//         if (animator != null)
//         {
//             animator.SetFloat("MoveX", movementInputRaw.x);
//             animator.SetFloat("MoveY", movementInputRaw.y);
//             animator.SetBool("IsSprinting", isSprinting && isMoving);
//             animator.SetBool("IsGrounded", contact);
//         }
//     }

//     private void FixedUpdate()
//     {
//         Vector3 moveLocal = new Vector3(movementValue.x * sprintValue, 0f, movementValue.y * sprintValue);
//         Vector3 moveWorld = transform.TransformDirection(moveLocal) * Time.fixedDeltaTime;

//         _rigidbody.MovePosition(_rigidbody.position + moveWorld);
//     }
// }
