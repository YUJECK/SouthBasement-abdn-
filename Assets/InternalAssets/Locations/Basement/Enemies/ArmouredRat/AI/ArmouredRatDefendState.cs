using System.Collections;
using NTC.ContextStateMachine;
using UnityEngine;

namespace SouthBasement.Basement.Enemies.ArmouredRat.AI
{
    public sealed class ArmouredRatDefendState : State<ArmouredRatAI>
    {
        private Coroutine _defendCoroutine;
        public ArmouredRatDefendState(ArmouredRatAI stateInitializer) : base(stateInitializer) { }

        protected override void OnEnter()
        {
            _defendCoroutine = Initializer.StartCoroutine(Defend());
        }

        protected override void OnExit() => Initializer.StopCoroutine(_defendCoroutine);

        private IEnumerator Defend()
        {
            while (Initializer.CanEnterDefendState())
            {
                yield return new WaitForSeconds(0.5f);
                Initializer.ArmouredRatDefender.Defend();

                yield return new WaitForSeconds(2.7f);

                Initializer.ArmouredRatDefender.UnDefend();
                Initializer.EnemyAnimator.PlayIdle();
                Initializer.NeedToAttack = true; 
                Initializer.NeedToDefend = false;
                
                yield return new WaitForSeconds(2f);
            }
        }
    }
}