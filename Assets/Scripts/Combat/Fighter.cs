using UnityEngine;
using RPG.Movement;
using RPG.Core;


namespace RPG.Combat 
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 2f;
        [SerializeField] float TimeBetweenAttack = 1f;

        Mover mover;
        Transform target;
        Health health;

        float timeSinceLastAttack = 0;

        void Start()
        {
            mover = GetComponent<Mover>();
            health = FindObjectOfType<Health>();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if(target == null) { return; }

            bool isInRange = Vector3.Distance(transform.position, target.position) < weaponRange;
            if(!isInRange)
            {
                mover.MoveTo(target.position);

            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }

        }

        private void AttackBehaviour()
        {
            if(timeSinceLastAttack > TimeBetweenAttack)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        //Animation Hit
        void Hit()
        {
            health.TakeDamage(weaponDamage);
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }     

        public void Cancel()
        {
            target = null;
        }


    }
}
