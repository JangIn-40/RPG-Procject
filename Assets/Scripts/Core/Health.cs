using UnityEngine;

namespace RPG.Core{
    public class Health : MonoBehaviour 
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
    }
}
