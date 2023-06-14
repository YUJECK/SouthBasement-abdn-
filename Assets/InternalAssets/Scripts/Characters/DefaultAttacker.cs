using System;
using Cysharp.Threading.Tasks;
using TheRat.Helpers.Rotator;
using UnityEngine;
using Zenject;

namespace TheRat.Characters
{
    public sealed class DefaultAttacker : MonoBehaviour
    {
        [SerializeField] private ObjectRotator _attackPoint;
        
        private StaminaController _staminaController;

        private bool _blocked;

        [Inject]
        private void Construct(StaminaController staminaController)
        {
            _staminaController = staminaController;
        }

        public void Attack(int damage, int staminaRequire, float culldown, float range)
        {
            if(_blocked && !_staminaController.TryDo(staminaRequire))
                return;
            
            _attackPoint.Stop(culldown - 0.05f);

            var mask = LayerMask.GetMask("Enemy"); 
            
            var hits = Physics2D.OverlapCircleAll(_attackPoint.Point.transform.position, range, mask);

            foreach (var hit in hits)
            {
                if(!hit.isTrigger && hit.TryGetComponent<IDamagable>(out var damagable))
                    damagable.Damage(damage);
            }
            
            Culldown(culldown);
        }

        private async void Culldown(float culldown)
        {
            _blocked = true;
            await UniTask.Delay(TimeSpan.FromSeconds(culldown));
            _blocked = false;
        }
    }
}