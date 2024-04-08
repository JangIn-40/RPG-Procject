using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        bool isTrigger = false;

        void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.CompareTag("Player") && !isTrigger)
            {
                isTrigger = true;
                GetComponent<PlayableDirector>().Play();
            }
        }

        public object CaptureState()
        {
            return isTrigger;
        }

        public void RestoreState(object state)
        {
            isTrigger = (bool)state;
        }
    }
}

