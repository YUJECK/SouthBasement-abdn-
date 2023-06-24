using System;
using SouthBasement.InputServices;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters.Rat.Hole
{
    public sealed class RatInHole : Character
    {
        private PlayerAnimator _animator;
        private Rigidbody2D _rigidbody;
        private IInputService _inputs;
        private WeaponsUsage _weaponsUsage;

        [Inject]
        private void Construct(IInputService inputs, CharacterStats characterStats, WeaponsUsage weaponsUsage)
        {
            _inputs = inputs;
            _weaponsUsage = weaponsUsage;
            Stats = characterStats;
        }
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnDestroy()
        {
        }
    }
}