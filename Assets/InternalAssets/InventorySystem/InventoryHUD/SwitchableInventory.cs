using SouthBasement.HUD.Base;
using SouthBasement.InputServices;
using UnityEngine;
using Zenject;

namespace SouthBasement.HUD
{
    [AddComponentMenu("HUD/Inventory/SwitchableInventory")]
    public sealed class SwitchableInventory : MonoBehaviour
    {
        [SerializeField] private GameObject passiveItems;
        [SerializeField] private HUDWindow inventoryPanel;
        private IInputService _inputSystem;

        [Inject]
        private void Construct(IInputService inputSystem)
            => _inputSystem = inputSystem;

        private void Start()
            => Disable();

        private void OnEnable() 
            => _inputSystem.InventoryOpen += OnInventoryOpen;
        private void OnDisable() 
            => _inputSystem.InventoryOpen -= OnInventoryOpen;

        private void Disable()
        {
            passiveItems.SetActive(false);
            inventoryPanel.Close();
        }

        private void OnInventoryOpen()
        {
            passiveItems.SetActive(!passiveItems.activeSelf);
            inventoryPanel.SetOpened(!inventoryPanel.CurrentlyOpened);
        }
    }
}
