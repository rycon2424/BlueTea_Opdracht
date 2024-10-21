using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Manager
{
    public class PopupManager : MonoBehaviour
    {
        public static PopupManager Singleton;

        [SerializeField] TMP_Text popupText;

        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(Singleton.gameObject);
            }
            Singleton = this;
        }

        private void Start()
        {
            ShowPopup("Find and pick up all the rotating art pieces in the room to continue!");
        }

        public void ShowPopup(string textToShow)
        {
            popupText.text = textToShow;
        }

        public void HidePopup()
        {
            popupText.text = string.Empty;
        }
    }
}