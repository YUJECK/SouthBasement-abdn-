using System;
using SouthBasement.InternalAssets.InventorySystem.ItemBase;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SouthBasement.InventorySystem
{
    [AddComponentMenu("HUD/Inventory/ActiveItemSlot")]
    public sealed class ActiveItemSlot : InventorySlot<ActiveItem>
    {
        private ActiveItemUsage _activeItemUsage;
        [SerializeField] private GameObject isCurrent;

        [Inject]
        private void Construct(ActiveItemUsage activeItemUsage)
        {
            _activeItemUsage = activeItemUsage;
            _activeItemUsage.OnSelected += CheckCurrent;
        }

        private void CheckCurrent(ActiveItem item)
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
            GetComponentInParent<Button>().onClick.AddListener(SetCurrent);
            OnSetted += CheckCurrent;
        }

        private void Start()
            => DefaultStart();

        private void SetCurrent()
        {
            if(CurrentItem == null)
                return;
            
            _activeItemUsage.SetCurrent(CurrentItem);
        }
    }
}