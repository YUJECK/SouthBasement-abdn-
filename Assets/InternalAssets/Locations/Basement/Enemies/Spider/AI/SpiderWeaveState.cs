using System.Collections;
using NTC.ContextStateMachine;
using UnityEngine;

namespace SouthBasement
{
    public sealed class SpiderWeaveState : State<SpiderAI>
    {
        private Coroutine _attackCoroutine;
        private float _weaveRate = 0.5f;

        public SpiderWeaveState(SpiderAI stateInitializer) : base(stateInitializer) { }

        public override void OnEnter() => _attackCoroutine = Initializer.StartCoroutine(Attack());
        public override void OnExit()
        {
            Initializer.StopCoroutine(_attackCoroutine);
            Initializer.Components.AudioPlayer.StopWeave();
            Initializer.AttackStrike = 0;
        }

        private IEnumerator Attack()
        {
            Initializer.Components.Animator.PlayAttack();

            while (Initializer.Enabled)
            {
                yield return new WaitForSeconds(_weaveRate);
                
                Initializer.CurrentlyAttacking = true;
                
                Initializer.Components.AudioPlayer.PlayWeave();
                
                yield return Initializer.Components.SpiderWeaver.Weave();
                
                Initializer.Components.AudioPlayer.StopWeave();

                Initializer.AttackStrike++;
                Initializer.CurrentlyAttacking = false;
            }
        }
    }
}