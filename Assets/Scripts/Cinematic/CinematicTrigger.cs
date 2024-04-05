using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic
{
    public class CinematicTrigger : MonoBehaviour
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
    }
}

