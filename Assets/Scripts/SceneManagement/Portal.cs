using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour 
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] float fadeOutTime = 2f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;

		
        void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }    
        }

        IEnumerator Transition()
        {
			DontDestroyOnLoad(this.gameObject);

            Fader fader = FindObjectOfType<Fader>();
            
            yield return fader.FadeOut(fadeOutTime);

            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();

			yield return SceneManager.LoadSceneAsync(sceneToLoad);

            savingWrapper.Load();

			Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);
			Destroy(this.gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this) continue;
                if(portal.destination != destination) continue;
                return portal;
            }
            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }
    }
}
