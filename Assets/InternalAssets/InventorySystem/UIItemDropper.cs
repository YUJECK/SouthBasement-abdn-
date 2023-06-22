using SouthBasement.Characters;
using SouthBasement.UI;
using SouthBasement.InventoryHUD.Base;
using UnityEngine;
using Zenject;

namespace SouthBasement.InventorySystem
{
    [RequireComponent(typeof(UIClickableObject))]
    [RequireComponent(typeof(InventorySlot<>))]
    public sealed class UIItemDropper : MonoBehaviour
    {
        private IInventorySlot _inventorySlot;

        private ItemDropper _itemDropper;
        private Character _character;

        [Inject]
        private void Construct(ItemDropper itemDropper, Character character)
        {
            _itemDropper = itemDropper;
            _character = character;
        }
        
        private void Awake()
        {
            GetComponent<UIClickableObject>().OnRightClick += DropItem;
            _inventorySlot = GetComponent<IInventorySlot>();
        }

        private void DropItem()
        {
            if(_inventorySlot.CurrentItemNonGeneric != null)
                _itemDropper.DropItem(_inventorySlot.CurrentItemNonGeneric, _character.transform.position);
        }
    }
}