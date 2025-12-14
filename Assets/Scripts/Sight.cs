using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
public class Sight : MonoBehaviour
{
    public float distance;
    public float angle;

    public Collider detectedObject;

    public LayerMask objectsLayers;
    public LayerMask obstaclesLayers;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
        Gizmos.color = Color.blue;
        Vector3 rightDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, rightDirection * distance);
        Vector3 leftDirection = Quaternion.Euler(0, -angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDirection * distance);
    }



    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, (int)objectsLayers);   // distance == radius of field
        for (int i = 0; i < colliders.Length; i++)
        {
            Collider collider = colliders[i];
            print(collider.name);       // Enters sphere
            Vector3 directionToController =
            // Vector3.Normalize(collider.transform.position - transform.position);
            Vector3.Normalize(collider.bounds.center - transform.position);
            float angleToController = Vector3.Angle(transform.forward, directionToController);
            if (angleToController < angle )
            {
                if (!Physics.Linecast(transform.position, collider.bounds.center, out RaycastHit hit, obstaclesLayers))    // collider.transform.position --> Apunta al piso
                {
                    
                    Debug.DrawLine(transform.position, collider.bounds.center, Color.green);
                    //Debug.Log("I see you " + collider.name);
                    detectedObject = collider;
                    break;      // We're using for-loop; We can execute this functionality but since we're using for-loop, break is implemented
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }
            // else
            // {
            //     Debug.DrawLine(transform.position,HierarchyType.poin)
            // }
            // collider.transform.position = A; transform.position = B;  A - B would be the direction B - A
        }
        // foreach(Collider collider in colliders)
    }

}


//     void Update()
// {
//     // 1. Draw the forward direction (cyan)
//     Debug.DrawRay(transform.position, transform.forward * distance, Color.cyan);

//     // 2. Sight logic...
//     Collider[] colliders = Physics.OverlapSphere(transform.position, distance, objectsLayers);
//     detectedObject = null;

//     for (int i = 0; i < colliders.Length; i++)
//     {
//         Collider collider = colliders[i];

//         Vector3 targetPoint = collider.bounds.center; // Aim at center of collider
//         Vector3 directionToController = Vector3.Normalize(targetPoint - transform.position);


//         Vector3 directionToController =
//             Vector3.Normalize(collider.transform.position - transform.position);

//         float angleToController = Vector3.Angle(transform.forward, directionToController);

//         // 3. Draw a ray to the collider (magenta)
//         Debug.DrawRay(transform.position, directionToController * distance, Color.magenta);

//         Debug.Log("Angle to " + collider.name + " = " + angleToController);

//         if (angleToController < angle)
//         {
//             if (!Physics.Linecast(transform.position, collider.bounds.center, out RaycastHit hit, obstaclesLayers))
//             {
//                 // (Optional) Draw green line for successful detection
//                 Debug.DrawLine(transform.position, collider.bounds.center, Color.green);
//                 detectedObject = collider;
//                 break;
//             }
//             else
//             {
//                 // (Optional) Draw red line for blocked LOS
//                 Debug.DrawLine(transform.position, hit.point, Color.red);
//             }
//         }
//     }
// }