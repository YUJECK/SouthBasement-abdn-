using System.Collections;
using DG.Tweening;
using NTC.ContextStateMachine;
using UnityEngine;

namespace TheRat
{
    public sealed class SpiderAttackState : State<SpiderAI>
    {
        private Coroutine _attackCoroutine;
        
        public SpiderAttackState(SpiderAI stateInitializer) : base(stateInitializer) { }

        public override void OnEnter()
        {
            _attackCoroutine = Initializer.StartCoroutine(Attack());
        }

        public override void OnExit()
        {
            Initializer.StopCoroutine(_attackCoroutine);
        }

        private IEnumerator Attack()
        {
            Initializer.SpiderAnimator.PlayAttack();

            while (Initializer.Enabled)
            {
                yield return new WaitForSeconds(0.5f);
                Initializer.CurrentlyAttacking = true;

                var attackerTransform = Initializer.SpiderAttacker.transform;
                
                var projectile = GameObject.Instantiate(
                    Initializer.SpiderAttacker.BulletPrefab, 
                    attackerTransform.position,
                    attackerTransform.rotation,
                    Initializer.transform);

                projectile.transform.localScale = new(0.3f, 0.3f, 1);
                projectile.transform.DOScale(new Vector3(1f, 1f, 1f), 2f);
                
                yield return new WaitForSeconds(2f);
                
                Initializer.SpiderAttacker.ThrowYarn(projectile);
                Initializer.CurrentlyAttacking = false;
            }
        }
    }
}