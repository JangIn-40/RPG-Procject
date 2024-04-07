using System;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour 
    {
        [SerializeField] GameObject persistentObjectPrefab;

        static bool hsaSpwaned = false;

        void Awake()
        {
            if(hsaSpwaned) return;

            SpawnPersistObject();

            hsaSpwaned = true;
        }

        private void SpawnPersistObject()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
