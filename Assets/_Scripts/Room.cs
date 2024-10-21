using Game.Interactables;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Rooms
{
    public class Room : MonoBehaviour
    {
        [Header("Room information")]
        [SerializeField] Light roomLight;
        [SerializeField] List<Transform> itemSpawnPositions = new List<Transform>();

        [Header("Unlocked Path")]
        [SerializeField] Animator pathAnimator;
        [SerializeField] string pathTrigger;

        [Header("Runtime Info")]
        [ReadOnly, SerializeField] int requiredItems;
        [ReadOnly, SerializeField] int itemsFound;

        public void SetupRoom(GameObject[] itemsToSpawn, int amountOfItems)
        {
            requiredItems = amountOfItems;

            for (int i = 0; i < amountOfItems; i++)
            {
                Transform randomSpawnPos = itemSpawnPositions[Random.Range(0, itemSpawnPositions.Count)];

                PickupAble spawnedPickup =
                    Instantiate(itemsToSpawn[Random.Range(0, itemsToSpawn.Length)], randomSpawnPos.position, Quaternion.identity)
                    .GetComponent<PickupAble>();

                // Assign event so the pickup knows what room it has been picked up
                spawnedPickup.OnObjectTaken += FoundItem;

                itemSpawnPositions.Remove(randomSpawnPos);
            }
        }

        void FoundItem(GameObject item)
        {
            item.SetActive(false);

            itemsFound++;

            CheckForCompletion();
        }

        bool CheckForCompletion()
        {
            if (itemsFound == requiredItems)
            {
                roomLight.color = Color.green;
                pathAnimator.SetTrigger(pathTrigger);
                return true;
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            foreach (var itemPos in itemSpawnPositions)
            {
                Gizmos.DrawCube(itemPos.position, Vector3.one * 0.25f);
            }
        }
    }
}