using SouthBasement.Economy;
using SouthBasement.InventorySystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace SouthBasement.Items
{
    public sealed class TradeItem : ItemPicker
    {
        [SerializeField] private TMP_Text text;
        
        private CheeseService _cheeseService;

        public int Price { get; set; }

        [Inject]
        private void Construct(CheeseService cheeseService) 
            => _cheeseService = cheeseService;

        public override void SetItem(Item item)
        {
            base.SetItem(item);
            
            Price = item.ItemPrice;
            text.text = Price.ToString();
        }

        public void SetItem(Item item, int price)
        {
            base.SetItem(item);
                
            Price = price;
            text.text = Price.ToString();
        }

        public override void Interact()
        {
            if(_cheeseService.RemoveCheese(Price))
                base.Interact();
        }
    }
}