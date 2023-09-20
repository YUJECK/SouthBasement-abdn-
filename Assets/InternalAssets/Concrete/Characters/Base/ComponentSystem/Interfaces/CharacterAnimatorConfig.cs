using System;
using UnityEngine;

namespace SouthBasement.Characters.Components
{
    [Serializable]
    public sealed class CharacterAnimatorConfig
    {
        [field: SerializeField] public RuntimeAnimatorController AnimatorController { get; private set; }

        public CharacterAnimatorConfig(RuntimeAnimatorController animatorController)
            => AnimatorController = animatorController;
    }
}