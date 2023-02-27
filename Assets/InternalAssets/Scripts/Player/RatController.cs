using TheRat.Core;
using UnityEngine;

namespace Assets.InternalAssets.Scripts.Player
{
    public sealed class RatController : MonoBehaviour, ICharacterController
    {
        public IMovable movable { get; private set; }

        [SerializeField] private new Rigidbody2D rigidbody;

        private void Awake()
        {
            movable = new RatMovable(rigidbody);
        }

        private void FixedUpdate()
        {
            movable.Move();
        }
    }
}