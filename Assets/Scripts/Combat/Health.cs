using UnityEngine;

namespace RPG.Combat{
    public class Health : MonoBehaviour 
    {
        [SerializeField] float health = 100f;

        Animator animator;

        bool isDie = false;
        public bool IsDie()
        {
            return isDie;
        }

        void Start()
        {
            animator = GetComponent<Animator>();
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
        }
    }
}
