using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game.Pooling
{
    [CreateAssetMenu(fileName = "PoolObject", menuName = "ScriptableObjects/PoolObject", order = 2)]
    public class PooledObjectSO : ScriptableObject
    {
        public string poolName;
        [Space]
        public int amount;
        public List<GameObject> objects = new List<GameObject>();

        [HideInInspector] public List<GameObject> spawnedObjects = new List<GameObject>();
    }
}