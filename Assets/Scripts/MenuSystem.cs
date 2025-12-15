using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Play()
    {
        
        SceneManager.LoadScene("FinalProjectGame");
    }

    // Update is called once per frame
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
