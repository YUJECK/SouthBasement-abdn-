using SouthBasement.Characters.Components;
using UnityEngine;

namespace SouthBasement.Characters.Hole.Rat
{
    public sealed class RatInHoleMovement : CharacterMovement<RatDummy>
    {
        public RatInHoleMovement(RatDummy ratDummy) 
            => Owner = ratDummy;

        public override void OnStart() => Owner.Inputs.OnMoved += Move;
        public override void Dispose() => Owner.Inputs.OnMoved -= Move;

        public override void Move(Vector2 movement)
        {
            CurrentMovement = movement * 5f;
            
            Owner.Rigidbody.velocity = CanMove ? CurrentMovement : Vector2.zero;

            if (Owner.Rigidbody.velocity != Vector2.zero)
            {
                InvokeOnMoved(movement);
                
                if(!Owner.WalkSource.isPlaying)
                    Owner.WalkSource.Play();
                    
                Owner.Animator.Play("RatWalk");
            }
            else
            {
                InvokeOnMovedReleased();

                if(Owner.WalkSource.isPlaying)
                    Owner.WalkSource.Stop();
                
                Owner.Animator.Play("RatIdle");
            }
        }
    }
}