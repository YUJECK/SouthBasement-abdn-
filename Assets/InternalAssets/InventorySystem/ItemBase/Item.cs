using System;
using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement.InventorySystem
{
    public abstract class Item : ScriptableObject
    {
        [field: SerializeField] public string ItemName { get; private set; }
        [field: SerializeField] public string ItemDescription { get; private set; }
        [field: SerializeField] public string ItemID { get; private set; }
        [field: SerializeField] public List<string> ItemTags { get; private set; } = new();
        [field: SerializeField] public Rarity Rarity { get; private set; } 
        [field: SerializeField] public Sprite ItemSprite { get; private set; }
        [field: SerializeField] public int ItemPrice { get; private set; }

        public abstract string GetStatsDescription();

        public virtual void Init() {}
        
        public abstract Type GetItemType();
    }
}