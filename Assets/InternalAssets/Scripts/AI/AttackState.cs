using System.Collections;
using NTC.ContextStateMachine;
using UnityEngine;

namespace TheRat.AI
{
    public sealed class AttackState : State<DefaultRatStateMachine>
    {
        public AttackState(DefaultRatStateMachine stateInitializer) : base(stateInitializer) { }

        public override void OnEnter()
        {
            Initializer.StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            Initializer.WarningPoint.Play();
            
            yield return new WaitForSeconds(0.5f);
            
            Initializer.AttackRangeAnimator.Play(1f);
            
            var playerLayer = LayerMask.GetMask("PlayerMarker");
            var hits = Physics2D.OverlapCircleAll(Initializer.AttackPoint.transform.position, 0.6f, playerLayer);

            foreach (var hit in hits)
            {
                if(!hit.isTrigger && hit.TryGetComponent<IDamagable>(out var damagable))
                    damagable.Damage(5);
            }
        }
    }
}