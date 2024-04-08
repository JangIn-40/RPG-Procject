using RPG.Saving;
using UnityEngine;

namespace RPG.Core{
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

        void Awake()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if(health == 0)
            {
                Die();
            }

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
