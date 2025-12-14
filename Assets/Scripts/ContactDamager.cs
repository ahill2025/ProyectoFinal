using Unity.VisualScripting;
using UnityEngine;

public class ContactDamager : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);    // Since it is in bullet, gameObject is the bullet itself

        Life life = other.GetComponent<Life>();   // It is applied to player objects, it affects them.
        if (life!=null)     // If you hit objects without Life class applied, returns null (Walls and other objects)
        {
            life.amount -= damage;
        }
    }
}
