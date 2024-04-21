using RPG.Stats;
using RPG.Saving;
using UnityEngine;
using RPG.Core;
using System;

namespace RPG.Attributes{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;

        float health = -1f;

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

        void Start()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            if(health < 0)
            {
                health = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }


        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage : " + damage);

            health = Mathf.Max(health - damage, 0);
            if(health == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetHealthPoints()
        {
            return health;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return health / GetComponent<BaseStats>().GetStat(Stat.Health) * 100;
        }

        void Die()
        {
            if(isDie) { return; }
            isDie = true;
            animator.SetTrigger("die");
            actionScheduler.CancelCurrentAction();
        }

        void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if(experience == null ) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            health = Mathf.Max(health, regenHealthPoints);
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
