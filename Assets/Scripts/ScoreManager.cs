using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;        // 'static' used for it to act as a global variable, to be accessed by different classes.   
    // Defining 'ScoreManager' variable in ScoreManager (to share)
    public int amount;

    void Awake()
    {
        if (instance == null) // No score manager
        {
            instance = this;
        }
        else
        {
            //print("Multiple ScoreManager instances detected.  Destroying duplicate.");
            Debug.LogError("Multiple ScoreManager instances detected.  Destroying duplicate.");
        }
    }

}
