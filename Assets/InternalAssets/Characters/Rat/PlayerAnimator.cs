using SouthBasement.Characters.Components;
using SouthBasement.Characters.Rat;
using UnityEngine;

namespace SouthBasement.Characters
{
    public sealed class PlayerAnimator : CharacterComponent<RatCharacter>
    {
        private readonly int _walkAnimation = Animator.StringToHash("Walk");
        private readonly int _attackAnimation = Animator.StringToHash("Attack");
        private readonly int _dashTrigger = Animator.StringToHash("Dash");
        private readonly int _hasWeapon = Animator.StringToHash("HasWeapon");
        private readonly int _diedTrigger = Animator.StringToHash("RatDied");

        public PlayerAnimator(RatCharacter ratCharacter)
            => Owner = ratCharacter;

        public void PlayWalk()
            => Owner.Animator.SetBool(_walkAnimation, true);

        public void PlayIdle()
            => Owner.Animator.SetBool(_walkAnimation, false);

        public void PlayAttack()
        {
            Owner.Animator.SetTrigger(_attackAnimation);
            Owner.Animator.SetBool(_hasWeapon, Owner.WeaponsUsage.CurrentWeapon != null); 
            Owner.AttackRangeAnimator.Play();
        }

        public void PlayDash()
            => Owner.Animator.SetTrigger(_dashTrigger);

        public void PlayDead() => Owner.Animator.SetBool(_diedTrigger, true);
    }
}