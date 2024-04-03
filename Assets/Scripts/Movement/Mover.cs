using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;

        NavMeshAgent navMeshAgent;
        Animator animator;
        Health health;

        void Start() 
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            navMeshAgent.enabled = !health.IsDie();
            UpdateAnimator();
        }

        public void StartMoveToAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);

            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelcotiy = transform.InverseTransformDirection(velocity);
            float speed = localVelcotiy.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
