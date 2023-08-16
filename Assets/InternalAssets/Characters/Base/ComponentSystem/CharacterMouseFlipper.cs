using SouthBasement.Characters.Rat;
using SouthBasement.Enums;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters.Components
{
    public sealed class CharacterMouseFlipper : CharacterComponent<RatCharacter>, IFlipper
    {
        public FacingDirections FacingDirection { get; private set; }
        public bool Blocked { get; set; } = false;

        private CursorService _cursorService;
        
        [Inject]
        private void Construct(CursorService cursorService) 
            => _cursorService = cursorService;

        public CharacterMouseFlipper(RatCharacter ratCharacter, FacingDirections startDirection)
        {
            FacingDirection = startDirection;
            Owner = ratCharacter;
        }

        public override void OnUpdate()
        {
            if(!Blocked)
                FaceToMouse();
        }

        public void Flip(FacingDirections facingDirections)
        {
            FacingDirection = facingDirections;
            
            if (facingDirections == FacingDirections.Right) 
                Owner.Animator.transform.localScale = new Vector3(-1, 1, 1);
            else if (facingDirections == FacingDirections.Left)
                Owner.Animator.transform.localScale = new Vector3(1, 1, 1);
        }

        private void FaceToMouse()
        {
            if (CanFaceRight())
            {
                Flip(FacingDirections.Right); 
                return;
            }
            if (CanFaceLeft())
            {
                Flip(FacingDirections.Left);
                return;                
            }
        }
        
        private bool CanFaceRight()
            => Owner.GameObject.transform.position.x + 0.5f < _cursorService.CursorPosition.x && FacingDirection == FacingDirections.Left;

        private bool CanFaceLeft()
            => Owner.GameObject.transform.position.x - 0.5f > _cursorService.CursorPosition.x && FacingDirection == FacingDirections.Right;
    }
}