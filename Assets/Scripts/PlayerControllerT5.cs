using Unity.VisualScripting;
using UnityEngine;

// First Video For movement
// public class PlayerControllerT5 : MonoBehaviour
// {
//     [SerializeField]
//     private float _Speed;
//     private Task5Player _playerInputController;

//     private void Awake()    // Called by Unity once; ideal place for initialization
//     {
//         _playerInputController = GetComponent<Task5Player>();
//     }

//     void Update()
//     {
//         Vector3 positionChange = new Vector3(_playerInputController.MovementInputVector.x,
//         0,
//         _playerInputController.MovementInputVector.y)
//         * Time.deltaTime
//         * _Speed;
//         transform.position += positionChange;   // If no input, the value will be 0.  Otherwise, it will have a value (movement)

//     }
// }

/// <summary>
/// ////////////////////////
/// Previous code above we're updating the position of the player
/// based on the input received.  
/// New code will change this by having the RigidBody control the movement
/// instead.


// public class PlayerControllerT5 : MonoBehaviour
// {
//     [SerializeField]
//     private float _Speed;
//     private Task5Player _playerInputController;
//     private Rigidbody _rigidbody;

//     private void Awake()    // Called by Unity once; ideal place for initialization
//     {
//         _playerInputController = GetComponent<Task5Player>();
//         _rigidbody = GetComponent<Rigidbody>();
//     }

//     void FixedUpdate()
//     {
//         Vector3 velocity = new Vector3(_playerInputController.MovementInputVector.x,
//         0,
//         _playerInputController.MovementInputVector.y)
//         * _Speed;

//         velocity.y = _rigidbody.linearVelocity.y;
//         _rigidbody.linearVelocity = velocity;


//     }
// }


public class PlayerControllerT5 : MonoBehaviour
{
    [SerializeField]
    private float _Speed;
    [SerializeField]
    private float _JumpSpeed;

    private float _Boost = 1;
    private Task5Player _playerInputController;
    private GroundControllerT5 _groundController;
    private Rigidbody _rigidbody;
    private bool _jumpTriggered;

    private void Awake()    // Called by Unity once; ideal place for initialization
    {
        _playerInputController = GetComponent<Task5Player>();
        _rigidbody = GetComponent<Rigidbody>();
        _groundController = GetComponent<GroundControllerT5>();

        _playerInputController.OnJumpButtonPressed += JumpButtonPressed;
    }

    void FixedUpdate()
    {
        Vector3 velocity = new Vector3(_playerInputController.MovementInputVector.x,
        0,
        _playerInputController.MovementInputVector.y)
        * _Speed *_Boost;

        velocity.y = _rigidbody.linearVelocity.y;

        if (_jumpTriggered)
        {
            velocity.y = _JumpSpeed;
            _jumpTriggered = false;
        }
        _rigidbody.linearVelocity = velocity;


    }

    private void JumpButtonPressed()
    {
        if (_groundController.isGrounded)
        {
            _jumpTriggered = true;
        }
    }
}