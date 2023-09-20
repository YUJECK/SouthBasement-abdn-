using System.Collections;
using System.Collections.Generic;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters.Base
{
    public sealed class WeaponSpriteSetter : MonoBehaviour
    {
        private SpriteRenderer _weaponSprite;

        private WeaponsUsage _weaponsUsage;
        
        [Inject]
        private void Construct(WeaponsUsage weaponsUsage)
            => _weaponsUsage = weaponsUsage;

        private void Awake()
        {
            _weaponSprite = GetComponent<SpriteRenderer>();
            SetItem(_weaponsUsage.CurrentWeapon);
        }

        private void OnEnable()
        {
            _weaponsUsage.OnSelected += SetItem;
            _weaponsUsage.OnSelectedNull += SetNull;
        }

        private void OnDisable()
        {
            _weaponsUsage.OnSelected -= SetItem;
            _weaponsUsage.OnSelectedNull -= SetNull;
        }

        private void SetNull()
        {
            _weaponSprite.color = Color.clear;
        }

        private void SetItem(WeaponItem item)
        {
            if (item == null)
            {
                SetNull();
                return;
            }
                
            UpdateSprite(item.ItemSprite);
            _weaponSprite.color = Color.white;
            item.OnItemSpriteChanged += UpdateSprite;
        }

        private void UpdateSprite(Sprite itemSprite)
            => _weaponSprite.sprite = itemSprite;
    }
}