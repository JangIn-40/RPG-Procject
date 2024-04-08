using System;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour 
    {
        const string defaultSavingFile = "save";

        void Update() 
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if(Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSavingFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSavingFile);
        }
    }
}

