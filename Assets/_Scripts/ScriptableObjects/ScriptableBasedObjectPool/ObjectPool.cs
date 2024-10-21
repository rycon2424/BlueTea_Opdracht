using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Pooling
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool instance;
        public List<PooledObjectSO> poolObjects = new List<PooledObjectSO>();

        private void Awake()
        {
            if (instance)
            {
                Destroy(this);
                return;
            }
            instance = this;
        }

        private void Start()
        {
            foreach (PooledObjectSO poolObject in poolObjects)
            {
                poolObject.spawnedObjects.Clear();

                GameObject temp = new GameObject(poolObject.poolName);
                temp.transform.SetParent(transform);

                foreach (GameObject go in poolObject.objects)
                {
                    for (int i = 0; i < poolObject.amount; i++)
                    {
                        GameObject g = Instantiate(go, temp.transform);
                        g.SetActive(false);
                        poolObject.spawnedObjects.Add(g);
                    }
                }
            }
        }

        /// <summary>
        /// Spawn an item using a GameObject as key
        /// </summary>
        public GameObject SpawnObject(GameObject objectToSpawn, Vector3 position, Vector3 rotation, Vector3 rotationOffset = new Vector3(), float time = 0)
        {
            foreach (PooledObjectSO pool in poolObjects)
            {
                if (pool.objects.Contains(objectToSpawn))
                {
                    if (pool.spawnedObjects.Count > 0)
                    {
                        return HandleObjectSpawning(pool, position, rotation, rotationOffset, time);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Spawn an item knowing the name of the pool
        /// </summary>
        public GameObject SpawnObject(string poolName, Vector3 position, Vector3 rotation, Vector3 rotationOffset = new Vector3(), float time = 0)
        {
            foreach (PooledObjectSO pool in poolObjects)
            {
                if (pool.poolName == poolName)
                {
                    if (pool.spawnedObjects.Count > 0)
                    {
                        return HandleObjectSpawning(pool, position, rotation, rotationOffset, time);
                    }
                }
            }
            return null;
        }

        GameObject HandleObjectSpawning(PooledObjectSO pool, Vector3 position, Vector3 rotation, Vector3 rotationOffset, float time)
        {
            GameObject spawnedObject = pool.spawnedObjects[Random.Range(0, pool.spawnedObjects.Count)];

            spawnedObject.transform.position = position;

            Quaternion wantedRotation = Quaternion.LookRotation(rotation);
            spawnedObject.transform.rotation = wantedRotation;
            spawnedObject.transform.eulerAngles += rotationOffset;

            spawnedObject.SetActive(true);

            pool.spawnedObjects.Remove(spawnedObject);

            if (time > 0)
            {
                StartCoroutine(ReturnToPool(pool, spawnedObject, time));
            }

            return spawnedObject;
        }

        IEnumerator ReturnToPool(PooledObjectSO pool, GameObject spawnedObject, float time)
        {
            yield return new WaitForSeconds(time);

            spawnedObject.SetActive(false);

            pool.spawnedObjects.Add(spawnedObject);
        }
    }
}