using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Manager
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] TMP_Text popupText;

        private void Start()
        {
            ShowPopup("Find all the rotating art pieces in the room to continue!");
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