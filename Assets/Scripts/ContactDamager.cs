// using Unity.VisualScripting;
// using UnityEngine;

// public class ContactDamager : MonoBehaviour
// {
//     public float damage;

//     void OnTriggerEnter(Collider other)
//     {
//         Destroy(gameObject);    // Since it is in bullet, gameObject is the bullet itself

//         Life life = other.GetComponent<Life>();   // It is applied to player objects, it affects them.
//         if (life!=null)     // If you hit objects without Life class applied, returns null (Walls and other objects)
//         {
//             life.amount -= damage;
//         }
//     }
// }


// using UnityEngine;

// public class ContactDamager : MonoBehaviour
// {
//     public float damage = 10f;

//     // Optional but STRONGLY recommended
//     [Tooltip("Set this to avoid damaging the shooter (e.g., Enemy, Player)")]
//     public string ignoreTag;

//     private void OnTriggerEnter(Collider other)
//     {
//         // Ignore self / friendly collisions
//         if (!string.IsNullOrEmpty(ignoreTag) && other.CompareTag(ignoreTag))
//             return;

//         Life life = other.GetComponent<Life>();

//         if (life != null)
//         {
//             life.amount -= damage;
//         }

//         // Destroy AFTER processing hit
//         Destroy(gameObject);
//     }
// }


using UnityEngine;

public class ContactDamager : MonoBehaviour
{
    public float damage;

        void OnTriggerEnter(Collider other)
        {
            Life life = other.GetComponentInParent<Life>();
            if (life == null) return;      // don't delete bullet on non-damageable hits

            life.amount -= damage;
            Destroy(gameObject);
        }
}

// using UnityEngine;

// public class ContactDamager : MonoBehaviour
// {
//     public float damage = 10f;

//     void OnTriggerEnter(Collider other)
//     {
//         Debug.Log($"[Bullet] Hit collider: {other.name} (root: {other.transform.root.name})");

//         Life lifeSame = other.GetComponent<Life>();
//         Life lifeParent = other.GetComponentInParent<Life>();
//         Life lifeRoot = other.transform.root.GetComponent<Life>();

//         Debug.Log($"[Bullet] Life same? {(lifeSame != null)} | parent? {(lifeParent != null)} | root? {(lifeRoot != null)}");

//         if (lifeSame != null) Debug.Log($"[Bullet] same amount before: {lifeSame.amount}");
//         if (lifeParent != null) Debug.Log($"[Bullet] parent amount before: {lifeParent.amount}");
//         if (lifeRoot != null) Debug.Log($"[Bullet] root amount before: {lifeRoot.amount}");

//         // Prefer root life if present (typical Player setup)
//         Life target = lifeRoot != null ? lifeRoot : (lifeParent != null ? lifeParent : lifeSame);

//         if (target != null)
//         {
//             target.amount -= damage;
//             Debug.Log($"[Bullet] Applied {damage}. amount after: {target.amount}");
//         }
//         else
//         {
//             Debug.LogWarning("[Bullet] No Life found anywhere up the hierarchy.");
//         }

//         Destroy(gameObject);
//     }
// }
