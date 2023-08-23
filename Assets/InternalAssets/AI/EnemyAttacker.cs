using System;
using System.Collections;
using SouthBasement.Items;
using SouthBasement.Scripts.Characters;
using UnityEngine;

namespace SouthBasement.AI
{
    public class EnemyAttacker : MonoBehaviour
    {
        [SerializeField] private AudioSource slashSound;
        [field: SerializeField] public float AttackRadius { get; private set; } = 3;
        [field: SerializeField] public int Damage { get; private set; } = 5;
        [field: SerializeField] public float AttackDelay { get; private set; } = 0.5f;
        [field: SerializeField] public float AttackDuration { get; private set; } = 1f;

        [SerializeField] private Transform attackPoint;
        [SerializeField] private SingleAnimationService warningPoint;
        [SerializeField] private AttackRangeAnimator attackRangeAnimator;

        public event Action OnAttackStarted;
        public event Action OnAttackReleased;

        public void StartAttack(Action callback = null) => StartCoroutine(Attack(callback));
        
        private IEnumerator Attack(Action callback)
        {
            OnAttackStarted?.Invoke();
            
            if(warningPoint != null) warningPoint.Play();

            yield return new WaitForSeconds(AttackDelay);
            
            if(slashSound != null) slashSound.Play();

            attackRangeAnimator.Play();
            
            var playerLayer = LayerMask.GetMask("PlayerMarker");
            
            OverlapDecorator
                .DoFor<IDamagable>(attackPoint.position, AttackRadius, playerLayer, 
                    result => result.ForEach( 
                           hit => hit.Damage(Damage, new[] {ItemsTags.All})));

            yield return new WaitForSeconds(AttackDuration);
            
            callback?.Invoke();
            OnAttackReleased?.Invoke();
        }

        private void OnDrawGizmosSelected()
            => Gizmos.DrawWireSphere(attackPoint.position, AttackRadius);
    }
}