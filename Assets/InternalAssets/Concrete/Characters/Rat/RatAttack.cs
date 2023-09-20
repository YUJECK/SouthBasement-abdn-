using System;
using Cysharp.Threading.Tasks;
using SouthBasement.Characters.Components;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.InventorySystem;
using UnityEngine;

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
            => Attack();

        public override AttackResult Attack()
        {
            if (Blocked) 
                return null;

            AttackResult result;
            
            if (Weapon != null && Weapon is IAttackOverridable attackOverridable)
            {
                result = attackOverridable.Attack();
                
                if(attackOverridable.UseCulldown())
                    Culldown(Owner.Stats.CombatStats.CurrentStats.Multiplied.AttackRate);
            }   
            else
            {
                result = DefaultAttack();
                if(Weapon != null) Weapon.OnAttack(result);
            }

            InvokeAttack(result.DamagedHits.ToArray());
            return result;
        }

        public override AttackResult DefaultAttack()
        {
            AttackResult result = new(Array.Empty<Collider2D>());
            
            if (!Owner.StaminaController.TryDo(Owner.Stats.CombatStats.CurrentStats.Multiplied.StaminaRequire))
                return result;
            
            Owner.Components.Get<ICharacterMovable>().CanMove = false;
            
            result = Owner.BaseRatAttacker.Attack(Owner.Stats.CombatStats.CurrentStats);
            
            Owner.AttackRangeAnimator.Play();
            Owner.AudioPlayer.PlayAttack();
            
            Culldown(Owner.Stats.CombatStats.CurrentStats.Multiplied.AttackRate);
            
            return result;
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