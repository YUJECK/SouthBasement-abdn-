using System;
using SouthBasement.InputServices;
using UnityEngine;
using Zenject;

namespace SouthBasement.HUD
{
    public sealed class SwitchableInventory : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objects;
        
        [Inject]
        private void Construct(IInputService inputSystem)
        {
            inputSystem.InventoryOpen += OnInventoryOpen;
        }

        private void Awake()
            => Disable();

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
