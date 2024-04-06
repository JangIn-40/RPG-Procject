using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour 
    {
        [SerializeField] int sceneToLoad = -1;

		
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
			yield return SceneManager.LoadSceneAsync(sceneToLoad);
			print("Scene Loaded");
			Destroy(this.gameObject);
        }
    }
}
