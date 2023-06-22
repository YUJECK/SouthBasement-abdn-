using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement.HUD
{
    [RequireComponent(typeof(ISlotHUD))]
    [AddComponentMenu("HUD/Inventory/InventoryStartUpdater")]
    public sealed class InventoryAwakeUpdater : MonoBehaviour
    {
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory)
            => _inventory = inventory;

        private void Awake()
        {
            var huds = GetComponents<ISlotHUD>();

            foreach (var hud in huds)
                hud.UpdateInventory(_inventory);
        }
    }
}