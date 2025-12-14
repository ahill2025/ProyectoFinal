using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WavesGameMode : MonoBehaviour
{
    [SerializeField] Life playerLife;       // We dragged Player game object; Player life
    [SerializeField] Life playerBaseLife;   // Base life

    void Start()
    {
        playerLife.onDeath.AddListener(OnPlayerDiedorBaseDied);
        playerBaseLife.onDeath.AddListener(OnPlayerDiedorBaseDied);
        EnemiesManager.instance.OnChange.AddListener(CheckWinCondition);
        WaveManagers.instance.OnChange.AddListener(CheckWinCondition);
    }

    

    void OnPlayerDiedorBaseDied()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    // Update is called once per frame
    void CheckWinCondition()
    {
        if (EnemiesManager.instance.enemies.Count <= 0
        && WaveManagers.instance.waves.Count <= 0)
        {
            SceneManager.LoadScene("WinScreen");
            // Debug.Log("You Win!");
        }
        // if (playerLife.amount <= 0)
        // {
        //     SceneManager.LoadScene("LoseScreen");
        //     // Debug.Log("You Lose!");
        // }
    }
}
