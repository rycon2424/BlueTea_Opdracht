using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Interactables
{
    public class WorldEventTrigger : MonoBehaviour
    {
        [SerializeField] UnityEvent onTriggerEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onTriggerEnter?.Invoke();
            }
        }
    }
}