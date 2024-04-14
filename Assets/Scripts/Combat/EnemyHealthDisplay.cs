using UnityEngine;
using TMPro;
using System;
using RPG.Attributes;


namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour 
    {
        Fighter fighter;
        TextMeshProUGUI healthValue;

        void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            healthValue = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            if(fighter.GetTarget() == null) 
            {
                healthValue.text = "Null";
                return;
            }
            Health health = fighter.GetTarget();
            healthValue.text = String.Format("{0:0}%", health.GetPercentage());
        }
    }
}
