using System;
using UnityEngine;

namespace SouthBasement.Characters.Components
{
    [Serializable]
    public sealed class CharacterAnimatorConfig
    {
        [field: SerializeField] public Animator UpAnimator { get; private set; }
        [field: SerializeField] public Animator SideAnimator { get; private set; }
        [field: SerializeField] public Animator BottomAnimator { get; private set; }
    }
}