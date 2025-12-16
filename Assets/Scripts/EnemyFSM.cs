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

    public Transform baseTransform;      // Current tower target (normal towers first, then main tower)
    public Transform playerTransform;    // Player target

    public float baseAttackDistance;
    public float playerAttackDistance;
    private NavMeshAgent agent;

    [Header("Aiming")]
    [Tooltip("How fast the enemy turns to face the target (degrees per second).")]
    public float turnSpeed = 720f;

    [Header("Towers")]
    [Tooltip("The 4 outer towers must be tagged with this.")]
    public string towerTag = "Tower";

    [Tooltip("The final/boss tower must be tagged with this (and should NOT have any children tagged 'Tower').")]
    public string mainTowerTag = "MainTower";

    // -------------------- MODS START: Shooting + Animator Driving --------------------
    [Header("Shooting")]
    public Transform shootPoint; // Assign In Prefab (Child Transform). Falls back to this.transform if null.
    public GameObject bulletPrefab;

    [Header("Attack Timing")]
    [SerializeField] private float attackCooldown = 1.0f; // seconds between shots
    private float nextAttackTime = 0f;

    [Header("Animation")]
    [Tooltip("Animator parameters must be exactly: Bool 'IsMoving', Trigger 'Attack'.")]
    private Animator anim;

    private EnemyState lastState;
    // -------------------- MODS END --------------------

    private void Awake()
    {
        baseTransform = ChooseTowerTarget();

        // Fallback to old behavior if tags aren't set (optional safety)
        if (baseTransform == null)
        {
            var towerObj = GameObject.Find("Tower");
            if (towerObj != null) baseTransform = towerObj.transform;
        }

        // Null-safe player lookup (name first, then tag)
        TryResolvePlayer();

        agent = GetComponent<NavMeshAgent>();
        if (agent == null) agent = GetComponentInParent<NavMeshAgent>();

        // Cache Animator (Animator is on enemy root in your setup)
        anim = GetComponent<Animator>();
        if (anim == null) anim = GetComponentInChildren<Animator>();

        currentState = EnemyState.GoToBase; // ensure they start walking
        lastState = currentState;

        if (agent != null) agent.isStopped = false;
    }

    void TryResolvePlayer()
    {
        if (playerTransform != null) return;

        var p = GameObject.Find("Player");
        if (p != null)
        {
            playerTransform = p.transform;
            return;
        }

        var pt = GameObject.FindGameObjectWithTag("Player");
        if (pt != null)
        {
            playerTransform = pt.transform;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
    }

    void Update()
    {
        // Drive locomotion animation from NavMeshAgent movement
        if (anim != null && agent != null)
        {
            anim.SetBool("IsMoving", agent.velocity.sqrMagnitude > 0.01f);
        }

        // Detect State Entry
        if (currentState != lastState)
        {
            // When entering an attack state, allow immediate attack
            if (currentState == EnemyState.AttackBase || currentState == EnemyState.AttackPlayer)
            {
                nextAttackTime = 0f; // so first shot can happen instantly
            }

            lastState = currentState;
        }

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
        if (baseTransform == null || TowerIsInvalidOrDead(baseTransform))
        {
            baseTransform = ChooseTowerTarget();

            if (baseTransform == null)
            {
                if (agent != null) agent.isStopped = true;
                return;
            }
        }

        if (agent == null) return;

        agent.isStopped = false;
        agent.SetDestination(baseTransform.position);
        print("Going to base.");

        AimAt(baseTransform);

        if (sightSensor != null && sightSensor.detectedObject != null)
        {
            currentState = EnemyState.ChasePlayer;
            print("Changing to Chase Player state");
        }

        float distanceToBase = DistanceToTargetSurface(baseTransform);
        if (distanceToBase < baseAttackDistance)
        {
            currentState = EnemyState.AttackBase;
            print("Changing to Attack Base state");
        }
    }

    void AttackBase()
    {
        if (TowerIsInvalidOrDead(baseTransform))
        {
            baseTransform = ChooseTowerTarget();
            currentState = EnemyState.GoToBase;
            return;
        }

        float distanceToBase = DistanceToTargetSurface(baseTransform);
        if (distanceToBase > baseAttackDistance * 1.1f)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        if (agent != null) agent.isStopped = true;

        AimAt(baseTransform);

        // Bypass animation events: shoot directly on cooldown
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;

            if (anim != null) anim.SetTrigger("Attack");
            Shoot();
        }

        // If player detected while attacking tower, chase player.
        if (sightSensor != null && sightSensor.detectedObject != null)
        {
            currentState = EnemyState.ChasePlayer;
            print("Changing to Chase Player state");
        }
    }

    void ChasePlayer()
    {
        if (agent == null) return;

        if (playerTransform == null)
        {
            TryResolvePlayer();
            if (playerTransform == null)
            {
                currentState = EnemyState.GoToBase;
                return;
            }
        }

        agent.isStopped = false;
        print("Chasing player.");
        agent.SetDestination(playerTransform.position);

        Transform chaseTarget = (sightSensor != null && sightSensor.detectedObject != null)
            ? sightSensor.detectedObject.transform
            : playerTransform;

        AimAt(chaseTarget);

        if (sightSensor == null || sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            print("Changing to Go To Base state");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer < playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
            print("Changing to Attack Player state");
        }
    }

    void AttackPlayer()
    {
        if (agent != null) agent.isStopped = true;

        if (sightSensor != null && sightSensor.detectedObject != null)
        {
            AimAt(sightSensor.detectedObject.transform);
        }
        else if (playerTransform != null)
        {
            AimAt(playerTransform);
        }

        // Bypass animation events: shoot directly on cooldown
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;

            if (anim != null) anim.SetTrigger("Attack");
            Shoot();
        }

        print("Attacking player.");

        if (sightSensor == null || sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            print("Changing to Go To Base state");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            currentState = EnemyState.ChasePlayer;
            print("Changing to Chase Player state");
        }
    }

    void AimAt(Transform target)
    {
        if (target == null) return;

        Vector3 toTarget = target.position - transform.position;
        toTarget.y = 0f;

        if (toTarget.sqrMagnitude < 0.0001f) return;

        Quaternion desiredRotation = Quaternion.LookRotation(toTarget);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            desiredRotation,
            turnSpeed * Time.deltaTime
        );
    }

    // Bypass animation events: Shoot() is called directly from code now
    public void Shoot()
    {
        if (bulletPrefab == null) return;

        Transform sp = shootPoint != null ? shootPoint : transform;
        Instantiate(bulletPrefab, sp.position, sp.rotation);
    }

    float DistanceToTargetSurface(Transform target)
    {
        if (target == null) return float.PositiveInfinity;

        Collider col = target.GetComponentInChildren<Collider>();
        if (col != null)
        {
            Vector3 closest = col.ClosestPoint(transform.position);
            return Vector3.Distance(transform.position, closest);
        }

        return Vector3.Distance(transform.position, target.position);
    }

    bool TowerIsInvalidOrDead(Transform tower)
    {
        if (tower == null) return true;

        Life life = tower.GetComponent<Life>();
        if (life == null) life = tower.GetComponentInParent<Life>();
        if (life == null) life = tower.GetComponentInChildren<Life>();

        if (life == null) return false;

        return life.amount <= 0f;
    }

    bool IsPartOfMainTowerHierarchy(GameObject go)
    {
        if (go == null) return false;

        Transform t = go.transform;
        while (t != null)
        {
            if (t.CompareTag(mainTowerTag)) return true;
            t = t.parent;
        }
        return false;
    }

    Transform ChooseTowerTarget()
    {
        if (AnyNormalTowerAlive())
            return GetNearestAliveTower();

        return GetMainTower();
    }

    bool AnyNormalTowerAlive()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);
        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i] == null) continue;

            if (IsPartOfMainTowerHierarchy(towers[i])) continue;

            Life life = towers[i].GetComponent<Life>();
            if (life == null) life = towers[i].GetComponentInParent<Life>();
            if (life == null) life = towers[i].GetComponentInChildren<Life>();

            if (life != null && life.amount > 0f) return true;
        }
        return false;
    }

    Transform GetNearestAliveTower()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);

        if (towers == null || towers.Length == 0)
        {
            var towerObj = GameObject.Find("Tower");
            return towerObj != null ? towerObj.transform : null;
        }

        Transform best = null;
        float bestDist = float.PositiveInfinity;

        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i] == null) continue;

            if (IsPartOfMainTowerHierarchy(towers[i])) continue;

            Transform t = towers[i].transform;

            Life life = t.GetComponent<Life>();
            if (life == null) life = t.GetComponentInParent<Life>();
            if (life == null) life = t.GetComponentInChildren<Life>();

            if (life != null && life.amount <= 0f) continue;

            float d = (t.position - transform.position).sqrMagnitude;
            if (d < bestDist)
            {
                bestDist = d;
                best = t;
            }
        }

        return best;
    }

    Transform GetMainTower()
    {
        GameObject main = GameObject.FindGameObjectWithTag(mainTowerTag);
        return main != null ? main.transform : null;
    }
}



















// using JetBrains.Annotations;
// using Unity.VisualScripting;
// using System;
// using UnityEngine;
// using UnityEngine.AI;

// public class EnemyFSM : MonoBehaviour
// {
    
//     public enum EnemyState
//     {
//         GoToBase,
//         AttackBase,
//         ChasePlayer,
//         AttackPlayer
//     }

    

//     public EnemyState currentState;
//     public Sight sightSensor;

//     public Transform baseTransform; // Base position
//     public Transform playerTransform;   // Player position

//     public float baseAttackDistance;
//     public float playerAttackDistance;
//     private NavMeshAgent agent;

//     private void Awake()
//     {
//         baseTransform = GameObject.Find("Tower").transform;
//         playerTransform = GameObject.Find("Player").transform;
//         agent = GetComponentInParent<NavMeshAgent>();
//     }
//     // Update is called once per frame

//     private void OnDrawGizmos()
//     {
//         Gizmos.color = Color.green;
//         Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
//         Gizmos.color = Color.yellow;
//         Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
//     }
//     void Update()
//     {
//         if (currentState == EnemyState.GoToBase)
//         {
//             GoToBase();
//         }
//         else if (currentState == EnemyState.AttackBase)
//         {
//             AttackBase();
//         }
//         else if (currentState == EnemyState.ChasePlayer)
//         {
//             ChasePlayer();
//         }
//         else 
//         {
//             AttackPlayer();
//         }
//     }


//     void GoToBase()
//     {
//         agent.isStopped = false;
//         agent.SetDestination(baseTransform.position);   // Queremos que el enemigo vaya hacia la base
//         print("Going to base.");
//         if (sightSensor.detectedObject != null)
//         {
//             currentState = EnemyState.ChasePlayer;
//             print("Changing to Chase Player state");
//         }
        
//         float distanceToBase = Vector3.Distance(transform.position, baseTransform.position);
//         if (distanceToBase < baseAttackDistance)
//         {
//             currentState = EnemyState.AttackBase;
//             print("Changing to Attack Base state");
//         }
        
//     }

//     void AttackBase()
//     {
//         // print("Attacking base.");
//         Debug.Log("Attacking base. detectedObject = " + sightSensor.detectedObject);
//         agent.isStopped = true;
//         EnemyShoot();
        
//     }

//     void ChasePlayer()
//     {
//         agent.isStopped = false;
//         print("Chasing player.");
//         agent.SetDestination(playerTransform.position);   // Queremos que el enemigo vaya hacia el jugador
//         if (sightSensor.detectedObject == null)
//         {
//             currentState = EnemyState.GoToBase;
//             print("Changing to Go To Base state");
//             return;
//         }
//         float distanceToPlayer = Vector3.Distance(transform.position,sightSensor.detectedObject.transform.position);
//         if (distanceToPlayer < playerAttackDistance)
//         {
//             currentState = EnemyState.AttackPlayer;
//             print("Changing to Attack Player state");
//         }
//     }

//     void AttackPlayer()
//     {
//         agent.isStopped = true;
//         EnemyShoot();
//         print("Attacking player.");
//         if (sightSensor.detectedObject == null)
//         {
//             currentState = EnemyState.GoToBase;
//             print("Changing to Go To Base state");
//             return;
//         }
//         float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
//         if (distanceToPlayer > playerAttackDistance*1.1f)
//         {
//             currentState = EnemyState.ChasePlayer;
//             print("Changing to Chase Player state");
//         }
//     }

//     // public float lastShootTime;
//     // public GameObject bulletPrefabb;
//     // public float fireRate;

//     // void EnemyShoot()
//     // {
//     //     var timeSinceLastShot = Time.time - lastShootTime;
//     //     if (timeSinceLastShot < fireRate)
//     //     {
//     //         lastShootTime = Time.time;
//     //         Instantiate(bulletPrefabb, transform.position , transform.rotation);
//     //     }
//     // }


//     [SerializeField] private float fireRate = 1f;
//     private float lastShootTime = -999f;
//     public GameObject bulletPrefab;

//     void EnemyShoot()
//     {
//         float timeSinceLastShot = Time.time - lastShootTime;

//         if (timeSinceLastShot >= fireRate)
//         {
//             lastShootTime = Time.time;
//             Instantiate(bulletPrefab, transform.position, transform.rotation);
//         }
//     }
// }