using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    public float amount;
    public UnityEvent onDeath;  // Nov 4

    void Update()   // *NEW* Verifies each frame.  It will ask if gameObject has life
    {
        if (amount<=0)
        {
            onDeath.Invoke();
            Destroy(gameObject);
        }
    }

}
