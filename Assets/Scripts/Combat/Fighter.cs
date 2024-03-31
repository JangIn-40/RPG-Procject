using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;

namespace RPG.Combat 
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;

        Mover mover;
        Transform target;

        void Start()
        {
            mover = GetComponent<Mover>();
        }

        void Update()
        {
            if(target == null) { return; }

            bool isInRange = Vector3.Distance(transform.position, target.position) < weaponRange;
            if(target != null && !isInRange)
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Stop();
            }
            
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }     

        public void Cancel()
        {
            target = null;
        }
    }
}
