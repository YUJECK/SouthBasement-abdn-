using SouthBasement.Economy;
using SouthBasement.InventorySystem;
using SouthBasement.InventorySystem.ItemBase;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public sealed class RubbishDealerLogic : MonoBehaviour
    {
        private Inventory _inventory;
        private CheeseService _cheeseService;

        [Inject]
        private void Construct(Inventory inventory, CheeseService cheeseService)
        {
            _inventory = inventory;
            _cheeseService = cheeseService;
        }

        public void SoldAll()
        {
            var rubbish = _inventory.ItemsContainer.GetAllInContainer<RubbishItem>();

            foreach (var item in rubbish)
            {
                _inventory.RemoveItem(item.ItemID);
                _cheeseService.AddCheese(item.ItemPrice);
            }
        }
    }
}