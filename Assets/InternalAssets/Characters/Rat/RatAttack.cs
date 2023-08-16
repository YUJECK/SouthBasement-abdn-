using System;
using Cysharp.Threading.Tasks;
using SouthBasement.Characters.Components;
using SouthBasement.InventorySystem;

namespace SouthBasement.Characters.Rat
{
    public sealed class RatAttack : CharacterCharacterAttacker<RatCharacter>
    {
        public override WeaponItem Weapon => Owner.WeaponsUsage.CurrentWeapon;
        
        public RatAttack(RatCharacter ratCharacter)
            => Owner = ratCharacter;

        public override void OnStart()
            => Owner.Inputs.OnAttack += StartAttack;

        public override void Dispose()
            => Owner.Inputs.OnAttack -= StartAttack;

        private void StartAttack()
        {
            Attack();
        }

        public override IDamagable[] Attack()
        {
            if (Blocked || !Owner.StaminaController.TryDo(Owner.Stats.AttackStats.CurrentStats.StaminaRequire)) 
                return null;

            IDamagable[] hitted;
            
            if (Weapon != null && Weapon is IAttackOverridable attackOverridable)
            {
                hitted = attackOverridable.Attack();
            }
            else
            {
                hitted = DefaultAttack();
                if(Weapon!=null) Weapon.OnAttack(hitted);
            }

            InvokeAttack(hitted);
            return hitted;
        }

        public override IDamagable[] DefaultAttack()
        {
            Owner.Components.Get<ICharacterMovable>().CanMove = false;

            var hitted = Owner.Attacker
                .Attack(Owner.Stats.AttackStats.CurrentStats.Damage,
                    Owner.Stats.AttackStats.CurrentStats.AttackRate,
                    Owner.Stats.AttackStats.CurrentStats.AttackRange, Weapon);
            
            Owner.AttackRangeAnimator.Play();
            Owner.AudioPlayer.PlayAttack();
            
            Culldown(Owner.Stats.AttackStats.CurrentStats.AttackRate);
            
            return hitted;
        }

        public async void Culldown(float culldown)
        {
            Blocked = true;
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            Owner.Components.Get<ICharacterMovable>().CanMove = true;
            await UniTask.Delay(TimeSpan.FromSeconds(culldown - 0.3f));
            Blocked = false;
        }
    }
}