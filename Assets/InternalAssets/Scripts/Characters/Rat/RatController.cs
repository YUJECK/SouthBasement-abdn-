using System;
using TheRat.Helpers.Rotator;
using UnityEngine;
using TheRat.InputServices;
using TheRat.Player;
using Zenject;

namespace TheRat.Characters.Rat
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class RatController : Character
    {
        [SerializeField] private ObjectRotator attackPoint;
        
        private Rigidbody2D _rigidbody;
        private IInputService _inputs;

        private PlayerAnimator _animator;

        [Inject]
        private void Construct(IInputService inputs, CharacterStats characterStats)
        {
            this._inputs = inputs;
            this.Stats = characterStats;
        }

        private void Awake()
        { 
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = new(GetComponentInChildren<Animator>());
            
            Movable = new RatMovable(_inputs, _rigidbody, Stats);
            Attackable = new RatAttack(_inputs, attackPoint, Stats, _animator);
            Dashable = new RatDashable(_inputs, Movable, transform, _animator, this);

            Movable.OnMoved += (Vector2 vector2) => _animator.PlayWalk();
            Movable.OnMoveReleased += () => _animator.PlayIdle();
        }
    }
}