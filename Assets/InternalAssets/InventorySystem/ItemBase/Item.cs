using System;
using UnityEngine;

namespace SouthBasement.InventorySystem
{
    public abstract class Item : ScriptableObject
    {
        [field: SerializeField] public string ItemID { get; private set; }
        [field: SerializeField] public string ItemCategory { get; private set; } = "any";
        [field: SerializeField] public Rarity Rarity { get; private set; } 
        [field: SerializeField] public Sprite ItemSprite { get; private set; }

        public abstract Type GetItemType();
    }
}