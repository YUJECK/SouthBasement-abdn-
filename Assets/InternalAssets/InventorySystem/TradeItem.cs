using SouthBasement.Economy;
using SouthBasement.InventorySystem;
using Zenject;

namespace TheRat.Items
{
    public sealed class TradeItem : ItemPicker
    {
        private CheeseService _cheeseService;

        public int Price { get; set; } = default;

        [Inject]
        private void Construct(CheeseService cheeseService) 
            => _cheeseService = cheeseService;

        public override void Interact()
        {
            if(_cheeseService.RemoveCheese(Price))
                base.Interact();
        }
    }
}