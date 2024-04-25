using RPG.Stats;
using RPG.Saving;
using UnityEngine;
using RPG.Core;
using System;
using GameDevTV.Utils;

namespace RPG.Attributes{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;

        LazyValue<float> health;

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
            health = new LazyValue<float>(GetInitialHealth);
        }

        float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        void Start()
        {
            health.ForceInit(); 
        }

        void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage : " + damage);

            health.value = Mathf.Max(health.value - damage, 0);
            if(health.value == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetHealthPoints()
        {
            return health.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return health.value / GetComponent<BaseStats>().GetStat(Stat.Health) * 100;
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
            health.value = Mathf.Max(health.value, regenHealthPoints);
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health.value = (float)state;

            if(health.value == 0)
            {
                Die();
            }
        }
    }
}
