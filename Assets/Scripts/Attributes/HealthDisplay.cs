using UnityEngine;
using TMPro;
using System;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour 
    {
        Health health;
        TextMeshProUGUI healthValue;

        void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            healthValue = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            healthValue.text = String.Format("{0:0}%", health.GetPercentage());
        }
    }
}
