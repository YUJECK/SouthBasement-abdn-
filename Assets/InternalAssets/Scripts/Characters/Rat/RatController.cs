using UnityEngine;
using Zenject;

namespace TheRat.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class RatController : Character
    {
        private Rigidbody2D _rigidbody;
        private InputMap _inputs;

        private PlayerAnimator _animator;

        [Inject]
        private void Construct(InputMap inputs, CharacterStats characterStats)
        {
            this._inputs = inputs;
            this.Stats = characterStats;
        }

        private void Awake()
        { 
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = new(GetComponentInChildren<Animator>());
            
            Movable = new RatMovable(_inputs, _rigidbody, Stats);

            Movable.OnMoved += (Vector2 vector2) => _animator.PlayWalk();
            Movable.OnMoveReleased += () => _animator.PlayIdle();
        }
        private void FixedUpdate()
        {
            Movable.Move();
        }
    }
}