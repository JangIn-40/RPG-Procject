using RPG.Cinematic;
using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic
{
    public class CinematicControlRemover : MonoBehaviour 
    {
		GameObject player;
        void Awake()
		{
			player = GameObject.FindWithTag("Player");
		}

        void OnEnable()
        {
            GetComponent<PlayableDirector>().played += DisalbeControl;
			GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisalbeControl;
			GetComponent<PlayableDirector>().stopped -= EnableControl;
        }

        void DisalbeControl(PlayableDirector nonsense)
        {

			player.GetComponent<ActionScheduler>().CancelCurrentAction();
			player.GetComponent<PlayerController>().enabled = false;
        }
        void EnableControl(PlayableDirector nonsense)
        {
			print("EnableControl");
			player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
