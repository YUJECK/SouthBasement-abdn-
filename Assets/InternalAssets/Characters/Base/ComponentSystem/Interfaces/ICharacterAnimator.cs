using SouthBasement.Enums;

namespace SouthBasement.Characters.Components
{
    public interface ICharacterAnimator
    {
        public CharacterAnimatorConfig CurrentAnimator { get; }

        public void ReplaceAnimator(CharacterAnimatorConfig newAnimator);
    }
}