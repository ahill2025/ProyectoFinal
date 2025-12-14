using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using Unity.VisualScripting;


public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager instance;
    public List<Enemy> enemies;
    public UnityEvent OnChange;

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        OnChange.Invoke();
    }
    
    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy); 
        OnChange.Invoke();
    }
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple EnemiesManager instances detected");
        }
    }
    
}
