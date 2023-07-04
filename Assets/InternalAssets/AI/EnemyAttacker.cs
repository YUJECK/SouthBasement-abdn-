using System;
using System.Collections;
using SouthBasement.Scripts.Characters;
using UnityEditor;
using UnityEngine;

namespace SouthBasement.AI
{
    public class EnemyAttacker : MonoBehaviour
    {
        [field: SerializeField] public float AttackRadius { get; private set; } = 3;
        [field: SerializeField] public float AttackDelay { get; private set; } = 0.5f;
        [field: SerializeField] public float AttackDuration { get; private set; } = 1f;

        [SerializeField] private Transform attackPoint;
        [SerializeField] private SingleAnimationService warningPoint;
        [SerializeField] private AttackRangeAnimator attackRangeAnimator;

        public event Action OnAttackStarted;
        public event Action OnAttackReleased;

        public void StartAttack(int damage, Action callback = null) => StartCoroutine(Attack(damage, callback));
        
        private IEnumerator Attack(int damage, Action callback)
        {
            OnAttackStarted?.Invoke();
            warningPoint.Play();
            
            yield return new WaitForSeconds(AttackDelay);
            
            attackRangeAnimator.Play();
            
            var playerLayer = LayerMask.GetMask("PlayerMarker");
            
            OverlapDecorator
                .DoFor<IDamagable>(attackPoint.position, AttackRadius, playerLayer, 
                (result) => result.ForEach( 
                       (hit) => hit.Damage(damage, new[] {""})) );

            yield return new WaitForSeconds(AttackDuration);
            
            callback?.Invoke();
            OnAttackReleased?.Invoke();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position, AttackRadius);
        }
    }
}