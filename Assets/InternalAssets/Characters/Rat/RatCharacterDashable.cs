using System.Collections;
using SouthBasement.Characters.Components;
using SouthBasement.Characters.Stats;
using UnityEngine;

namespace SouthBasement.Characters.Rat
{
    public sealed class RatCharacterDashable : CharacterDashable<RatCharacter>
    {
        private bool _blocked;

        private CharacterMoveStats _moveStats;
        private IMovable _movable;
        
        public RatCharacterDashable(RatCharacter ratCharacter)
        {
            Owner = ratCharacter;
            
            _movable = Owner.ComponentContainer.GetComponent<IMovable>();
            _moveStats = ratCharacter.Stats.MoveStats;
        }

        public override void OnStart() => Owner.Inputs.OnDashed += Dash;
        public override void Dispose() => Owner.Inputs.OnDashed -= Dash;


        public override void Dash()
        {
            if (Blocked || _blocked || !Owner.StaminaController.TryDo(_moveStats.DashStaminaRequire))
                return;

            Owner.StartCoroutine(DashCoroutine());
        }

        private IEnumerator DashCoroutine()
        {
            StartDash();
            {
                var dashMove = GetPositionInVector2() + Owner.ComponentContainer.GetComponent<IMovable>().CurrentMovement;
                
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
            _movable.CanMove = false;
            _blocked = true;
            Owner.gameObject.layer = 11;
            
            InvokeOnDash();
        }

        private void ReleaseDash()
        {
            Owner.gameObject.layer = 7;
            _movable.CanMove = true;
        }

        private Vector2 GetPositionInVector2() => new(Owner.transform.position.x, Owner.transform.position.y);
    }
}   