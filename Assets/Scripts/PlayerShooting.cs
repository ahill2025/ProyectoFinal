        using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootPoint;

    [SerializeField] private Animator animator;

    [SerializeField] private float shotsPerSecond = 8f;

    private bool buttonHeld;
    private float nextShotTime;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    public void OnFire(InputValue button)
    {
        buttonHeld = button.isPressed;
    }

    void Update()
    {
        if (animator == null || prefab == null || shootPoint == null) return;

        bool isGrounded = animator.GetBool("IsGrounded");
        bool isSprinting = animator.GetBool("IsSprinting");

        bool canShoot = buttonHeld && isGrounded && !isSprinting;

        // Drive looping shoot animation
        animator.SetBool("IsFiring", canShoot);

        if (!canShoot) return;

        // Automatic fire rate
        if (Time.time < nextShotTime) return;
        nextShotTime = Time.time + (1f / shotsPerSecond);

        GameObject clone = Instantiate(prefab);
        clone.transform.position = shootPoint.transform.position;
        clone.transform.rotation = shootPoint.transform.rotation;
    }
}



// using UnityEngine;
// using System;
// using UnityEngine.InputSystem;
// using UnityEngine.UI;

// public class PlayerShooting : MonoBehaviour
// {
//     public GameObject prefab;       // Projectile (Bullet)
//     public GameObject shootPoint;   // GameObject where our bullet will be firing from
//     private bool buttonPressed = false;     // Used to signal whether the fire button is being pressed or not.  Code be modified for automatic fire.

//     private Button button;  // Button variable
//     public void OnFire(InputValue button)   // Sends OnFire() activated by Action 
//     {
//         if (button.isPressed){buttonPressed = true;}    // if-statement that will assign boolean to 'true'
//         else{buttonPressed = false;}
//     }
//     // Update is called once per frame
//     void Update()
//     {
//         if (buttonPressed)      // if buttonPressed is true, fire a bullet.
//         {
//             GameObject clone = Instantiate(prefab);     // Instantiate 'Bullet' gameObject
//             clone.transform.position = shootPoint.transform.position;   // Assigns 'Bullet' gameObject clone shootPoint's transform position
//             clone.transform.rotation = shootPoint.transform.rotation;   // Assigns 'Bullet' gameObject clone shootPoint's transform rotation
//             //buttonPressed = false;  // Assigns 'false' to 'buttonPressed' for single fire (projectile per press)
//         }
//     }


// }


