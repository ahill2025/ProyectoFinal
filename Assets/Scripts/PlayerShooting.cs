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


// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerShooting : MonoBehaviour
// {
//     public GameObject prefab;        // Projectile (Bullet)
//     public Transform shootPoint;     // Where the bullet fires from
//     [SerializeField] private Animator animator;

//     private bool buttonPressed;
//     private bool shotConsumed;       // Prevents auto-retrigger on landing

//     private void Awake()
//     {
//         if (animator == null)
//         {
//             animator = GetComponentInChildren<Animator>();
//         }
//     }

//     public void OnFire(InputValue button)
//     {
//         if (button.isPressed)
//         {
//             buttonPressed = true;
//         }
//         else
//         {
//             buttonPressed = false;
//             shotConsumed = false;    // Reset when button released
//         }
//     }

//     void Update()
//     {
//         if (!buttonPressed || shotConsumed) return;
//         if (animator == null) return;

//         // Read Animator state
//         bool isGrounded = animator.GetBool("IsGrounded");
//         bool isSprinting = animator.GetBool("IsSprinting");

//         // Gate shooting
//         if (!isGrounded) return;
//         if (isSprinting) return;

//         // Fire once per press
//         shotConsumed = true;

//         // Spawn projectile
//         GameObject clone = Instantiate(prefab);
//         clone.transform.SetPositionAndRotation(
//             shootPoint.position,
//             shootPoint.rotation
//         );

//         // Trigger shoot animation
//         animator.SetTrigger("Shoot");
//     }
// }




// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerShooting : MonoBehaviour
// {
//     public GameObject prefab;
//     public GameObject shootPoint;

//     private bool buttonPressed;
//     private Animator anim;

//     void Awake()
//     {
//         anim = GetComponent<Animator>();
//     }

//     public void OnFire(InputValue button)
//     {
//         if (button.isPressed)
//         {
//             // Trigger the attack animation ONCE
//             anim.SetTrigger("Attack");
//         }
//     }

//     // Called by Animation Event
//     public void Shoot()
//     {
//         GameObject clone = Instantiate(prefab);
//         clone.transform.position = shootPoint.transform.position;
//         clone.transform.rotation = shootPoint.transform.rotation;
//     }
// }

// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerShooting : MonoBehaviour
// {
//     public GameObject prefab;          // Bullet prefab
//     public Transform shootPoint;       // Drag your muzzle/fire point here in Inspector

//     public float fireRate = 6f;

//     private bool buttonPressed;
//     private float nextFireTime;
//     private Animator anim;

//     void Awake()
//     {
//         anim = GetComponent<Animator>();
//         anim.SetBool("IsFiring", buttonPressed);
//     }

//     public void OnFire(InputValue button)
//     {
//         buttonPressed = button.isPressed;
//     }

//     void Update()
//     {
//         if (!buttonPressed) return;

//         if (Time.time >= nextFireTime)
//         {
//             nextFireTime = Time.time + (1f / fireRate);
//             anim.SetTrigger("Attack");
//         }
//     }

//     // Called by the Animation Event
//     public void Shoot()
//     {
//         Instantiate(prefab, shootPoint.position, shootPoint.rotation);
//     }
// }
//  using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerShooting : MonoBehaviour
// {
//     [Header("Projectile")]
//     public GameObject prefab;            // Bullet prefab
//     public Transform shootPoint;         // Muzzle / fire point

//     [Header("Auto Fire")]
//     [Tooltip("Shots per second. Example: 6 = 6 bullets/sec.")]
//     public float fireRate = 6f;

//     [Header("Animator")]
//     [Tooltip("If empty, will auto-find an Animator on this object or children.")]
//     public Animator anim;
//     [Tooltip("Animator bool parameter name that keeps the shoot state active while held.")]
//     public string isFiringParam = "IsFiring";

//     private bool buttonPressed;
//     private float nextFireTime;

//     void Awake()
//     {
//         if (anim == null)
//         {
//             anim = GetComponent<Animator>();
//             if (anim == null) anim = GetComponentInChildren<Animator>();
//         }

//         SetFiringBool(false);
//         nextFireTime = 0f;
//     }

//     // public void OnFire(InputValue button)
//     // {
//     //     buttonPressed = button.isPressed;

//     //     // Hold -> play shooting animation state. Release -> return to locomotion.
//     //     SetFiringBool(buttonPressed);

//     //     // Optional: Fire immediately on press.
//     //     if (buttonPressed)
//     //     {
//     //         nextFireTime = 0f;
//     //     }
//     // }

//     public void OnFire(InputValue button)
//     {
//         buttonPressed = button.isPressed;
//         Debug.Log($"OnFire called. buttonPressed={buttonPressed}");
//         SetFiringBool(buttonPressed);
//         if (buttonPressed) nextFireTime = 0f;
//     }


//     void Update()
//     {
//         // Keep Animator in sync even if input events behave oddly.
//         SetFiringBool(buttonPressed);

//         if (!buttonPressed) return;
//         if (prefab == null || shootPoint == null) return;
//         if (fireRate <= 0f) return;

//         if (Time.time >= nextFireTime)
//         {
//             nextFireTime = Time.time + (1f / fireRate);
//             FireProjectile();
//         }
//     }

//     private void SetFiringBool(bool value)
//     {
//         if (anim == null) return;
//         anim.SetBool(isFiringParam, value);
//     }

//     private void FireProjectile()
//     {
//         Instantiate(prefab, shootPoint.position, shootPoint.rotation);
//     }
// }


// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerShooting : MonoBehaviour
// {
//     public GameObject prefab;        // Bullet prefab
//     public Transform shootPoint;     // Muzzle / fire point

//     [Header("Auto Fire")]
//     [Tooltip("Shots per second. Example: 6 = 6 bullets/sec.")]
//     public float fireRate = 6f;

//     private bool buttonPressed;
//     private float nextFireTime;
//     private Animator anim;

//     void Awake()
//     {
//         anim = GetComponent<Animator>();
//         anim.SetBool("IsFiring", false);
//     }

//     public void OnFire(InputValue button)
//     {
//         buttonPressed = button.isPressed;

//         // Hold = play shoot animation, release = return to locomotion.
//         anim.SetBool("IsFiring", buttonPressed);

//         // Optional: fire immediately when the button is first pressed.
//         if (buttonPressed)
//         {
//             nextFireTime = 0f;
//         }
//     }

//     void Update()
//     {
//         if (!buttonPressed) return;
//         if (prefab == null || shootPoint == null) return;
//         if (fireRate <= 0f) return;

//         if (Time.time >= nextFireTime)
//         {
//             nextFireTime = Time.time + (1f / fireRate);
//             FireProjectile();
//         }
//     }

//     private void FireProjectile()
//     {
//         Instantiate(prefab, shootPoint.position, shootPoint.rotation);
//     }

//     // IMPORTANT:
//     // If you are now using timer-based auto fire, you should NOT use the Animation Event.
//     // Leaving this method is fine, but remove/disable the Animation Event that calls Shoot(),
//     // or it will double-fire.
//     public void Shoot()
//     {
//         FireProjectile();
//     }
// }









        // if (Input.GetKeyDown(KeyCode.Mouse0))
        // {
        //     GameObject clone = Instantiate(prefab);
        //     //clone.transform.position = transform.position;
        //     //clone.transform.rotation = transform.rotation;
        //     clone.transform.position = shootPoint.transform.position;
        //     clone.transform.rotation = shootPoint.transform.rotation;
        // }       // Mouse0 is left-click, Mouse1 is right-click









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
