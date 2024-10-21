using Game.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interactables
{
    public class PickupAble : MonoBehaviour, IPickupAble, IHoverAble
    {
        public delegate void PickedUp(GameObject item);
        public event PickedUp OnObjectTaken;

        public void OnPickedUp()
        {
            OnObjectTaken?.Invoke(gameObject);
        }

        public void OnHovered()
        {
            PopupManager.Singleton.ShowPopup("Press E/F to pickup");
        }

        public void OnStoppedHovering()
        {
            PopupManager.Singleton.HidePopup();
        }
    }
}