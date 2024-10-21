using Game.Player;
using Game.Rooms;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
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

            OnStartGame?.Invoke();
        }

        void SpawnItemsInRooms(int itemsPerRoom)
        {
            foreach (var room in roomsToSetup)
            {
                room.SetupRoom(randomItems, itemsPerRoom);
            }
        }

        [System.Serializable]
        struct GameDifficulties
        {
            public int itemCount;
        }
    }
}
