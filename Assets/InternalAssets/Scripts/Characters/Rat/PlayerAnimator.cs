using UnityEngine;

namespace TheRat.Player
{
    public sealed class PlayerAnimator
    {
        private readonly Animator _animator;

        private readonly int _walkAnimation = Animator.StringToHash("RatWalk");
        private readonly int _idleAnimation = Animator.StringToHash("RatIdle");
        private readonly int _attackAnimation = Animator.StringToHash("RatAttack");
        
        public PlayerAnimator(Animator animator) 
            => _animator = animator;

        public void PlayWalk()
            => _animator.Play(_walkAnimation);

        public void PlayIdle()
            => _animator.Play(_idleAnimation);

        public void PlayAttack()
            => _animator.Play(_attackAnimation);
    }
}