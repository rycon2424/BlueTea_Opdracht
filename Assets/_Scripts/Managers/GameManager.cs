using Game.Manager;
using Game.Player;
using Game.Rooms;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Singleton;

        [Header("Game Difficulties")]
        [SerializeField] GameDifficulties[] difficulties;

        [Header("Items")]
        [SerializeField] GameObject[] randomItems;

        [Header("Rooms")]
        [SerializeField] Room[] roomsToSetup;

        [Header("References")]
        [SerializeField] TMP_Text timerText;

        Coroutine gameTimer;

        public delegate void StartGame();
        public event StartGame OnStartGame;

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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        /// <summary>
        /// Called using Unity Button Events
        /// </summary>
        public void StartingGame(int selectedDifficulty)
        {
            GameDifficulties difficultySelected = difficulties[selectedDifficulty];

            SpawnItemsInRooms(difficultySelected.itemCount);

            gameTimer = StartCoroutine(GameTimer());

            OnStartGame?.Invoke();
        }

        void SpawnItemsInRooms(int itemsPerRoom)
        {
            foreach (var room in roomsToSetup)
            {
                room.SetupRoom(randomItems, itemsPerRoom);
            }
        }

        IEnumerator GameTimer()
        {
            float elapsedTime = 0f;
            while (true)
            {
                elapsedTime += Time.deltaTime;

                int minutes = Mathf.FloorToInt(elapsedTime / 60f);
                int seconds = Mathf.FloorToInt(elapsedTime % 60);
                int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000) / 10;

                // Right formatting
                timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);

                yield return null;
            }
        }

        // Called from the trigger events script using Unity Events
        public void FinishedGame()
        {
            StopCoroutine(gameTimer);
            PopupManager.Singleton.ShowPopup($"You won with an amazing time of {timerText.text}!");
        }


        [System.Serializable]
        struct GameDifficulties
        {
            public int itemCount;
        }
    }
}
