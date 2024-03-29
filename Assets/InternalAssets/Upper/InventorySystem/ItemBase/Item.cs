﻿using System;
using System.Collections.Generic;
using SouthBasement.Items;
using SouthBasement.Localization;
using UnityEngine;

namespace SouthBasement.InventorySystem.ItemBase
{
    public abstract class Item : ScriptableObject
    {
        [field: SerializeField] public LocalizedString ItemName { get; private set; }
        [field: SerializeField] public LocalizedString ItemDescription { get; private set; }
        [field: SerializeField] public string ItemID { get; private set; }
        [field: SerializeField] public List<ItemsTags> ItemTags { get; private set; } = new();
        [field: SerializeField] public Rarity Rarity { get; private set; } 
        [field: SerializeField] public Sprite ItemSprite { get; protected set; }
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