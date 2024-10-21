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

        [Header("Runtime Info")]
        [ReadOnly, SerializeField] int requiredItems;
        [ReadOnly, SerializeField] int itemsFound;

        public void SetupRoom(GameObject[] itemsToSpawn, int amountOfItems)
        {
            requiredItems = amountOfItems;

            for (int i = 0; i < amountOfItems; i++)
            {
                Transform randomSpawnPos = itemSpawnPositions[Random.Range(0, itemSpawnPositions.Count)];

                Instantiate(itemsToSpawn[Random.Range(0, itemsToSpawn.Length)], randomSpawnPos.position, Quaternion.identity);

                itemSpawnPositions.Remove(randomSpawnPos);
            }
        }

        void FoundItem()
        {

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