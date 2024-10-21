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

        public void SpawnObject(GameObject objectToSpawn, Vector3 position, Vector3 rotation, Vector3 rotationOffset = new Vector3(), float time = 0)
        {
            foreach (PooledObjectSO pool in poolObjects)
            {
                if (pool.objects.Contains(objectToSpawn))
                {
                    int length = pool.spawnedObjects.Count;

                    if (length == 0)
                    {
                        Debug.Log($"The Pool for {objectToSpawn.name} is empty!");
                        return;
                    }

                    HandleObjectSpawning(pool, position, rotation, rotationOffset, time);

                    return;
                }
            }
        }

        public void SpawnObject(string poolName, Vector3 position, Vector3 rotation, Vector3 rotationOffset = new Vector3(), float time = 0)
        {
            foreach (PooledObjectSO pool in poolObjects)
            {
                if (pool.poolName == poolName)
                {
                    int length = pool.spawnedObjects.Count;

                    if (length == 0)
                    {
                        Debug.Log($"The Pool {poolName} is empty!");
                        return;
                    }

                    HandleObjectSpawning(pool, position, rotation, rotationOffset, time);

                    return;
                }
            }
        }

        void HandleObjectSpawning(PooledObjectSO pool, Vector3 position, Vector3 rotation, Vector3 rotationOffset, float time)
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
        }

        IEnumerator ReturnToPool(PooledObjectSO pool, GameObject spawnedObject, float time)
        {
            yield return new WaitForSeconds(time);

            spawnedObject.SetActive(false);

            pool.spawnedObjects.Add(spawnedObject);
        }
    }
}