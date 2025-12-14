using UnityEngine;

public class MyFirstScript : MonoBehaviour
{
    [SerializeField]
    //private float speed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("Test Start.");
    }

    // Update is called once per frame
    void Update()
    {
        print("Hello World!");
    }
}

