using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WaveManagers : MonoBehaviour
{
    public static WaveManagers instance;

    public List<WaveSpawner> waves;

    public UnityEvent OnChange;

    public void AddWave(WaveSpawner wave)
    {
        waves.Add(wave);
        OnChange.Invoke();
    }
    
    public void RemoveWave(WaveSpawner wave)
    {
        waves.Remove(wave);
        OnChange.Invoke();
    }

    void Awake()
    {
        if (instance == null)       // Singleton for WavesManager
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple WaveManager instances active.");
        }
        
    }
}
