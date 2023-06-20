using SouthBasement.Characters;
using SouthBasement.InventorySystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SouthBasement.HUD
{
    [RequireComponent(typeof(Image))]
    public sealed class PassiveItemsSlot : InventorySlot<PassiveItem>
    {
        private Character _character;
        private ItemDropper _itemDropper;

        [Inject]
        private void Construct(ItemDropper itemDropper, Character character)
        {
            _itemDropper = itemDropper;
            _character = character;
        }
        
        private void Awake()
        {
            DefaultAwake();
            
            GetComponent<Button>().onClick.AddListener(DropItem);
        }

        private void DropItem() 
            => _itemDropper.DropItem(CurrentItem, _character.transform.position, Vector2.down);
    }
}