using System;
using Cysharp.Threading.Tasks;
using TheRat.InputServices;
using TheRat.InternalAssets.Scripts.Characters;
using TheRat.InventorySystem;
using UnityEngine;

namespace TheRat.Characters.Rat
{
    public sealed class RatAttack : IAttackable
    {
        private readonly CharacterStats _characterStats;
        private readonly IInputService _inputService;
        private readonly DefaultAttacker _attacker;
        private readonly WeaponsUsage _weaponsUsage;
        private readonly StaminaController _staminaController;

        private bool _blocked;

        public WeaponItem Weapon => _weaponsUsage.CurrentWeapon;

        public event Action<float> OnAttacked;

        public RatAttack
            (IInputService inputService, CharacterStats characterStats, DefaultAttacker attacker, WeaponsUsage weaponsUsage, StaminaController staminaController)
        {
            _characterStats = characterStats;
            _inputService = inputService;
            _attacker = attacker;
            _weaponsUsage = weaponsUsage;
            _staminaController = staminaController;
                
            _inputService.OnAttack += Attack;
        }

        public void Dispose()
        {
            _inputService.OnAttack -= Attack;
        }

        public void Attack()
        {
            if (_blocked || !_staminaController.TryDo(_characterStats.WeaponStats.StaminaRequire)) 
                return;
            
            if(Weapon != null)
                Weapon.OnAttack();
                
            _attacker
                .Attack(_characterStats.WeaponStats.Damage,
                    _characterStats.WeaponStats.AttackRate, 
                    _characterStats.WeaponStats.AttackRange);
                
            OnAttacked?.Invoke(_characterStats.WeaponStats.AttackRate);
            Culldown(_characterStats.WeaponStats.AttackRate);
        } 
        
        private async void Culldown(float culldown)
        {
            _blocked = true;
            await UniTask.Delay(TimeSpan.FromSeconds(culldown));
            _blocked = false;
        }
        
    }
}