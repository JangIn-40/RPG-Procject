using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour 
    {
        [SerializeField] Health healthComponenet = null;
        [SerializeField] RectTransform foreground = null;

        Canvas canvas;

        void Awake()
        {
            canvas = GetComponentInChildren<Canvas>();
        }

        void Update()
        {
            if(Mathf.Approximately(healthComponenet.GetPercentage() / 100, 0)
            || Mathf.Approximately(healthComponenet.GetPercentage() / 100, 1))
            {
                canvas.enabled = false;
                return;
            }

            canvas.enabled = true;
            

            foreground.localScale = new Vector3(healthComponenet.GetPercentage() / 100, 1);

        }
    }
}