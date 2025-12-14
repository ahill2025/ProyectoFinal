using UnityEngine;

public class Autodestroy : MonoBehaviour
{
    public float delay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Destroy(gameObject, delay);    // gameObject === objeto, no clase
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
