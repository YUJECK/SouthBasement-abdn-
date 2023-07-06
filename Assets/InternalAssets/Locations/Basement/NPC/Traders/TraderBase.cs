using System.Collections.Generic;
using SouthBasement.Interactions;
using SouthBasement.InventorySystem;
using SouthBasement.Items;
using SouthBasement.TraderItemDescriptionHUD;
using UnityEngine;

namespace SouthBasement.Basement.NPC.Traders
{
    public abstract class TraderBase : MonoBehaviour
    {
        [SerializeField] private Transform[] tradePoints;
        [SerializeField] private int markup;

        protected List<Item> SpawnedItems = new();
        
        protected abstract ItemsContainer GetItemsContainer();
        protected abstract string TraderName();
        protected abstract TraderHUD GetTraderHUD();
        protected abstract Item GetItem();
        protected abstract bool CanRepeat();

        protected void SpawnItems()
        {
            foreach (var tradePoint in tradePoints)
            {
                var item = GetItem();
                
                while (!CanRepeat() && SpawnedItems.Contains(item))
                    item = GetItem();
                
                var spawnedItem = GetItemsContainer().SpawnForTradeItem(item, tradePoint.position, item.ItemPrice + markup, tradePoint);
                
                SpawnedItems.Add(spawnedItem.Item);
                spawnedItem.OnDetected += ShowItemInfo;
                spawnedItem.OnDetectionReleased += (_) => GetTraderHUD().Disable();
            }
        }
        
        private void ShowItemInfo(IInteractive interactive)
        {
            var item = interactive as TradeItem;
            GetTraderHUD().ShowItemInfo(item.Item, TraderName());
        }
    }
}