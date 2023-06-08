using TheRat.InputServices;
using TheRat.Player;
using UnityEngine;

namespace TheRat.Characters.Rat
{
    public sealed class RatAttack : IAttackable
    {
        private readonly Transform _attackPoint;
        private readonly CharacterStats _characterStats;
        private readonly PlayerAnimator _playerAnimator;
        private readonly IInputService _inputService;

        public RatAttack(IInputService inputService, Transform attackPoint, CharacterStats characterStats, PlayerAnimator playerAnimator)
        {
            _attackPoint = attackPoint;
            _characterStats = characterStats;
            _playerAnimator = playerAnimator;
            _inputService = inputService;
            
            _inputService.OnAttack += Attack;
        }

        public void Attack()
        {
            _playerAnimator.PlayAttack();

            var hits = Physics2D.OverlapCircleAll(_attackPoint.transform.position, _characterStats.AttackRange);

            foreach (var hit in hits)
            {
                if(hit.TryGetComponent<IDamagable>(out var damagable))
                    damagable.Damage(_characterStats.Damage);
            }
        }
    }
}