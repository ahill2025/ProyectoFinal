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

//     public Transform baseTransform;      // Tower target
//     public Transform playerTransform;    // Player target

//     public float baseAttackDistance;
//     public float playerAttackDistance;
//     private NavMeshAgent agent;

//     // -------------------- CHANGES START --------------------
//     [Header("Aiming")]
//     [Tooltip("How fast the enemy turns to face the target (degrees per second).")]
//     public float turnSpeed = 720f;
//     // -------------------- CHANGES END --------------------

//     private void Awake()
//     {
//         baseTransform = GameObject.Find("Tower").transform;
//         playerTransform = GameObject.Find("Player").transform;
//         agent = GetComponentInParent<NavMeshAgent>();
//     }

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
//         agent.SetDestination(baseTransform.position);
//         print("Going to base.");

//         // -------------------- CHANGES START --------------------
//         // CHANGE: While moving to the tower, also rotate to face it.
//         // This keeps aiming stable even if enemies get pushed by other enemies near the tower.
//         AimAt(baseTransform);
//         // -------------------- CHANGES END --------------------

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
//         Debug.Log("Attacking base. detectedObject = " + sightSensor.detectedObject);
//         agent.isStopped = true;

//         // -------------------- CHANGES START --------------------
//         // CHANGE: Always face the tower before shooting, so shots go toward the target.
//         AimAt(baseTransform);
//         // -------------------- CHANGES END --------------------

//         EnemyShoot();
//     }

//     void ChasePlayer()
//     {
//         agent.isStopped = false;
//         print("Chasing player.");
//         agent.SetDestination(playerTransform.position);

//         // -------------------- CHANGES START --------------------
//         // CHANGE: While chasing, face the player (or the detected object) to keep aim aligned.
//         // If the player moves and enemies get bumped, they keep tracking the target direction.
//         Transform chaseTarget = (sightSensor != null && sightSensor.detectedObject != null)
//             ? sightSensor.detectedObject.transform
//             : playerTransform;
//         AimAt(chaseTarget);
//         // -------------------- CHANGES END --------------------

//         if (sightSensor.detectedObject == null)
//         {
//             currentState = EnemyState.GoToBase;
//             print("Changing to Go To Base state");
//             return;
//         }

//         float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
//         if (distanceToPlayer < playerAttackDistance)
//         {
//             currentState = EnemyState.AttackPlayer;
//             print("Changing to Attack Player state");
//         }
//     }

//     void AttackPlayer()
//     {
//         agent.isStopped = true;

//         // -------------------- CHANGES START --------------------
//         // CHANGE: Face the detected player target before shooting so bullets go toward the player.
//         if (sightSensor != null && sightSensor.detectedObject != null)
//         {
//             AimAt(sightSensor.detectedObject.transform);
//         }
//         // -------------------- CHANGES END --------------------

//         EnemyShoot();
//         print("Attacking player.");

//         if (sightSensor.detectedObject == null)
//         {
//             currentState = EnemyState.GoToBase;
//             print("Changing to Go To Base state");
//             return;
//         }

//         float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
//         if (distanceToPlayer > playerAttackDistance * 1.1f)
//         {
//             currentState = EnemyState.ChasePlayer;
//             print("Changing to Chase Player state");
//         }
//     }

//     // -------------------- CHANGES START --------------------
//     // CHANGE: New helper method that rotates the enemy toward a target smoothly.
//     // This keeps the enemy's forward direction aligned with the target, so Instantiate(..., transform.rotation)
//     // produces bullets that travel toward the intended target even if the enemy is moving or being pushed.
//     void AimAt(Transform target)
//     {
//         if (target == null) return;

//         Vector3 toTarget = target.position - transform.position;

//         // Ignore vertical difference so the enemy stays upright.
//         toTarget.y = 0f;

//         if (toTarget.sqrMagnitude < 0.0001f) return;

//         Quaternion desiredRotation = Quaternion.LookRotation(toTarget);
//         transform.rotation = Quaternion.RotateTowards(
//             transform.rotation,
//             desiredRotation,
//             turnSpeed * Time.deltaTime
//         );
//     }
//     // -------------------- CHANGES END --------------------

//     [SerializeField] private float fireRate = 1f;
//     private float lastShootTime = -999f;
//     public GameObject bulletPrefab;

//     void EnemyShoot()
//     {
//         float timeSinceLastShot = Time.time - lastShootTime;

//         if (timeSinceLastShot >= fireRate)
//         {
//             lastShootTime = Time.time;

//             // NOTE: This uses transform.rotation, which now points toward the target because AimAt(...) updates it.
//             Instantiate(bulletPrefab, transform.position, transform.rotation);
//         }
//     }
// }



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

//     public Transform baseTransform;      // Current tower target (we will update this when towers die)
//     public Transform playerTransform;    // Player target

//     public float baseAttackDistance;
//     public float playerAttackDistance;
//     private NavMeshAgent agent;

//     [Header("Aiming")]
//     [Tooltip("How fast the enemy turns to face the target (degrees per second).")]
//     public float turnSpeed = 720f;

//     // -------------------- CHANGES START --------------------
//     [Header("Towers")]
//     [Tooltip("All tower objects should be tagged with this (e.g., Tower).")]
//     public string towerTag = "Tower";

//     // Main/boss tower tag (only becomes active once all normal towers are dead)
//     public string mainTowerTag = "MainTower";

//     // -------------------- CHANGES END --------------------

//     private void Awake()
//     {
//         // CHANGE: Still works if you only have 1 tower named "Tower",
//         // but now we prefer choosing a tower via tag so multiple towers are supported.
//         // baseTransform = GetNearestAliveTower(); // <-- CHANGED
//         // PHASE LOGIC: choose outer towers first, main tower only after they are destroyed
//         if (AnyNormalTowerAlive())
//         {
//             baseTransform = GetNearestAliveTower();
//         }
//         else
//         {
//             baseTransform = GetMainTower();
//         }

//         if (baseTransform == null)
//         {
//             // Fallback to your old behavior if you forgot to tag towers.
//             var towerObj = GameObject.Find("Tower");
//             if (towerObj != null) baseTransform = towerObj.transform;
//         }

//         playerTransform = GameObject.Find("Player").transform;
//         agent = GetComponent<NavMeshAgent>();
//         if (agent == null) agent = GetComponentInParent<NavMeshAgent>();

//         currentState = EnemyState.GoToBase; // ensure they start walking
//         agent.isStopped = false;            // ensure agent is allowed to move

//     }

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
//         Debug.Log($"GoToBase START | state={currentState} | agentNull={agent==null} | stopped={(agent!=null && agent.isStopped)} | onNavMesh={(agent!=null && agent.isOnNavMesh)} | tower={(baseTransform?baseTransform.name:"NULL")}");

//         // if (baseTransform == null) baseTransform = GetNearestAliveTower();
//         // PHASE LOGIC: choose outer towers first, main tower only after they are destroyed
//         // if (AnyNormalTowerAlive())
//         // {
//         //     baseTransform = GetNearestAliveTower();
//         // }
//         // else
//         // {
//         //     baseTransform = GetMainTower();
//         // }

//         // PHASE LOGIC: Only retarget when we don't have a valid target.
//         // if (baseTransform == null || TowerIsInvalidOrDead(baseTransform))
//         // {
//         //     if (AnyNormalTowerAlive())
//         //     {
//         //         baseTransform = GetNearestAliveTower();
//         //     }
//         //     else
//         //     {
//         //         baseTransform = GetMainTower();
//         //     }
//         // }

//         if (baseTransform == null || TowerIsInvalidOrDead(baseTransform))
//         {
//             baseTransform = ChooseTowerTarget();

//             if (baseTransform == null)
//             {
//                 agent.isStopped = true;
//                 return;
//             }
//         }



//         // -------------------- CHANGES START --------------------
//         // CHANGE: If the current tower is gone/dead, pick a new one immediately.
//         // if (TowerIsInvalidOrDead(baseTransform))
//         // {
//         //     baseTransform = GetNearestAliveTower();
//         //     if (baseTransform == null)
//         //     {
//         //         // No towers left. Stop moving/shooting; your game mode can treat this as a win.
//         //         agent.isStopped = true;
//         //         return;
//         //     }
//         // }

//         // CHANGE: If the current target (tower or main tower) is gone/dead, pick a new one using PHASE logic.
//         // This prevents overwriting the main tower target with GetNearestAliveTower().
//         // if (TowerIsInvalidOrDead(baseTransform))
//         // {
//         //     if (AnyNormalTowerAlive())
//         //     {
//         //         baseTransform = GetNearestAliveTower();
//         //     }
//         //     else
//         //     {
//         //         baseTransform = GetMainTower();
//         //     }

//         //     if (baseTransform == null)
//         //     {
//         //         agent.isStopped = true;
//         //         return;
//         //     }
//         // }

//         // -------------------- CHANGES END --------------------



//         agent.isStopped = false;
//         agent.SetDestination(baseTransform.position);
//         print("Going to base.");

//         AimAt(baseTransform);

//         // Keep behavior: if player detected before reaching tower, chase player.
//         if (sightSensor != null && sightSensor.detectedObject != null)
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
//         // -------------------- CHANGES START --------------------
//         // CHANGE: If the tower we were attacking got destroyed, stop attacking and retarget.
//         // if (TowerIsInvalidOrDead(baseTransform))
//         // {
//         //     // baseTransform = GetNearestAliveTower();
//         //     // PHASE LOGIC: choose outer towers first, main tower only after they are destroyed
//         //     // if (AnyNormalTowerAlive())
//         //     // {
//         //     //     baseTransform = GetNearestAliveTower();
//         //     // }
//         //     // else
//         //     // {
//         //     //     baseTransform = GetMainTower();
//         //     // }


//         //     if (baseTransform == null || TowerIsInvalidOrDead(baseTransform))
//         //     {
//         //         if (AnyNormalTowerAlive())
//         //         {
//         //             baseTransform = GetNearestAliveTower();
//         //         }
//         //         else
//         //         {
//         //             baseTransform = GetMainTower();
//         //         }
//         //     }

//         //     // If there is still a tower alive, go to it. Otherwise stop.
//         //     currentState = EnemyState.GoToBase;
//         //     return;
//         // }

//         if (TowerIsInvalidOrDead(baseTransform))
//         {
//             baseTransform = ChooseTowerTarget();
//             currentState = EnemyState.GoToBase;
//             return;
//         }

//         // -------------------- CHANGES END --------------------

//         Debug.Log("Attacking base. detectedObject = " + (sightSensor != null ? sightSensor.detectedObject : null));
//         agent.isStopped = true;

//         AimAt(baseTransform);
//         EnemyShoot();

//         // Keep behavior: if player gets detected while attacking tower, switch to chasing player.
//         if (sightSensor != null && sightSensor.detectedObject != null)
//         {
//             currentState = EnemyState.ChasePlayer;
//             print("Changing to Chase Player state");
//         }
//     }

//     void ChasePlayer()
//     {
//         agent.isStopped = false;
//         print("Chasing player.");
//         agent.SetDestination(playerTransform.position);

//         Transform chaseTarget = (sightSensor != null && sightSensor.detectedObject != null)
//             ? sightSensor.detectedObject.transform
//             : playerTransform;

//         AimAt(chaseTarget);

//         if (sightSensor == null || sightSensor.detectedObject == null)
//         {
//             currentState = EnemyState.GoToBase;
//             print("Changing to Go To Base state");
//             return;
//         }

//         float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
//         if (distanceToPlayer < playerAttackDistance)
//         {
//             currentState = EnemyState.AttackPlayer;
//             print("Changing to Attack Player state");
//         }
//     }

//     void AttackPlayer()
//     {
//         agent.isStopped = true;

//         if (sightSensor != null && sightSensor.detectedObject != null)
//         {
//             AimAt(sightSensor.detectedObject.transform);
//         }

//         EnemyShoot();
//         print("Attacking player.");

//         if (sightSensor == null || sightSensor.detectedObject == null)
//         {
//             currentState = EnemyState.GoToBase;
//             print("Changing to Go To Base state");
//             return;
//         }

//         float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
//         if (distanceToPlayer > playerAttackDistance * 1.1f)
//         {
//             currentState = EnemyState.ChasePlayer;
//             print("Changing to Chase Player state");
//         }
//     }

//     void AimAt(Transform target)
//     {
//         if (target == null) return;

//         Vector3 toTarget = target.position - transform.position;
//         toTarget.y = 0f;

//         if (toTarget.sqrMagnitude < 0.0001f) return;

//         Quaternion desiredRotation = Quaternion.LookRotation(toTarget);
//         transform.rotation = Quaternion.RotateTowards(
//             transform.rotation,
//             desiredRotation,
//             turnSpeed * Time.deltaTime
//         );
//     }

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

//     // -------------------- CHANGES START --------------------
//     // CHANGE: Returns true if tower is null, missing Life, or already dead.
//     // This lets enemies stop shooting when a tower gets destroyed and immediately retarget.
//     bool TowerIsInvalidOrDead(Transform tower)
// {
//     if (tower == null) return true;

//     // CHANGE: If Life is missing, assume tower is alive (so enemies still move).
//     // If you WANT towers to be destroyable, add Life to them.
//     Life life = tower.GetComponent<Life>();
//     if (life == null) return false;

//     return life.amount <= 0f;
// }


//     // CHANGE: Finds the nearest tower tagged "Tower" that is still alive (Life.amount > 0).
//     Transform GetNearestAliveTower()
// {
//     GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);

//     // CHANGE: If you forgot to tag towers, fall back to the old single "Tower" by name
//     // so enemies still move.
//     if (towers == null || towers.Length == 0)
//     {
//         var towerObj = GameObject.Find("Tower");
//         return towerObj != null ? towerObj.transform : null;
//     }

//     Transform best = null;
//     float bestDist = float.PositiveInfinity;

//     for (int i = 0; i < towers.Length; i++)
//     {
//         if (towers[i] == null) continue;

//         Transform t = towers[i].transform;

//         // CHANGE: If Life exists, require it to be alive. If Life doesn't exist, treat as alive.
//         Life life = t.GetComponent<Life>();
//         if (life != null && life.amount <= 0f) continue;

//         float d = (t.position - transform.position).sqrMagnitude;
//         if (d < bestDist)
//         {
//             bestDist = d;
//             best = t;
//         }
//     }

//     return best;
// }

//     bool AnyNormalTowerAlive()
//     {
//         GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);
//         for (int i = 0; i < towers.Length; i++)
//         {
//             if (towers[i] == null) continue;
//             Life life = towers[i].GetComponent<Life>();
//             if (life != null && life.amount > 0f) return true;
//         }
//         return false;
//     }

//     Transform GetMainTower()
//     {
//         GameObject main = GameObject.FindGameObjectWithTag(mainTowerTag);
//         return main != null ? main.transform : null;
//     }

//     Transform ChooseTowerTarget()
// {
//     if (AnyNormalTowerAlive())
//     {
//         return GetNearestAliveTower();
//     }
//     else
//     {
//         return GetMainTower();
//     }
// }



//     // -------------------- CHANGES END --------------------
// }






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

//     public Transform baseTransform;      // Current tower target (we will update this when towers die)
//     public Transform playerTransform;    // Player target

//     public float baseAttackDistance;
//     public float playerAttackDistance;
//     private NavMeshAgent agent;

//     [Header("Aiming")]
//     [Tooltip("How fast the enemy turns to face the target (degrees per second).")]
//     public float turnSpeed = 720f;

//     [Header("Towers")]
//     [Tooltip("All tower objects should be tagged with this (e.g., Tower).")]
//     public string towerTag = "Tower";

//     // Main/boss tower tag (only becomes active once all normal towers are dead)
//     public string mainTowerTag = "MainTower";

//     private void Awake()
//     {
//         // PHASE LOGIC: choose outer towers first, main tower only after they are destroyed
//         baseTransform = ChooseTowerTarget();

//         if (baseTransform == null)
//         {
//             // Fallback to your old behavior if you forgot to tag towers.
//             var towerObj = GameObject.Find("Tower");
//             if (towerObj != null) baseTransform = towerObj.transform;
//         }

//         playerTransform = GameObject.Find("Player").transform;
//         agent = GetComponent<NavMeshAgent>();
//         if (agent == null) agent = GetComponentInParent<NavMeshAgent>();

//         currentState = EnemyState.GoToBase; // ensure they start walking
//         agent.isStopped = false;            // ensure agent is allowed to move
//     }

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
//         Debug.Log($"GoToBase START | state={currentState} | agentNull={agent==null} | stopped={(agent!=null && agent.isStopped)} | onNavMesh={(agent!=null && agent.isOnNavMesh)} | tower={(baseTransform ? baseTransform.name : "NULL")}");

//         // Retarget only when we don't have a valid target.
//         if (baseTransform == null || TowerIsInvalidOrDead(baseTransform))
//         {
//             baseTransform = ChooseTowerTarget();

//             if (baseTransform == null)
//             {
//                 agent.isStopped = true;
//                 return;
//             }
//         }

//         agent.isStopped = false;
//         agent.SetDestination(baseTransform.position);
//         print("Going to base.");

//         AimAt(baseTransform);

//         // Keep behavior: if player detected before reaching tower, chase player.
//         if (sightSensor != null && sightSensor.detectedObject != null)
//         {
//             currentState = EnemyState.ChasePlayer;
//             print("Changing to Chase Player state");
//         }

//         // -------------------- CHANGES START --------------------
//         // CHANGE: Use distance to the tower's *collider surface* (closest point), not the tower center.
//         // This fixes large MainTower where the pivot/center is far away and enemies never entered AttackBase.
//         float distanceToBase = DistanceToTargetSurface(baseTransform);
//         // -------------------- CHANGES END --------------------

//         if (distanceToBase < baseAttackDistance)
//         {
//             currentState = EnemyState.AttackBase;
//             print("Changing to Attack Base state");
//         }
//     }

//     void AttackBase()
//     {
//         // If target got destroyed, stop attacking and retarget.
//         if (TowerIsInvalidOrDead(baseTransform))
//         {
//             baseTransform = ChooseTowerTarget();
//             currentState = EnemyState.GoToBase;
//             return;
//         }

//         // -------------------- CHANGES START --------------------
//         // CHANGE: If enemies get pushed away from the tower, go back to GoToBase to reposition.
//         float distanceToBase = DistanceToTargetSurface(baseTransform);
//         if (distanceToBase > baseAttackDistance * 1.1f)
//         {
//             currentState = EnemyState.GoToBase;
//             return;
//         }
//         // -------------------- CHANGES END --------------------

//         Debug.Log("Attacking base. detectedObject = " + (sightSensor != null ? sightSensor.detectedObject : null));
//         agent.isStopped = true;

//         AimAt(baseTransform);
//         EnemyShoot();

//         // Keep behavior: if player gets detected while attacking tower, switch to chasing player.
//         if (sightSensor != null && sightSensor.detectedObject != null)
//         {
//             currentState = EnemyState.ChasePlayer;
//             print("Changing to Chase Player state");
//         }
//     }

//     void ChasePlayer()
//     {
//         agent.isStopped = false;
//         print("Chasing player.");
//         agent.SetDestination(playerTransform.position);

//         Transform chaseTarget = (sightSensor != null && sightSensor.detectedObject != null)
//             ? sightSensor.detectedObject.transform
//             : playerTransform;

//         AimAt(chaseTarget);

//         if (sightSensor == null || sightSensor.detectedObject == null)
//         {
//             currentState = EnemyState.GoToBase;
//             print("Changing to Go To Base state");
//             return;
//         }

//         float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
//         if (distanceToPlayer < playerAttackDistance)
//         {
//             currentState = EnemyState.AttackPlayer;
//             print("Changing to Attack Player state");
//         }
//     }

//     void AttackPlayer()
//     {
//         agent.isStopped = true;

//         if (sightSensor != null && sightSensor.detectedObject != null)
//         {
//             AimAt(sightSensor.detectedObject.transform);
//         }

//         EnemyShoot();
//         print("Attacking player.");

//         if (sightSensor == null || sightSensor.detectedObject == null)
//         {
//             currentState = EnemyState.GoToBase;
//             print("Changing to Go To Base state");
//             return;
//         }

//         float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
//         if (distanceToPlayer > playerAttackDistance * 1.1f)
//         {
//             currentState = EnemyState.ChasePlayer;
//             print("Changing to Chase Player state");
//         }
//     }

//     void AimAt(Transform target)
//     {
//         if (target == null) return;

//         Vector3 toTarget = target.position - transform.position;
//         toTarget.y = 0f;

//         if (toTarget.sqrMagnitude < 0.0001f) return;

//         Quaternion desiredRotation = Quaternion.LookRotation(toTarget);
//         transform.rotation = Quaternion.RotateTowards(
//             transform.rotation,
//             desiredRotation,
//             turnSpeed * Time.deltaTime
//         );
//     }

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

//     // -------------------- CHANGES START --------------------
//     // CHANGE: Distance to the target *surface* (closest point on its collider), not its center.
//     // This fixes large MainTower where enemies stand near the wall but are far from the pivot.
//     float DistanceToTargetSurface(Transform target)
//     {
//         if (target == null) return float.PositiveInfinity;

//         Collider col = target.GetComponentInChildren<Collider>();
//         if (col != null)
//         {
//             Vector3 closest = col.ClosestPoint(transform.position);
//             return Vector3.Distance(transform.position, closest);
//         }

//         return Vector3.Distance(transform.position, target.position);
//     }
//     // -------------------- CHANGES END --------------------

//     bool TowerIsInvalidOrDead(Transform tower)
//     {
//         if (tower == null) return true;

//         // If Life is missing, assume tower is alive (so enemies still move).
//         Life life = tower.GetComponent<Life>();
//         if (life == null) return false;

//         return life.amount <= 0f;
//     }

//     Transform GetNearestAliveTower()
//     {
//         GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);

//         // If you forgot to tag towers, fall back to the old single "Tower" by name
//         if (towers == null || towers.Length == 0)
//         {
//             var towerObj = GameObject.Find("Tower");
//             return towerObj != null ? towerObj.transform : null;
//         }

//         Transform best = null;
//         float bestDist = float.PositiveInfinity;

//         for (int i = 0; i < towers.Length; i++)
//         {
//             if (towers[i] == null) continue;

//             Transform t = towers[i].transform;

//             // If Life exists, require it to be alive. If Life doesn't exist, treat as alive.
//             Life life = t.GetComponent<Life>();
//             if (life != null && life.amount <= 0f) continue;

//             float d = (t.position - transform.position).sqrMagnitude;
//             if (d < bestDist)
//             {
//                 bestDist = d;
//                 best = t;
//             }
//         }

//         return best;
//     }

//     bool AnyNormalTowerAlive()
//     {
//         GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);
//         for (int i = 0; i < towers.Length; i++)
//         {
//             if (towers[i] == null) continue;
//             Life life = towers[i].GetComponent<Life>();
//             if (life != null && life.amount > 0f) return true;
//         }
//         return false;
//     }

//     Transform GetMainTower()
//     {
//         GameObject main = GameObject.FindGameObjectWithTag(mainTowerTag);
//         return main != null ? main.transform : null;
//     }

//     Transform ChooseTowerTarget()
//     {
//         if (AnyNormalTowerAlive())
//         {
//             return GetNearestAliveTower();
//         }
//         else
//         {
//             return GetMainTower();
//         }
//     }
// }








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

//     public Transform baseTransform;      // Current tower target (normal towers first, then main tower)
//     public Transform playerTransform;    // Player target

//     public float baseAttackDistance;
//     public float playerAttackDistance;
//     private NavMeshAgent agent;

//     [Header("Aiming")]
//     [Tooltip("How fast the enemy turns to face the target (degrees per second).")]
//     public float turnSpeed = 720f;

//     [Header("Towers")]
//     [Tooltip("The 4 outer towers must be tagged with this.")]
//     public string towerTag = "Tower";

//     [Tooltip("The final/boss tower must be tagged with this (and should NOT have any children tagged 'Tower').")]
//     public string mainTowerTag = "MainTower";

//     private void Awake()
//     {
//         baseTransform = ChooseTowerTarget();

//         // Fallback to old behavior if tags aren't set (optional safety)
//         if (baseTransform == null)
//         {
//             var towerObj = GameObject.Find("Tower");
//             if (towerObj != null) baseTransform = towerObj.transform;
//         }

//         playerTransform = GameObject.Find("Player").transform;

//         agent = GetComponent<NavMeshAgent>();
//         if (agent == null) agent = GetComponentInParent<NavMeshAgent>();

//         currentState = EnemyState.GoToBase; // ensure they start walking
//         if (agent != null) agent.isStopped = false;
//     }

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
//         // Retarget only when we don't have a valid target.
//         if (baseTransform == null || TowerIsInvalidOrDead(baseTransform))
//         {
//             baseTransform = ChooseTowerTarget();

//             if (baseTransform == null)
//             {
//                 if (agent != null) agent.isStopped = true;
//                 return;
//             }
//         }

//         if (agent == null) return;

//         agent.isStopped = false;
//         agent.SetDestination(baseTransform.position);
//         print("Going to base.");

//         AimAt(baseTransform);

//         // If player detected before reaching tower, chase player.
//         if (sightSensor != null && sightSensor.detectedObject != null)
//         {
//             currentState = EnemyState.ChasePlayer;
//             print("Changing to Chase Player state");
//         }

//         // CHANGE: Use distance to the target collider surface, not the center.
//         float distanceToBase = DistanceToTargetSurface(baseTransform);
//         if (distanceToBase < baseAttackDistance)
//         {
//             currentState = EnemyState.AttackBase;
//             print("Changing to Attack Base state");
//         }
//     }

//     void AttackBase()
//     {
//         // If target got destroyed, stop attacking and retarget.
//         if (TowerIsInvalidOrDead(baseTransform))
//         {
//             baseTransform = ChooseTowerTarget();
//             currentState = EnemyState.GoToBase;
//             return;
//         }

//         // CHANGE: If enemies get pushed away, go back to GoToBase to reposition.
//         float distanceToBase = DistanceToTargetSurface(baseTransform);
//         if (distanceToBase > baseAttackDistance * 1.1f)
//         {
//             currentState = EnemyState.GoToBase;
//             return;
//         }

//         if (agent != null) agent.isStopped = true;

//         AimAt(baseTransform);
//         EnemyShoot();

//         // If player gets detected while attacking tower, switch to chasing player.
//         if (sightSensor != null && sightSensor.detectedObject != null)
//         {
//             currentState = EnemyState.ChasePlayer;
//             print("Changing to Chase Player state");
//         }
//     }

//     void ChasePlayer()
//     {
//         if (agent == null) return;

//         agent.isStopped = false;
//         print("Chasing player.");
//         agent.SetDestination(playerTransform.position);

//         Transform chaseTarget = (sightSensor != null && sightSensor.detectedObject != null)
//             ? sightSensor.detectedObject.transform
//             : playerTransform;

//         AimAt(chaseTarget);

//         if (sightSensor == null || sightSensor.detectedObject == null)
//         {
//             currentState = EnemyState.GoToBase;
//             print("Changing to Go To Base state");
//             return;
//         }

//         float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
//         if (distanceToPlayer < playerAttackDistance)
//         {
//             currentState = EnemyState.AttackPlayer;
//             print("Changing to Attack Player state");
//         }
//     }

//     void AttackPlayer()
//     {
//         if (agent != null) agent.isStopped = true;

//         if (sightSensor != null && sightSensor.detectedObject != null)
//         {
//             AimAt(sightSensor.detectedObject.transform);
//         }

//         EnemyShoot();
//         print("Attacking player.");

//         if (sightSensor == null || sightSensor.detectedObject == null)
//         {
//             currentState = EnemyState.GoToBase;
//             print("Changing to Go To Base state");
//             return;
//         }

//         float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
//         if (distanceToPlayer > playerAttackDistance * 1.1f)
//         {
//             currentState = EnemyState.ChasePlayer;
//             print("Changing to Chase Player state");
//         }
//     }

//     void AimAt(Transform target)
//     {
//         if (target == null) return;

//         Vector3 toTarget = target.position - transform.position;
//         toTarget.y = 0f;

//         if (toTarget.sqrMagnitude < 0.0001f) return;

//         Quaternion desiredRotation = Quaternion.LookRotation(toTarget);
//         transform.rotation = Quaternion.RotateTowards(
//             transform.rotation,
//             desiredRotation,
//             turnSpeed * Time.deltaTime
//         );
//     }

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

//     // CHANGE: Distance to the target *surface* (closest point on its collider), not its center.
//     float DistanceToTargetSurface(Transform target)
//     {
//         if (target == null) return float.PositiveInfinity;

//         Collider col = target.GetComponentInChildren<Collider>();
//         if (col != null)
//         {
//             Vector3 closest = col.ClosestPoint(transform.position);
//             return Vector3.Distance(transform.position, closest);
//         }

//         return Vector3.Distance(transform.position, target.position);
//     }

//     // Returns true if target is null or has Life.amount <= 0 (Life can be on parent/children too).
//     bool TowerIsInvalidOrDead(Transform tower)
//     {
//         if (tower == null) return true;

//         Life life = tower.GetComponent<Life>();
//         if (life == null) life = tower.GetComponentInParent<Life>();
//         if (life == null) life = tower.GetComponentInChildren<Life>();

//         // If Life is missing, assume alive (prevents freezing if setup is imperfect).
//         if (life == null) return false;

//         return life.amount <= 0f;
//     }

//     // Phase selection: normal towers first; main tower only once no normal tower is alive.
//     Transform ChooseTowerTarget()
//     {
//         if (AnyNormalTowerAlive())
//             return GetNearestAliveTower();

//         return GetMainTower();
//     }

//     // True if at least one normal tower (tagged Tower) is alive.
//     // NOTE: Life can be on parent/children; also excludes MainTower variants even if a child is tagged wrong.
//     bool AnyNormalTowerAlive()
//     {
//         GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);
//         for (int i = 0; i < towers.Length; i++)
//         {
//             if (towers[i] == null) continue;

//             // Exclude any tower object that belongs to a MainTower hierarchy (prefab variant safety).
//             Transform root = towers[i].transform.root;
//             if (root != null && root.CompareTag(mainTowerTag)) continue;

//             Life life = towers[i].GetComponent<Life>();
//             if (life == null) life = towers[i].GetComponentInParent<Life>();
//             if (life == null) life = towers[i].GetComponentInChildren<Life>();

//             if (life != null && life.amount > 0f) return true;
//         }
//         return false;
//     }

//     // Finds the nearest alive normal tower.
//     // Excludes MainTower prefab variants even if a child is incorrectly tagged "Tower".
//     Transform GetNearestAliveTower()
//     {
//         GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);

//         if (towers == null || towers.Length == 0)
//         {
//             var towerObj = GameObject.Find("Tower");
//             return towerObj != null ? towerObj.transform : null;
//         }

//         Transform best = null;
//         float bestDist = float.PositiveInfinity;

//         for (int i = 0; i < towers.Length; i++)
//         {
//             if (towers[i] == null) continue;

//             // Prefab variant safety: if this "Tower" is part of a MainTower hierarchy, ignore it.
//             Transform root = towers[i].transform.root;
//             if (root != null && root.CompareTag(mainTowerTag)) continue;

//             Transform t = towers[i].transform;

//             Life life = t.GetComponent<Life>();
//             if (life == null) life = t.GetComponentInParent<Life>();
//             if (life == null) life = t.GetComponentInChildren<Life>();

//             if (life != null && life.amount <= 0f) continue;

//             float d = (t.position - transform.position).sqrMagnitude;
//             if (d < bestDist)
//             {
//                 bestDist = d;
//                 best = t;
//             }
//         }

//         return best;
//     }

//     Transform GetMainTower()
//     {
//         GameObject main = GameObject.FindGameObjectWithTag(mainTowerTag);
//         return main != null ? main.transform : null;
//     }
// }





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

    private void Awake()
    {
        baseTransform = ChooseTowerTarget();

        // Fallback to old behavior if tags aren't set (optional safety)
        if (baseTransform == null)
        {
            var towerObj = GameObject.Find("Tower");
            if (towerObj != null) baseTransform = towerObj.transform;
        }

        playerTransform = GameObject.Find("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        if (agent == null) agent = GetComponentInParent<NavMeshAgent>();

        currentState = EnemyState.GoToBase; // ensure they start walking
        if (agent != null) agent.isStopped = false;
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
        // Retarget only when we don't have a valid target.
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

        // If player detected before reaching tower, chase player.
        if (sightSensor != null && sightSensor.detectedObject != null)
        {
            currentState = EnemyState.ChasePlayer;
            print("Changing to Chase Player state");
        }

        // Distance to the target *surface* (closest point on its collider), not its center.
        float distanceToBase = DistanceToTargetSurface(baseTransform);
        if (distanceToBase < baseAttackDistance)
        {
            currentState = EnemyState.AttackBase;
            print("Changing to Attack Base state");
        }
    }

    void AttackBase()
    {
        // If target got destroyed, stop attacking and retarget.
        if (TowerIsInvalidOrDead(baseTransform))
        {
            baseTransform = ChooseTowerTarget();
            currentState = EnemyState.GoToBase;
            return;
        }

        // If enemies get pushed away, go back to GoToBase to reposition.
        float distanceToBase = DistanceToTargetSurface(baseTransform);
        if (distanceToBase > baseAttackDistance * 1.1f)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        if (agent != null) agent.isStopped = true;

        AimAt(baseTransform);
        EnemyShoot();

        // If player gets detected while attacking tower, switch to chasing player.
        if (sightSensor != null && sightSensor.detectedObject != null)
        {
            currentState = EnemyState.ChasePlayer;
            print("Changing to Chase Player state");
        }
    }

    void ChasePlayer()
    {
        if (agent == null) return;

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

        EnemyShoot();
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

    [SerializeField] private float fireRate = 1f;
    private float lastShootTime = -999f;
    public GameObject bulletPrefab;

    void EnemyShoot()
    {
        float timeSinceLastShot = Time.time - lastShootTime;

        if (timeSinceLastShot >= fireRate)
        {
            lastShootTime = Time.time;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    // Distance to the target *surface* (closest point on its collider), not its center.
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

    // Returns true if target is null or has Life.amount <= 0 (Life can be on parent/children too).
    bool TowerIsInvalidOrDead(Transform tower)
    {
        if (tower == null) return true;

        Life life = tower.GetComponent<Life>();
        if (life == null) life = tower.GetComponentInParent<Life>();
        if (life == null) life = tower.GetComponentInChildren<Life>();

        // If Life is missing, assume alive (prevents freezing if setup is imperfect).
        if (life == null) return false;

        return life.amount <= 0f;
    }

    // -------------------- CHANGES START --------------------
    // CHANGE: Replaces transform.root checks with a precise parent-walk check.
    // Using transform.root can accidentally exclude ALL towers if they share a common scene root.
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
    // -------------------- CHANGES END --------------------

    // Phase selection: normal towers first; main tower only once no normal tower is alive.
    Transform ChooseTowerTarget()
    {
        if (AnyNormalTowerAlive())
            return GetNearestAliveTower();

        return GetMainTower();
    }

    // True if at least one normal tower (tagged Tower) is alive.
    bool AnyNormalTowerAlive()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);
        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i] == null) continue;

            // -------------------- CHANGES START --------------------
            // CHANGE: Exclude ONLY objects that are actually under the MainTower hierarchy.
            if (IsPartOfMainTowerHierarchy(towers[i])) continue;
            // -------------------- CHANGES END --------------------

            Life life = towers[i].GetComponent<Life>();
            if (life == null) life = towers[i].GetComponentInParent<Life>();
            if (life == null) life = towers[i].GetComponentInChildren<Life>();

            if (life != null && life.amount > 0f) return true;
        }
        return false;
    }

    // Finds the nearest alive normal tower.
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

            // -------------------- CHANGES START --------------------
            // CHANGE: Exclude ONLY objects that are actually under the MainTower hierarchy.
            if (IsPartOfMainTowerHierarchy(towers[i])) continue;
            // -------------------- CHANGES END --------------------

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
