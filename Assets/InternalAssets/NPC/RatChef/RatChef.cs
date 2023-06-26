using System.Collections.Generic;
using SouthBasement.Dialogues;
using SouthBasement.InventorySystem;
using SouthBasement.Items;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public class RatChef : MonoBehaviour
    {
        [SerializeField] private Transform[] tradePoints;
        public DialogueContainer DialogueContainer;

        private readonly List<FoodItem> _currentItems = new();
        private ItemsContainer _itemsContainer;
        private IDialogueService _dialogueService;

        [Inject]
        private void Construct(ItemsContainer itemsContainer, IDialogueService dialogueService)
        {
            _itemsContainer = itemsContainer;
            _dialogueService = dialogueService;
        }

        private void Start()
        {
            SpawnItems();
            _dialogueService.StartDialogue(DialogueContainer);
        }

        private void SpawnItems()
        {
            foreach (var point in tradePoints)
            {
                var item = GetFood();
                
                _currentItems.Add(item);
                _itemsContainer.SpawnForTradeItem(item, point.position, 8);
            }
        }

        private FoodItem GetFood()
        {
            FoodItem food = _itemsContainer.GetRandomInTypeAndCategory(typeof(FoodItem),"cockroach_food") as FoodItem;
            
            while (_currentItems.Contains(food))
                food = _itemsContainer.GetRandomInTypeAndCategory(typeof(FoodItem),"cockroach_food") as FoodItem;

            return food;
        }
    }
}
