using System;
using Cysharp.Threading.Tasks;
using SouthBasement.Characters.Components;
using SouthBasement.InventorySystem;

namespace SouthBasement.Characters.Rat
{
    public sealed class RatAttack : CharacterAttackable<RatCharacter>, IAttackable
    {
        public WeaponItem Weapon => Owner.WeaponsUsage.CurrentWeapon;

        public event Action<float> OnAttacked;
        
        public RatAttack(RatCharacter ratCharacter)
            => Owner = ratCharacter;

        public override void OnStart()
            => Owner.Inputs.OnAttack += Attack;

        public override void Dispose()
            => Owner.Inputs.OnAttack -= Attack;

        public override void Attack()
        {
            if (Blocked || !Owner.StaminaController.TryDo(Owner.Stats.AttackStats.CurrentStats.StaminaRequire)) 
                return;
            
            var hitted = Owner.Attacker
                .Attack(Owner.Stats.AttackStats.CurrentStats.Damage,
                    Owner.Stats.AttackStats.CurrentStats.AttackRate, 
                    Owner.Stats.AttackStats.CurrentStats.AttackRange, Weapon);
            
            if(Weapon != null)
                Weapon.OnAttack(hitted);
                
            OnAttacked?.Invoke(Owner.Stats.AttackStats.CurrentStats.AttackRate);
            Culldown(Owner.Stats.AttackStats.CurrentStats.AttackRate);
        }

        private async void Culldown(float culldown)
        {
            Blocked = true;
            await UniTask.Delay(TimeSpan.FromSeconds(culldown));
            Blocked = false;
        }
    }
}