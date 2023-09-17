using UnityEngine;
using System;
using SouthBasement.InventorySystem.ItemBase;

namespace SouthBasement.Items.Weapons.BloodthirstyFalchion
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Weapon + "BloodthirstyFalchion")]
    public sealed class BloodthirstyFalchion : WeaponItem
    {
        [SerializeField] private int hungryDamage = 5;
        [SerializeField] private Sprite hungrySprite;

        [SerializeField] private int startingDamage;
        
        private bool _boosted = false;
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
            if(results.DamagedHits.Count > 0)
                DamageUpDisable();
            
            foreach (var result in results.DamagedHits)
            {
                if (result.CurrentHealth <= 0)
                    DamageUpEnable();
            }
            
            CombatStats.Damage = GetDamage();
        }

        private int GetDamage()
        {
            if (_boosted)
            {
                UpdateSprite(hungrySprite);
                return startingDamage + GetHungryDamage();
            }

            ItemSprite = _defaultSprite;
            UpdateSprite(ItemSprite);
            
            return startingDamage;
        }

        private int GetHungryDamage()
            => hungryDamage;

        private void DamageUpEnable()
        {
            _boosted = true;
            ItemSprite = hungrySprite;
            UpdateSprite(hungrySprite);
        }
        
        private void DamageUpDisable()
        {
            _boosted = false;
            ItemSprite = _defaultSprite;
            UpdateSprite(hungrySprite);
        }
    }
}