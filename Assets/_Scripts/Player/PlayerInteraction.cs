using Game.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Raycast Settings")]
        [SerializeField] LayerMask interactionMask;
        [SerializeField] float interactionRange = 2f;
        [Space]
        [SerializeField] Transform cameraTransform;

        private IHoverAble lastHoveredItem;

        public void CheckForInteractions()
        {
            RaycastHit rayHit;
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 2);

            if (Physics.Raycast(ray, out rayHit, interactionRange, interactionMask))
            {
                StoppedHovering();

                lastHoveredItem = rayHit.transform.GetComponent<IHoverAble>();

                lastHoveredItem?.OnHovered();

                Debug.Log("Hit object: " + rayHit.collider.name);
            }
            else
            {
                StoppedHovering();
            }
        }

        void StoppedHovering()
        {
            if (lastHoveredItem != null)
            {
                lastHoveredItem.OnStoppedHovering();
            }
        }
    }
}