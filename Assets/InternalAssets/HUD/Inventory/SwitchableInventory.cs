using TheRat.InputServices;
using UnityEngine;
using Zenject;

namespace TheRat.HUD
{
    public sealed class SwitchableInventory : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objects;
        
        [Inject]
        private void Construct(IInputService inputSystem)
        {
            inputSystem.InventoryOpen += OnInventoryOpen;
        }

        private void OnInventoryOpen()
        {
            foreach (var gameObject in _objects)
                gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
