using System;
using GameDevTV.Utils;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour 
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifier = false;

        public event Action onLevelUp;

        LazyValue<int> currentLevel;

        Experience experience;

        void Awake() 
        {
            experience = GetComponent<Experience>();
            currentLevel = new LazyValue<int>(CalculateLevel);
        }

        void Start()
        {
            currentLevel.ForceInit();
        }

        void OnEnable()
        {
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
            
        }

        void OnDisable() {
            if (experience != null)
            {
                experience.onExperienceGained -= UpdateLevel;
            }
        }
        void UpdateLevel() 
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);

        }

        public float GetStat(Stat stat)
        {
            return GetBaseStat(stat) + GetAddtiveModifier(stat) * (1 + GetPercentageModifier(stat) / 100);
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            return currentLevel.value;
        }

        private float GetAddtiveModifier(Stat stat)
        {
            if(!shouldUseModifier) return 0;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetPercentageModifier(Stat stat)
        {
            if(!shouldUseModifier) return 0;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();

            if(experience == null) 
            {
                return startingLevel;
            }

            float currentXP = experience.GetExperience();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int levels = 1; levels < penultimateLevel; levels++)
            {
                float xpToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, levels);
                if(xpToLevelUp > currentXP)
                {
                    return levels;
                }
            }
            return penultimateLevel + 1;
        }

    }
}
