using SouthBasement.AI;
using UnityEngine;

namespace SouthBasement.Basement.Enemies.ArmouredRat.AI
{
    public sealed class ArmouredRatAnimator : EnemyAnimator
    {
        private const string RatDefends = "ArmouredRatDefended";
        public ArmouredRatAnimator(Animator animator) : base(animator) { }

        public void PlayDefends()
        {
            Animator.Play(RatDefends);
        }
    }
}