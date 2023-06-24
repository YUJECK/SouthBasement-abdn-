using System;
using SouthBasement.InventorySystem;
using UnityEngine;

namespace SouthBasement.InventorySystem
{
    [Serializable]
    public sealed class CurrentItemChecker<TSlot, TITem> : IDisposable
        where TSlot : InventorySlot<TITem>
        where TITem : Item
    {
        [SerializeField] private GameObject isCurrent;

        private TSlot _slot;

        public void Init(TSlot slot)
        {
            if (_slot != null)
            {
                Debug.LogError("You tried to init slot twice");        
                return;
            }

            _slot = slot;
            _slot.OnSetted += CheckCurrent;
            
            CheckCurrent(_slot.CurrentItem);
        }

        public void Dispose()
        {
            _slot.OnSetted -= CheckCurrent;
            _slot = null;
        }

        public void CheckCurrent(TITem item)
        {
            if(_slot == null) return;
            if(isCurrent == null) return;

            isCurrent.SetActive(IsCurrent(item));

            bool IsCurrent(TITem itemToCheck)
                => itemToCheck != null && _slot.CurrentItem != null && itemToCheck.ItemID == _slot.CurrentItem.ItemID;
        }
    }
}