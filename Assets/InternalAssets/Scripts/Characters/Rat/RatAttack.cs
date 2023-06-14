using System;
using TheRat.InputServices;
using TheRat.InventorySystem;

namespace TheRat.Characters.Rat
{
    public sealed class RatAttack : IAttackable
    {
        private readonly CharacterStats _characterStats;
        private readonly IInputService _inputService;
        private readonly DefaultAttacker _attacker;
        private readonly PlayerAnimator _playerAnimator;
        
        private WeaponItem _weapon;

        public event Action<float> OnAttacked;

        public WeaponItem Weapon
        {
            get => _weapon;
            set
            {
                if(_weapon != null)
                    _weapon = value;
            } 
        }

        public RatAttack(IInputService inputService, CharacterStats characterStats, DefaultAttacker attacker, PlayerAnimator playerAnimator)
        {
            _characterStats = characterStats;
            _inputService = inputService;
            _attacker = attacker;
            _playerAnimator = playerAnimator;
                
            _inputService.OnAttack += Attack;
        }

        ~RatAttack()
        {
            _inputService.OnAttack -= Attack;
        }
        
        public void Attack()
        {
            _playerAnimator.PlayAttack();
            _attacker.Attack(_characterStats.Damage.Value, _weapon.StaminaRequire, _characterStats.AttackRate.Value, _characterStats.AttackRange.Value);
        } 
    }
}