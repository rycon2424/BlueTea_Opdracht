using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interactables
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] Transform explosionCenter;
        [SerializeField] float explosionRadius = 5f;
        [SerializeField] float launchForce = 10f;

        // Called from the trigger events script using Unity Events
        [Button]
        public void Launch()
        {
            Collider[] hitColliders = Physics.OverlapSphere(explosionCenter.position, explosionRadius);

            foreach (Collider hit in hitColliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = (hit.transform.position - explosionCenter.position).normalized;

                    rb.AddForce(direction * launchForce, ForceMode.Impulse);
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (explosionCenter != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(explosionCenter.position, explosionRadius);
            }
        }
    }
}