using UnityEngine;
using TMPro;
using System;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour 
    {
        Experience experience;
        TextMeshProUGUI healthValue;

        void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            healthValue = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            healthValue.text = String.Format("{0:0}", experience.GetExperience());
        }

    }
}
