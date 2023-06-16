using System;
using TheRat.InputServices;
using TheRat.InventorySystem;
using UnityEngine;
using Zenject;

namespace TheRat.Characters.Rat.Hole
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

            Movable = new RatMovable(_inputs, _rigidbody, Stats);
            
            Movable.OnMoved += (Vector2 _) => _animator.PlayWalk();
            Movable.OnMoveReleased += () => _animator.PlayIdle();
        }

        private void OnDestroy()
        {
            Movable.Dispose();
        }
    }
}