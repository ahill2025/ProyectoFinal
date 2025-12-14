using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        // EnemiesManager.instance.enemies.Add(this);      // Adds 'Enemy' object to 'enemies' list.
        EnemiesManager.instance.AddEnemy(this);
    }
    
    void OnDestroy()
    {
        // EnemiesManager.instance.enemies.Remove(this);
        EnemiesManager.instance.RemoveEnemy(this);
    }


}
