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
            weaponsUsage.OnSelected += item => _weaponSprite.sprite = item.ItemSprite;
        }

        private void Awake()
        {
            _weaponSprite = GetComponent<SpriteRenderer>();
        }
    }
}