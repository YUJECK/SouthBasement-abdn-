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

        private readonly WeaponsUsage _weaponsUsage;

        public WeaponItem Weapon => _weaponsUsage.CurrentWeapon;

        public event Action<float> OnAttacked;

        public RatAttack(IInputService inputService, CharacterStats characterStats, DefaultAttacker attacker, PlayerAnimator playerAnimator, WeaponsUsage weaponsUsage)
        {
            _characterStats = characterStats;
            _inputService = inputService;
            _attacker = attacker;
            _playerAnimator = playerAnimator;
            _weaponsUsage = weaponsUsage;
                
            _inputService.OnAttack += Attack;
        }

        ~RatAttack()
        {
            _inputService.OnAttack -= Attack;
        }
        
        public void Attack()
        {
            _playerAnimator.PlayAttack();
            
            if(Weapon != null)
                Weapon.OnAttack();

            _attacker
                .Attack(_characterStats.WeaponStats.Damage,
                    _characterStats.WeaponStats.StaminaRequire, 
                    _characterStats.WeaponStats.AttackRate, 
                    _characterStats.WeaponStats.AttackRange);
        } 
    }
}