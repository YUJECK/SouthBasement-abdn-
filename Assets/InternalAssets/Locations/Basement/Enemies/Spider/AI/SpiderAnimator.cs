using SouthBasement.AI;
using UnityEngine;

namespace TheRat
{
    public sealed class SpiderAnimator : EnemyAnimator 
    {
        private const string GoDown = "GoDown";
        private const string GoUp = "GoUp";

        public SpiderAnimator(Animator animator) : base(animator) { }

        public void PlayGoUp()
        {
            Animator.Play(GoUp);
        }

        public void PlayGoDown()
        {
            Animator.Play(GoDown);
        }
    }
}