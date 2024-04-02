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
        Health target;

        Animator animator;

        float timeSinceLastAttack = 0;

        void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            

            if(target == null) { return; }
            if(target.IsDie())  { return; }
            {

            }

            bool isInRange = Vector3.Distance(transform.position, target.transform.position) < weaponRange;
            if(!isInRange)
            {
                mover.MoveTo(target.transform.position);

            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }

        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > TimeBetweenAttack)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        //Animation Hit
        void Hit()
        {
            
            target.TakeDamage(weaponDamage);
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }     

        public void Cancel()
        {
            animator.SetTrigger("stopAttack");
            target = null;
        }


    }
}
