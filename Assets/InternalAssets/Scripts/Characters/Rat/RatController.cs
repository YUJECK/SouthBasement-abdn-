using UnityEngine;
using Zenject;

namespace TheRat.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class RatController : Character
    {
        private Rigidbody2D _rigidbody;
        private InputMap _inputs;

        private Animator _animator;

        [Inject]
        private void Construct(InputMap inputs, CharacterStats characterStats)
        {
            this._inputs = inputs;
            this.Stats = characterStats;
        }

        private void Awake()
        { 
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            
            Movable = new RatMovable(_inputs, _rigidbody, Stats);

            Movable.OnMoved += (Vector2 vector2) => _animator.Play("RatWalk");
            Movable.OnMoveReleased += () => _animator.Play("RatIdle");
        }
        private void FixedUpdate()
        {
            Movable.Move();
        }
    }
}