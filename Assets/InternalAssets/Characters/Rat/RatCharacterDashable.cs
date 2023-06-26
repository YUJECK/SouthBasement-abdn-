using System.Collections;
using SouthBasement.Characters.Components;
using SouthBasement.Characters.Stats;
using SouthBasement.Enums;
using UnityEngine;

namespace SouthBasement.Characters.Rat
{
    public sealed class RatCharacterDashable : CharacterDashable<RatCharacter>
    {
        private bool _blocked;

        private CharacterMoveStats _moveStats;
        
        public RatCharacterDashable(RatCharacter ratCharacter)
        {
            Owner = ratCharacter;
            
            _moveStats = ratCharacter.Stats.MoveStats;
        }

        public override void OnStart() => Owner.Inputs.OnDashed += Dash;
        public override void Dispose() => Owner.Inputs.OnDashed -= Dash;
        
        public override void Dash()
        {
            if (Blocked || _blocked || !Owner.StaminaController.TryDo(_moveStats.DashStaminaRequire))
                return;
            if (Owner.Components.Get<IMovable>().CurrentMovement == Vector2.zero)
                return;

            Owner.StartCoroutine(DashCoroutine());
        }

        private IEnumerator DashCoroutine()
        {
            StartDash();
            {
                var dashMove = GetPositionInVector2() + Owner.Components.Get<IMovable>().CurrentMovement;
                
                var dashStopTime = Time.time + 0.135;
                
                while (Time.time < dashStopTime)
                {
                    Owner.Rigidbody
                        .MovePosition(Vector2.MoveTowards(Owner.transform.position, dashMove, Time.deltaTime * 30));
                    
                    yield return new WaitForFixedUpdate();
                }
            }
            ReleaseDash();

            yield return new WaitForSeconds(0.7f);
            _blocked = false;
        }

        private void StartDash()
        {
            Owner.Components.Get<IMovable>().CanMove = false;
            _blocked = true;
            Owner.gameObject.layer = 11;

            Owner.Components.Get<IFlipper>().Blocked = true;
            Owner.Components.Get<IFlipper>().Flip(GetFacingDirection());
            
            InvokeOnDash();
        }

        private void ReleaseDash()
        {
            Owner.gameObject.layer = 7;
            Owner.Components.Get<IMovable>().CanMove = true;
            Owner.Components.Get<IFlipper>().Blocked = false;
        }

        private Vector2 GetPositionInVector2() => new(Owner.transform.position.x, Owner.transform.position.y);

        private FacingDirections GetFacingDirection()
        {
            if (Owner.Components.Get<IMovable>().CurrentMovement.x > 0)
                return FacingDirections.Right;

            return FacingDirections.Left;
        }
    }
}   