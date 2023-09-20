using SouthBasement.Enums;

namespace SouthBasement.Characters.Components
{
    public interface IFlipper
    {
        FacingDirections FacingDirection { get; }

        bool Blocked { get; set; }

        void Flip(FacingDirections facingDirections);
    }
}