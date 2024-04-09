using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject 
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject weaponPrefab = null;

        public void Spwan(Transform handtransform, Animator animator)
        {
            Instantiate(weaponPrefab, handtransform);
            animator.runtimeAnimatorController = animatorOverride;
        }
    }
}