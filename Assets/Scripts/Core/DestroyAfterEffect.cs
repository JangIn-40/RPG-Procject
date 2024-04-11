using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        ParticleSystem hitEffect;

        void Start() 
        {
            hitEffect = GetComponent<ParticleSystem>();
        }


        void Update()
        {
            if(!hitEffect.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
