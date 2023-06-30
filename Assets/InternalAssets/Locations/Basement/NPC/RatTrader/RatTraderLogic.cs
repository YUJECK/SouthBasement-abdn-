using SouthBasement.Helpers;
using SouthBasement.Items;
using UnityEngine;
using Zenject;

namespace TheRat
{
    public class RatTraderLogic : MonoBehaviour
    {
        [SerializeField] private Transform[] tradePoints;
        [SerializeField] private int markup = 2;
        
        private ItemsContainer _itemsContainer;

        [Inject]
        private void Construct(ItemsContainer itemsContainer)
        {
            _itemsContainer = itemsContainer;
        }

        private void Start()
        {
            CreateTradeItems();
        }

        private void CreateTradeItems()
        {
            foreach (var tradePoint in tradePoints)
            {
                var item = _itemsContainer.GetRandomInRarity(ChanceSystem.GetRandomRarity());
                _itemsContainer.SpawnForTradeItem(item, tradePoint.position, item.ItemPrice + markup);
            }
        }
    }
}
