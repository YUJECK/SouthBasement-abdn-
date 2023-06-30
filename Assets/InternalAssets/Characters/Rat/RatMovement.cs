using System;
using SouthBasement.Characters.Components;
using UnityEngine;

namespace SouthBasement.Characters.Rat
{
    public class RatMovement : CharacterMovement<RatCharacter>
    {
        public RatMovement(RatCharacter ratCharacter)
            => Owner = ratCharacter;

        public override void OnStart()
        { 
            Owner.Inputs.OnMoved += Move;
        }

        public override void Dispose()
        {
            Owner.Inputs.OnMoved -= Move;
        }

        public override void Move(Vector2 movement)
        {
            CurrentMovement = movement * Owner.Stats.MoveStats.MoveSpeed;
            
            if (CanMove)
            {
                Owner.Rigidbody.velocity = CurrentMovement;
            }
            else
            {
                Owner.Rigidbody.velocity = Vector2.zero;
            }

            if (movement != Vector2.zero)
            {
                InvokeOnMoved(movement);
                Owner.AudioPlayer.PlayWalk();
            }
            else
            {
                Owner.AudioPlayer.StopWalk();
                InvokeOnMovedReleased();
            }
        }
    }
}