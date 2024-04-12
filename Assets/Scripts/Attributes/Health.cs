using RPG.Stats;
using RPG.Saving;
using UnityEngine;
using RPG.Core;

namespace RPG.Attributes{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health = 100f;

        Animator animator;
        ActionScheduler actionScheduler;

        bool isDie = false;
        public bool IsDie()
        {
            return isDie;
        }

        void Start()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
            health = GetComponent<BaseStats>().GetHealth();
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if(health == 0)
            {
                Die();
            }

        }

        public float GetPercentage()
        {
            return health / GetComponent<BaseStats>().GetHealth() * 100;
        }

        private void Die()
        {
            if(isDie) { return; }
            isDie = true;
            animator.SetTrigger("die");
            actionScheduler.CancelCurrentAction();
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;

            if(health == 0)
            {
                Die();
            }
        }
    }
}
