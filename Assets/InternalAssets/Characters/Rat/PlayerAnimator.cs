using SouthBasement.InternalAssets.Scripts.Characters;
using SouthBasement.InventorySystem;
using UnityEngine;

namespace SouthBasement.Characters
{
    public sealed class PlayerAnimator
    {
        private readonly Animator _animator;
        private readonly AttackRangeAnimator _attackRangeAnimator;
        private readonly WeaponsUsage _weaponUsage;

        private readonly int _walkAnimation = Animator.StringToHash("Walk");
        private readonly int _attackAnimation = Animator.StringToHash("Attack");
        private readonly int _dashTrigger = Animator.StringToHash("Dash");
        private readonly int _hasWeapon = Animator.StringToHash("HasWeapon");

        public PlayerAnimator(Animator animator, AttackRangeAnimator attackRangeAnimator, WeaponsUsage weaponsUsage)
        {
            _animator = animator;
            _attackRangeAnimator = attackRangeAnimator;
            _weaponUsage = weaponsUsage;
        }

        public void PlayWalk()
            => _animator.SetBool(_walkAnimation, true);

        public void PlayIdle()
            => _animator.SetBool(_walkAnimation, false);

        public void PlayAttack()
        {
            _animator.SetTrigger(_attackAnimation);
            _animator.SetBool(_hasWeapon, _weaponUsage.CurrentWeapon != null); 
            _attackRangeAnimator.Play();
        }

        public void PlayDash()
            => _animator.SetTrigger(_dashTrigger);
    }
}