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

        private Sprite _defaultSprite;
        
        public override Type GetItemType()
            => typeof(WeaponItem);

        private void Awake()
        {
            startingDamage = CombatStats.Damage;
            _defaultSprite = ItemSprite;
        }

        public override void OnAttack(AttackResult results)
        {
            if(_uppedHits > 0 && results.DamagedHits.Count > 0)
                _uppedHits--;
            
            foreach (var result in results.DamagedHits)
            {
                if (result.CurrentHealth <= 0)
                    DamageUp();
            }
            
            CombatStats.Damage = GetDamage();
        }

        private int GetDamage()
        {
            if (_uppedHits > 0)
            {
                UpdateSprite(hungrySprite);
                return startingDamage + GetHungryDamage();
            }

            ItemSprite = _defaultSprite;
            UpdateSprite(ItemSprite);
            
            return startingDamage;
        }

        private int GetHungryDamage()
        {
            var damage = hungryDamage * _uppedHits;

            if (damage > 50)
                damage = 50;
            
            return damage;
        }

        private void DamageUp()
        {
            _uppedHits += 2;
            ItemSprite = hungrySprite;
            UpdateSprite(hungrySprite);
        }
    }
}