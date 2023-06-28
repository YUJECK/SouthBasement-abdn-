using UnityEngine;

namespace SouthBasement.AI
{
    public class EnemyAnimator
    {
        protected readonly Animator Animator;

        public EnemyAnimator(Animator animator)
        {
            Animator = animator;
        }

        public virtual void PlayIdle()
        {
            Animator.Play("Idle");
        }

        public virtual void PlayWalk()
        {
            Animator.Play("Walk");
        }

        public virtual void PlayAttack()
        {
            Animator.Play("Attack");
        }

        public virtual void PlayAFK()
        {
            Animator.Play("AFK");
        }
    }
}