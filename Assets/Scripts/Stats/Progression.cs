using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject 
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            foreach(ProgressionCharacterClass progressionClass in characterClasses)
            {
                if(progressionClass.characterClass != characterClass) continue;

                foreach (ProgreesionStat progressionStat in progressionClass.stats)
                {
                    if(progressionStat.stat != stat) continue;

                    if(progressionStat.level.Length < level) continue;
                    return progressionStat.level[level - 1];
                }

            }
            return 0;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgreesionStat[] stats; 
        }

        [System.Serializable]
        class ProgreesionStat
        {
            public float[] level;
            public Stat stat;
        }

    }
}