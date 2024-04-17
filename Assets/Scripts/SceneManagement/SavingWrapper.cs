using System;
using UnityEngine;
using RPG.Saving;
using System.Collections;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour 
    {
        const string defaultSavingFile = "save";
        [SerializeField] float fadeInTime = 0.2f;

        IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSavingFile);
            yield return fader.FadeIn(fadeInTime);
        }

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
            if(Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
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

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSavingFile);
        }
    }
}

