using System;
using Cysharp.Threading.Tasks;
using SouthBasement.InternalAssets.InventorySystem.ItemBase;
using SouthBasement.InventorySystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace SouthBasement.InternalAssets.Items.Weapons.BloodthirstyFalchion
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Weapon + "BloodthirstyFalchion")]
    public sealed class BloodthirstyFalchion : WeaponItem
    {
        [SerializeField] private int hungryDamage = 5;
        [SerializeField] private Sprite hungrySprite;
        
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
            CombatStats.Damage += hungryDamage;
            UpdateSprite(hungrySprite);

            await UniTask.Delay(TimeSpan.FromSeconds(5.5f));
            
            UpdateSprite(ItemSprite);
            CombatStats.Damage -= hungryDamage;
        }
    }
}