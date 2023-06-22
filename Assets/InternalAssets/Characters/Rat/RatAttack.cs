using System;
using Cysharp.Threading.Tasks;
using NaughtyAttributes.Test;
using SouthBasement.InputServices;
using SouthBasement.InventorySystem;
using SouthBasement.Characters.Stats;

namespace SouthBasement.Characters.Rat
{
    public sealed class RatAttack : IAttackable
    {
        private readonly CharacterAttackStats _attackStats;
        private readonly IInputService _inputService;
        private readonly DefaultAttacker _attacker;
        private readonly WeaponsUsage _weaponsUsage;
        private readonly StaminaController _staminaController;


        public WeaponItem Weapon => _weaponsUsage.CurrentWeapon;
        public bool Blocked { get; set; }

        public event Action<float> OnAttacked;

        public RatAttack
            (IInputService inputService, CharacterAttackStats attackStats, DefaultAttacker attacker, WeaponsUsage weaponsUsage, StaminaController staminaController)
        {
            _attackStats = attackStats;
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
            if (Blocked || !_staminaController.TryDo(_attackStats.CurrentStats.StaminaRequire)) 
                return;
            
            var hitted = _attacker.Attack(_attackStats.CurrentStats.Damage,_attackStats.CurrentStats.AttackRate, _attackStats.CurrentStats.AttackRange);
            
            if(Weapon != null)
                Weapon.OnAttack(hitted);
                
            OnAttacked?.Invoke(_attackStats.CurrentStats.AttackRate);
            Culldown(_attackStats.CurrentStats.AttackRate);
        } 
        
        private async void Culldown(float culldown)
        {
            Blocked = true;
            await UniTask.Delay(TimeSpan.FromSeconds(culldown));
            Blocked = false;
        }
    }
}