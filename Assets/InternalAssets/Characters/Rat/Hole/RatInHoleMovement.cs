using SouthBasement.Characters.Components;
using UnityEngine;

namespace SouthBasement.Characters.Rat.Hole
{
    public sealed class RatInHoleMovement : CharacterMovement<RatInHole>
    {
        public RatInHoleMovement(RatInHole ratInHole) 
            => Owner = ratInHole;

        public override void OnStart() => Owner.Inputs.OnMoved += Move;
        public override void Dispose() => Owner.Inputs.OnMoved -= Move;

        public override void Move(Vector2 movement)
        {
            CurrentMovement = movement * Owner.Stats.MoveStats.MoveSpeed;
            
            Owner.Rigidbody.velocity = CanMove ? CurrentMovement : Vector2.zero;

            if (movement != Vector2.zero)
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