using UnityEngine;
using UnityEngine.Events;

public class WaveSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float startTime;
    public float endTime;
    public float spawnRate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // WaveManagers.instance.waves.Add(this);
        // WaveManagers.instance.AddWave(this);
        InvokeRepeating("Spawn", startTime, spawnRate);
        Invoke("EndSpawner", endTime);    // End Spawner.  Call EndSpawner 
        WaveManagers.instance.AddWave(this);
    }
    void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }



    void EndSpawner()
    {
        // WaveManagers.instance.waves.Remove(this);
        WaveManagers.instance.RemoveWave(this);
        CancelInvoke();
    }
}
