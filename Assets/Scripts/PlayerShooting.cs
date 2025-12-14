using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;       // Projectile (Bullet)
    public GameObject shootPoint;   // GameObject where our bullet will be firing from
    private bool buttonPressed = false;     // Used to signal whether the fire button is being pressed or not.  Code be modified for automatic fire.

    private Button button;  // Button variable
    public void OnFire(InputValue button)   // Sends OnFire() activated by Action 
    {
        if (button.isPressed){buttonPressed = true;}    // if-statement that will assign boolean to 'true'
        else{buttonPressed = false;}
    }
    // Update is called once per frame
    void Update()
    {
        if (buttonPressed)      // if buttonPressed is true, fire a bullet.
        {
            GameObject clone = Instantiate(prefab);     // Instantiate 'Bullet' gameObject
            clone.transform.position = shootPoint.transform.position;   // Assigns 'Bullet' gameObject clone shootPoint's transform position
            clone.transform.rotation = shootPoint.transform.rotation;   // Assigns 'Bullet' gameObject clone shootPoint's transform rotation
            //buttonPressed = false;  // Assigns 'false' to 'buttonPressed' for single fire (projectile per press)
        }
    }
}









        // if (Input.GetKeyDown(KeyCode.Mouse0))
        // {
        //     GameObject clone = Instantiate(prefab);
        //     //clone.transform.position = transform.position;
        //     //clone.transform.rotation = transform.rotation;
        //     clone.transform.position = shootPoint.transform.position;
        //     clone.transform.rotation = shootPoint.transform.rotation;
        // }       // Mouse0 is left-click, Mouse1 is right-click