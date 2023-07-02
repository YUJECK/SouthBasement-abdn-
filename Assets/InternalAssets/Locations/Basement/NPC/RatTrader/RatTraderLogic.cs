using System;
using SouthBasement.Helpers;
using SouthBasement.Interactions;
using SouthBasement.Items;
using SouthBasement.Scripts.Helpers;
using SouthBasement.TraderItemDescriptionHUD;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public class RatTraderLogic : MonoBehaviour
    {
        [SerializeField] private Transform[] tradePoints;
        [SerializeField] private int markup = 2;
        
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
            CreateTradeItems();
            
            GetComponentInChildren<TriggerCallback>().OnTriggerExit += (_) => _traderHUD.Disable();
        }

        private void CreateTradeItems()
        {
            foreach (var tradePoint in tradePoints)
            {
                var item = _itemsContainer.GetRandomInRarity(ChanceSystem.GetRandomRarity());
                var spawnedItem = _itemsContainer.SpawnForTradeItem(item, tradePoint.position, item.ItemPrice + markup);
                
                spawnedItem.OnDetected += ShowItemInfo;
                //spawnedItem.OnDetectionReleased += (_) => _traderHUD.Disable();
            }
        }

        private void ShowItemInfo(IInteractive interactive)
        {
            var item = interactive as TradeItem;
            _traderHUD.ShowItemInfo(item.Item, "Trader");
        }
    }
}
