using System;
using UnityEngine;
using TheRat.InputServices;
using TheRat.InventorySystem;
using Zenject;

namespace TheRat.Characters.Rat
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class RatController : Character
    {
        [SerializeField] private DefaultAttacker attacker;
        
        private Rigidbody2D _rigidbody;
        private IInputService _inputs;

        private PlayerAnimator _animator;
        private WeaponsUsage _weaponsUsage;

        [Inject]
        private void Construct(IInputService inputs, CharacterStats characterStats, WeaponsUsage weaponsUsage)
        {
            this._inputs = inputs;
            this.Stats = characterStats;
            _weaponsUsage = weaponsUsage;
        }

        private void Awake()
        { 
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = new(GetComponentInChildren<Animator>());
            
            Movable = new RatMovable(_inputs, _rigidbody, Stats);
            Attackable = new RatAttack(_inputs, Stats, attacker, _animator, _weaponsUsage);
            Dashable = new RatDashable(_inputs, Movable, transform, _animator, this);

            Movable.OnMoved += (Vector2 vector2) => _animator.PlayWalk();
            Movable.OnMoveReleased += () => _animator.PlayIdle();
        }

        private void OnDestroy()
        {
            Movable.Dispose();
            Dashable.Dispose();
            Attackable.Dispose();
        }
    }
}