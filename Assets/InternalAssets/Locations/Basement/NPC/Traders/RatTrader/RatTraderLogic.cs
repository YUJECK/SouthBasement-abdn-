using SouthBasement.Basement.NPC.Traders;
using SouthBasement.Helpers;
using SouthBasement.InventorySystem;
using SouthBasement.Items;
using SouthBasement.Scripts.Helpers;
using SouthBasement.TraderItemDescriptionHUD;
using Zenject;

namespace SouthBasement
{
    public class RatTraderLogic : TraderBase
    {
        private ItemsContainer _itemsContainer;
        private TraderHUD _traderHUD;

        [Inject]
        private void Construct(ItemsContainer itemsContainer, TraderHUD traderHUD)
        {
            _itemsContainer = itemsContainer;
            _traderHUD = traderHUD;
        }

        private void Start()
        {
            SpawnItems();
            
            GetComponentInChildren<TriggerCallback>().OnTriggerExit += (_) => _traderHUD.Disable();
        }

        protected override ItemsContainer GetItemsContainer() => _itemsContainer;
        protected override string TraderName() => "Rat Trader";
        protected override TraderHUD GetTraderHUD() => _traderHUD;
        protected override Item GetItem() => _itemsContainer.GetRandomInRarity(ChanceSystem.GetRandomRarity());
        protected override bool CanRepeat() => false;
    }
}
