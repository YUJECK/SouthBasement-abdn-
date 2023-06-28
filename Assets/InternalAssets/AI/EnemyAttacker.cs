using System;
using System.Collections;
using SouthBasement.InternalAssets.Scripts.Characters;
using UnityEditor;
using UnityEngine;

namespace SouthBasement.AI
{
    public class EnemyAttacker : MonoBehaviour
    {
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
            var hits = Physics2D.OverlapCircleAll(attackPoint.transform.position, 0.6f, playerLayer);

            foreach (var hit in hits)
            {
                if (!hit.isTrigger && hit.TryGetComponent<IDamagable>(out var damagable))
                    damagable.Damage(damage, new[] {""});
            }

            yield return new WaitForSeconds(AttackDuration);
            
            callback?.Invoke();
            OnAttackReleased?.Invoke();
        }
    }
}