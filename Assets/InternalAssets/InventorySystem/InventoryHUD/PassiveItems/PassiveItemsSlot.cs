using SouthBasement.Characters;
using SouthBasement.InventorySystem;
using TheRat.InternalAssets.Characters.Base;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SouthBasement.HUD
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("HUD/Inventory/PassiveItemsSlot")]
    public sealed class PassiveItemsSlot : InventorySlot<PassiveItem>
    {
        private Character _character;
        private ItemDropper _itemDropper;

        [Inject]
        private void Construct(ItemDropper itemDropper, CharacterFactory character)
        {
            _itemDropper = itemDropper;
            _character = character.Instance;
        }
        
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(DropItem);

            DefaultStart();
        }

        private void DropItem() 
            => _itemDropper.DropItem(CurrentItem, _character.transform.position, Vector2.down);
    }
}