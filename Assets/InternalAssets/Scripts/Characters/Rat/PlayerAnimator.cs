using UnityEngine;

namespace TheRat.Characters
{
    public sealed class PlayerAnimator
    {
        private readonly Animator _animator;

        private readonly string _walkAnimation = "Walk";
        private readonly string _attackAnimation = "Attack";
        private readonly string _dashTrigger = "Dash";
        public PlayerAnimator(Animator animator) 
            => _animator = animator;

        public void PlayWalk()
            => _animator.SetBool(_walkAnimation, true);

        public void PlayIdle()
            => _animator.SetBool(_walkAnimation, false);

        public void PlayAttack()
            => _animator.SetTrigger(_attackAnimation);
        
        public void PlayDash()
            => _animator.SetTrigger(_dashTrigger);
    }
}