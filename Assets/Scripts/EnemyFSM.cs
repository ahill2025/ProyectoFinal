using JetBrains.Annotations;
using Unity.VisualScripting;
using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    
    public enum EnemyState
    {
        GoToBase,
        AttackBase,
        ChasePlayer,
        AttackPlayer
    }

    

    public EnemyState currentState;
    public Sight sightSensor;

    public Transform baseTransform; // Base position
    public Transform playerTransform;   // Player position

    public float baseAttackDistance;
    public float playerAttackDistance;
    private NavMeshAgent agent;

    private void Awake()
    {
        baseTransform = GameObject.Find("Base").transform;
        playerTransform = GameObject.Find("Player").transform;
        agent = GetComponentInParent<NavMeshAgent>();
    }
    // Update is called once per frame

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
    }
    void Update()
    {
        if (currentState == EnemyState.GoToBase)
        {
            GoToBase();
        }
        else if (currentState == EnemyState.AttackBase)
        {
            AttackBase();
        }
        else if (currentState == EnemyState.ChasePlayer)
        {
            ChasePlayer();
        }
        else 
        {
            AttackPlayer();
        }
    }


    void GoToBase()
    {
        agent.isStopped = false;
        agent.SetDestination(baseTransform.position);   // Queremos que el enemigo vaya hacia la base
        print("Going to base.");
        if (sightSensor.detectedObject != null)
        {
            currentState = EnemyState.ChasePlayer;
            print("Changing to Chase Player state");
        }
        
        float distanceToBase = Vector3.Distance(transform.position, baseTransform.position);
        if (distanceToBase < baseAttackDistance)
        {
            currentState = EnemyState.AttackBase;
            print("Changing to Attack Base state");
        }
        
    }

    void AttackBase()
    {
        // print("Attacking base.");
        Debug.Log("Attacking base. detectedObject = " + sightSensor.detectedObject);
        agent.isStopped = true;
        EnemyShoot();
        
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        print("Chasing player.");
        agent.SetDestination(playerTransform.position);   // Queremos que el enemigo vaya hacia el jugador
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            print("Changing to Go To Base state");
            return;
        }
        float distanceToPlayer = Vector3.Distance(transform.position,sightSensor.detectedObject.transform.position);
        if (distanceToPlayer < playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
            print("Changing to Attack Player state");
        }
    }

    void AttackPlayer()
    {
        agent.isStopped = true;
        EnemyShoot();
        print("Attacking player.");
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            print("Changing to Go To Base state");
            return;
        }
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer > playerAttackDistance*1.1f)
        {
            currentState = EnemyState.ChasePlayer;
            print("Changing to Chase Player state");
        }
    }

    public float lastShootTime;
    public GameObject bulletPrefabb;
    public float fireRate;

    void EnemyShoot()
    {
        var timeSinceLastShot = Time.time - lastShootTime;
        if (timeSinceLastShot < fireRate)
        {
            lastShootTime = Time.time;
            Instantiate(bulletPrefabb, transform.position , transform.rotation);
        }
    }
}


