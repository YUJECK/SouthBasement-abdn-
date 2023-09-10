using SouthBasement.InternalAssets.InventorySystem.ItemBase;
using UnityEngine;
using System;

namespace SouthBasement.InternalAssets.Items.Weapons.BloodthirstyFalchion
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Weapon + "BloodthirstyFalchion")]
    public sealed class BloodthirstyFalchion : WeaponItem
    {
        [SerializeField] private int hungryDamage = 5;
        [SerializeField] private Sprite hungrySprite;

        [SerializeField] private int startingDamage;
        [SerializeField] private int _uppedHits = 0;
        
        public override Type GetItemType()
            => typeof(WeaponItem);

        private void Awake()
        {
            startingDamage = CombatStats.Damage;
        }

        public override void OnAttack(AttackResult results)
        {
            foreach (var result in results.DamagedHits)
            {
                if (result.CurrentHealth <= 0)
                    DamageUp();
            }

            if(results.DamagedHits.Count > 0)
                _uppedHits--;
            
            CombatStats.Damage = GetDamage();
        }

        private int GetDamage()
        {
            if (_uppedHits > 0)
                return startingDamage + hungryDamage * _uppedHits;

            UpdateSprite(ItemSprite);
            return startingDamage;
        }

        private void DamageUp()
        {
            _uppedHits += 2;
            UpdateSprite(hungrySprite);
        }
    }
}