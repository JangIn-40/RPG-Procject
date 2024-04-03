using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 2f;

        Fighter fighter;
        Health health;
        Mover mover;
        ActionScheduler actionScheduler;
        GameObject player;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            actionScheduler = GetComponent<ActionScheduler>();
            player = GameObject.FindWithTag("Player");
            guardPosition = transform.position;
        }

        void Update()
        {
            if(health.IsDie()) { return; }
            if(InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                fighter.Attack(player);
            }
            //의심상태 suspicion
            else if(timeSinceLastSawPlayer < suspicionTime)
            {
                actionScheduler.CancelCurrentAction();
            }
            else
            {
                mover.StartMoveToAction(guardPosition);
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        //Called by Untiy
        void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
