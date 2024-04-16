using UnityEngine;
using TMPro;
using System;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour 
    {
        BaseStats baseStats;
        TextMeshProUGUI healthValue;

        void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            healthValue = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            healthValue.text = String.Format("{0:0}", baseStats.GetLevel());
        }

    }
}
