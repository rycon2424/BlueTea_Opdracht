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
        private GameObject currentHittingObject;

        public void CheckForInteractions()
        {
            RaycastHit rayHit;
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

            //Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 2);

            StoppedHovering();
            currentHittingObject = null;

            if (Physics.Raycast(ray, out rayHit, interactionRange, interactionMask))
            {
                lastHoveredItem = rayHit.transform.GetComponent<IHoverAble>();

                lastHoveredItem?.OnHovered();

                currentHittingObject = rayHit.transform.gameObject;
            }
        }

        void StoppedHovering()
        {
            if (lastHoveredItem != null)
            {
                lastHoveredItem.OnStoppedHovering();
            }
        }

        public bool TryPickupItem()
        {
            if (currentHittingObject != null)
            {
                IPickupAble pickup = currentHittingObject.GetComponent<IPickupAble>();

                pickup.OnPickedUp();

                currentHittingObject = null;

                return true;
            }
            return false;
        }
    }
}