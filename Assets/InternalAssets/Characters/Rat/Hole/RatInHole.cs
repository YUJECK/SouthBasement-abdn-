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
            _animator = new(GetComponentInChildren<Animator>(), null, _weaponsUsage);

            Movable = new RatMovable(_inputs, _rigidbody, Stats.MoveStats);
            
            Movable.OnMoved += (Vector2 _) => _animator.PlayWalk();
            Movable.OnMoveReleased += () => _animator.PlayIdle();
        }

        private void OnDestroy()
        {
            Movable.Dispose();
        }
    }
}