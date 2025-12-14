using System;
using UnityEngine;
using UnityEngine.InputSystem;

// public class Task5Player : MonoBehaviour
// {

//     public Vector2 MovementInputVector { get; private set; } // setter so that only this script can change value
//     private void OnMove(InputValue inputValue)
//     {
//         MovementInputVector = inputValue.Get<Vector2>();        // Value available in PlayerControllerT5
//     }
// //
// }

/// <summary>
/// Part 2
/// </summary>

public class Task5Player : MonoBehaviour
{
    // Raise an event whenever the jump button is pressed

    public Vector2 MovementInputVector { get; private set; } // setter so that only this script can change value

    public event Action OnJumpButtonPressed;    // C# Action as event type since we don't need to pass parameters
    // We need to raise this event whenever this input action is triggered

    private void OnMove(InputValue inputValue)
    {
        MovementInputVector = inputValue.Get<Vector2>();        // Value available in PlayerControllerT5
    }

    private void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            OnJumpButtonPressed?.Invoke();
        }
    }
    //
}

/////////
/// Part 3
/// ////
/// 
/// 



