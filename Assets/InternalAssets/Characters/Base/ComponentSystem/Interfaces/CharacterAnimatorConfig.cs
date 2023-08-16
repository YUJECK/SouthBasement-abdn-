using System;
using UnityEditor.Animations;
using UnityEngine;

namespace SouthBasement.Characters.Components
{
    [Serializable]
    public sealed class CharacterAnimatorConfig
    {
        [field: SerializeField] public AnimatorController AnimatorController { get; private set; }

        public CharacterAnimatorConfig(AnimatorController animatorController)
            => AnimatorController = animatorController;
    }
}