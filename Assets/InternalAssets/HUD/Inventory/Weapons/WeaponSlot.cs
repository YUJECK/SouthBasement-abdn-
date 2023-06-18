using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SouthBasement.InventorySystem.Weapons
{
    [RequireComponent(typeof(Image))]
    public sealed class WeaponSlot : InventorySlot<WeaponItem>
    {
        private WeaponsUsage _weaponUsage;
        [SerializeField] private GameObject isCurrent;

        [Inject]
        private void Construct(WeaponsUsage weapon)
        {
            _weaponUsage = weapon;
            _weaponUsage.OnSelected += CheckCurrent;
        }

        private void CheckCurrent(WeaponItem item)
        {
            if (CurrentItem == null || item == null)
            {
                isCurrent.SetActive(false);
                return;
            }

            if (item.ItemID == CurrentItem.ItemID)
                isCurrent.SetActive(true);
            else
                isCurrent.SetActive(false);
        }

        private void Awake()
        {
            ItemImage = GetComponent<Image>();
            GetComponentInParent<Button>().onClick.AddListener(SetCurrent);
            
            OnSetted += CheckCurrent;
            SetItem(null);
        }

        private void SetCurrent()
        {
            if(CurrentItem == null)
                return;
            
            _weaponUsage.SetCurrent(CurrentItem);
        }
    }
}