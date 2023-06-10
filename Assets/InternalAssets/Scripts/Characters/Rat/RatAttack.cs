using System;
using Cysharp.Threading.Tasks;
using TheRat.Helpers.Rotator;
using TheRat.InputServices;
using TheRat.Player;
using UnityEngine;

namespace TheRat.Characters.Rat
{
    public sealed class RatAttack : IAttackable
    {
        private readonly ObjectRotator _attackPoint;
        private readonly CharacterStats _characterStats;
        private readonly PlayerAnimator _playerAnimator;
        private readonly IInputService _inputService;

        private bool _blocked;
        
        public RatAttack(IInputService inputService, ObjectRotator attackPoint, CharacterStats characterStats, PlayerAnimator playerAnimator)
        {
            _attackPoint = attackPoint;
            _characterStats = characterStats;
            _playerAnimator = playerAnimator;
            _inputService = inputService;
            
            _inputService.OnAttack += Attack;
        }

        public event Action<float> OnAttacked;

        public void Attack()
        {
            if(_blocked)
                return;
            
            _playerAnimator.PlayAttack();
            _attackPoint.Stop(_characterStats.AttackRate - 0.05f);
            
            var hits = Physics2D.OverlapCircleAll(_attackPoint.Point.transform.position, _characterStats.AttackRange);

            foreach (var hit in hits)
            {
                if(!hit.isTrigger && hit.TryGetComponent<IDamagable>(out var damagable))
                    damagable.Damage(_characterStats.Damage);
            }
            OnAttacked?.Invoke(_characterStats.AttackRate);
            
            Culldown();
        }

        private async void Culldown()
        {
            _blocked = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_characterStats.AttackRate));
            _blocked = false;
        }
    }
}