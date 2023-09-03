using SouthBasement.InternalAssets.InventorySystem.ItemBase;
using SouthBasement.InventorySystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SouthBasement.InventorySystem.Weapons
{
    [AddComponentMenu("HUD/Inventory/WeaponSlot")]
    public sealed class WeaponSlot : InventorySlot<WeaponItem>
    {
        [SerializeField] private CurrentItemChecker<WeaponSlot, WeaponItem> currentItemChecker = new();

        private WeaponsUsage _weaponUsage;

        [Inject]
        private void Construct(WeaponsUsage weapon) 
            => _weaponUsage = weapon;

        private void Start()
        {
            GetComponentInParent<Button>().onClick.AddListener(SetCurrent);
            
            currentItemChecker.Init(this);
            _weaponUsage.OnSelected += currentItemChecker.CheckCurrent;
            
            DefaultStart();
        }
        
        private void OnDestroy()
        {
            _weaponUsage.OnSelected += currentItemChecker.CheckCurrent;
            currentItemChecker.Dispose();
        }

        private void SetCurrent()
        {
            if(CurrentItem == null)
                return;
            
            _weaponUsage.SetCurrent(CurrentItem);
        }
    }
}