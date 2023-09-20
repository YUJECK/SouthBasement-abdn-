using SouthBasement.AI;
using UnityEngine;

namespace SouthBasement
{
    public sealed class SpiderAnimator : EnemyAnimator 
    {
        private const string GoDown = "GoDown";
        private const string GoUp = "GoUp";
        private const string StateName = "SpiderStay";

        public SpiderAnimator(Animator animator) : base(animator) { }

        public void PlayGoUp() => Animator.Play(GoUp);

        public void PlayGoDown() => Animator.Play(GoDown);
        public void PlayAfraid() => Animator.Play(StateName);
    }
}