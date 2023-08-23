using System;
using Cysharp.Threading.Tasks;
using SouthBasement.Characters.Components;
using SouthBasement.InventorySystem;
using SouthBasement.Items;

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
            if (Blocked) 
                return null;

            IDamagable[] hitted;
            
            if (Weapon != null && Weapon is IAttackOverridable attackOverridable)
            {
                hitted = attackOverridable.Attack();
                
                if(attackOverridable.UseCulldown())
                    Culldown(Owner.Stats.AttackStats.CurrentStats.AttackRate);
            }
            else
            {
                hitted = DefaultAttack();
                if(Weapon != null) Weapon.OnAttack(hitted);
            }

            InvokeAttack(hitted);
            return hitted;
        }

        public override IDamagable[] DefaultAttack()
        {
            var hitted = new IDamagable[12];
            
            if (!Owner.StaminaController.TryDo(Owner.Stats.AttackStats.CurrentStats.StaminaRequire))
                return hitted;
            
            Owner.Components.Get<ICharacterMovable>().CanMove = false;

            ItemsTags[] args = Array.Empty<ItemsTags>();
            
            if (Weapon != null)
                args = Weapon.ItemTags.ToArray();
            
            hitted = Owner.BaseRatAttacker
                .Attack(Owner.Stats.AttackStats.CurrentStats.Damage,
                    Owner.Stats.AttackStats.CurrentStats.AttackRate,
                    Owner.Stats.AttackStats.CurrentStats.AttackRange, args);
            
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