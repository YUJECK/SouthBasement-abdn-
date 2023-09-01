using System;
using Cysharp.Threading.Tasks;
using SouthBasement.InventorySystem;
using UnityEngine;

namespace SouthBasement.InternalAssets.Items.Weapons.BloodthirstyFalchion
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Weapon + "BloodthirstyFalchion")]
    public sealed class BloodthirstyFalchion : WeaponItem
    {
        public override Type GetItemType()
            => typeof(WeaponItem);

        public override void OnAttack(AttackResult results)
        {
            foreach (var result in results.DamagedHits)
            {
                if (result.CurrentHealth <= 0)
                    DamageUp();
            }
        }

        private async void DamageUp()
        {
            CombatStats.Damage += 5;
            await UniTask.Delay(TimeSpan.FromSeconds(4.5f));
            CombatStats.Damage -= 5;
        }
    }
}