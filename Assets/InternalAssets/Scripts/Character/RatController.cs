using UnityEngine;
using Zenject;

namespace TheRat.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class RatController : Character
    {
        [SerializeField] private new Rigidbody2D rigidbody;
        private InputMap inputs;

        [Inject]
        private void Construct(InputMap inputs, CharacterStats characterStats)
        {
            this.inputs = inputs;
            this.Stats = characterStats;
        }

        private void Awake() => Movable = new RatMovable(inputs, rigidbody, Stats);
        private void FixedUpdate()
        {
            Movable.Move();
        }
    }
}