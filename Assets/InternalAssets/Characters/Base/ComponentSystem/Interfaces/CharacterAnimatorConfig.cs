using System;
using UnityEditor.Animations;
using UnityEngine;

namespace SouthBasement.Characters.Components
{
    [Serializable]
    [CreateAssetMenu]
    public sealed class CharacterAnimatorConfig : ScriptableObject
    {
        [field: SerializeField] public AnimatorController UpAnimator { get; private set; }
        [field: SerializeField] public AnimatorController SideAnimator { get; private set; }
        [field: SerializeField] public AnimatorController BottomAnimator { get; private set; }
    }
}