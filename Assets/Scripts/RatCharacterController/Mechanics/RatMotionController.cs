using UnityEngine;

using static RimuruDev.Helpers.Tag;

namespace RimuruDev.Mechanics.Character
{
    public sealed class RatMotionController : MonoBehaviour
    {
        public RatCharacterData characterData;
        private Vector2 movementDirection;

        private void Awake()
        {
            if (characterData == null)
            {
                characterData = GetComponent<RatCharacterData>();

                if (characterData == null)
                    characterData = GameObject.FindObjectOfType<RatCharacterData>();
            }
        }

        private void Update() => movementDirection = GetPlayerInput();

        public Vector2 GetPlayerInput() => new Vector2(Input.GetAxisRaw(Horizontal), Input.GetAxisRaw(Vertical));

        public void RatMovement()
            => characterData.ratRigidbody2D.velocity =
                new Vector2(
                    movementDirection.normalized.x * characterData.speed,
                    movementDirection.normalized.y * characterData.speed);


        public void RatMovementOrStopped()
        {
            if (!characterData.isStopped)
                RatMovement();
            else
                characterData.ratRigidbody2D.velocity = Vector2.zero;
        }
    }
}