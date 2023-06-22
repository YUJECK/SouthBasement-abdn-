using SouthBasement.InputServices;
using UnityEngine;
using Zenject;

namespace SouthBasement.HUD
{
    [AddComponentMenu("HUD/Inventory/SwitchableInventory")]
    public sealed class SwitchableInventory : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objects;
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
            foreach (var gameObject in _objects)
                gameObject.SetActive(false);
        }

        private void OnInventoryOpen()
        {
            foreach (var gameObject in _objects)
                gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
