using SouthBasement.Enums;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters.Components
{
    public class MouseFlipper<TCharacter> : CharacterComponent<TCharacter>, IFlipper
        where TCharacter : MonoBehaviour
    {
        public FacingDirections FacingDirection { get; private set; }
        public bool Blocked { get; set; } = false;

        private CursorService _cursorService;
        private readonly GameObject _objectToFlip;

        [Inject]
        private void Construct(CursorService cursorService) 
            => _cursorService = cursorService;

        public MouseFlipper(TCharacter owner, GameObject objectToFlip, FacingDirections startDirection)
        {
            FacingDirection = startDirection;
            _objectToFlip = objectToFlip;
            Owner = owner;
        }

        public override void OnUpdate()
        {
            if(!Blocked)
                FaceToMouse();
        }

        public void Flip(FacingDirections facingDirections)
        {
            if (facingDirections == FacingDirections.Right)
            {
                _objectToFlip.transform.localScale = new Vector3(-1, 1, 1);
                FacingDirection = FacingDirections.Right;
            }
            else if (facingDirections == FacingDirections.Left)
            {
                _objectToFlip.transform.localScale = new Vector3(1, 1, 1);
                FacingDirection = FacingDirections.Left;
            }
        }

        private void FaceToMouse()
        {
            if (CanFaceRight()) Flip(FacingDirections.Right);
            else if (CanFaceLeft()) Flip(FacingDirections.Left);
        }
        
        private bool CanFaceRight()
            => Owner.transform.position.x + 0.5f < _cursorService.CursorPosition.x && FacingDirection == FacingDirections.Left;
        private bool CanFaceLeft()
            => _objectToFlip.transform.position.x - 0.5f> _cursorService.CursorPosition.x && FacingDirection == FacingDirections.Right;
    }
}