using System.Collections;
using NTC.ContextStateMachine;
using UnityEngine;

namespace SouthBasement
{
    public sealed class SpiderAttackState : State<SpiderAI>
    {
        private Coroutine _attackCoroutine;
        private float _weaveRate = 0.5f;

        public SpiderAttackState(SpiderAI stateInitializer) : base(stateInitializer) { }

        public override void OnEnter() => _attackCoroutine = Initializer.StartCoroutine(Attack());
        public override void OnExit()
        {
            Initializer.StopCoroutine(_attackCoroutine);
            Initializer.AttackStrike = 0;
        }

        private IEnumerator Attack()
        {
            Initializer.Components.SpiderAnimator.PlayAttack();

            while (Initializer.Enabled)
            {
                yield return new WaitForSeconds(_weaveRate);
                
                Initializer.CurrentlyAttacking = true;
                yield return Initializer.Components.SpiderWeaver.Weave();
                
                Initializer.AttackStrike++;
                Initializer.CurrentlyAttacking = false;
            }
        }
    }
}