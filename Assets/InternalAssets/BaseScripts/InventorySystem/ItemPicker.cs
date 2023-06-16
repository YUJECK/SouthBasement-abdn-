using TheRat.Interactions;
using TheRat.Tests;
using UnityEngine;
using Zenject;

namespace TheRat.InventorySystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ItemPicker : MonoBehaviour, IInteractive
    {
        [SerializeField] private Item item;

        private SpriteRenderer _spriteRenderer;
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory, DiContainer diContainer)
        {
            diContainer.Inject(item);
            _inventory = inventory;
        } 

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if(item == null)
                Destroy(gameObject);
            
            SetItem(item);
        }

        public void SetItem(Item item)
        {
            this.item = item;
            _spriteRenderer.sprite = this.item.ItemSprite;
        } 

        public void Detect() { /*тут короче надо ставить материал*/ }

        public void Interact()
        {
            _inventory.AddItem(item);            
        }

        public void DetectionReleased()
        {
            /*тут короче надо убирать материал*/
        }
    }
}