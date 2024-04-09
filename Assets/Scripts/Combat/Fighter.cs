using UnityEngine;
using RPG.Movement;
using RPG.Core;


namespace RPG.Combat 
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float TimeBetweenAttack = 1f;
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon weapon = null;

        Mover mover;
        Health target;
        Animator animator;

        float timeSinceLastAttack = 0;

        void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            SpawnWeapon();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            
            if(target == null) { return; }
            if(target.IsDie())  { return; }

            bool isInRange = Vector3.Distance(transform.position, target.transform.position) < weapon.GetRange();
            if(!isInRange)
            {
                mover.MoveTo(target.transform.position, 1f);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        void SpawnWeapon()
        {
            if(weapon == null) return;
            weapon.Spwan(handTransform, animator);
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > TimeBetweenAttack)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //Animation Hit
        void Hit()
        {
            if(target == null) { return; }
            target.TakeDamage(weapon.GetDamage());
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }     

        public void Cancel()
        {
            StopAttack();
            target = null;
            mover.Cancel();
        }

        private void StopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDie();
        }

    }
}
