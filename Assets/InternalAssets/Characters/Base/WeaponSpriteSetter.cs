using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters.Base
{
    public sealed class WeaponSpriteSetter : MonoBehaviour
    {
        private SpriteRenderer _weaponSprite;
        
        [Inject]
        private void Construct(WeaponsUsage weaponsUsage)
        {
            weaponsUsage.OnSelected += item =>
            {
                _weaponSprite.sprite = item.ItemSprite;
                _weaponSprite.color = Color.white;
            };
            weaponsUsage.OnSelectedNull += () => _weaponSprite.color = Color.clear;
        }

        private void Awake()
            => _weaponSprite = GetComponent<SpriteRenderer>();
    }
}