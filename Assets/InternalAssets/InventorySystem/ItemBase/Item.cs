using System;
using System.Collections.Generic;
using SouthBasement.Items;
using UnityEngine;

namespace SouthBasement.InternalAssets.InventorySystem.ItemBase
{
    public abstract class Item : ScriptableObject
    {
        [field: SerializeField] public string ItemNameEntryName { get; private set; }
        [field: TextArea(2, 10),SerializeField] public string ItemDescriptionEntryName { get; private set; }
        [field: SerializeField] public string ItemID { get; private set; }
        [field: SerializeField] public List<ItemsTags> ItemTags { get; private set; } = new();
        [field: SerializeField] public Rarity Rarity { get; private set; } 
        [field: SerializeField] public Sprite ItemSprite { get; private set; }
        [field: SerializeField] public int ItemPrice { get; private set; }

        public event Action<Sprite> OnItemSpriteChanged;
        
        public virtual void OnAddedToInventory() {}
        public virtual void OnRemovedFromInventory() {}
        public abstract string GetStatsDescription();
        public abstract Type GetItemType();

        protected void UpdateSprite(Sprite sprite)
        {
            OnItemSpriteChanged?.Invoke(sprite);
        }
    }
}