using UnityEngine;

namespace TheRat.AI
{
    public sealed class EnemyAnimator
    {
        private readonly Animator _animator;

        public EnemyAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void PlayIdle()
        {
            _animator.Play("Idle");
        }

        public void PlayWalk()
        {
            _animator.Play("Walk");
        }

        public void PlayAttack()
        {
            _animator.Play("Attack");
        }
    }
}