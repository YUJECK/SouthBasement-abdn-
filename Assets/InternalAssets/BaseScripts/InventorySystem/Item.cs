using UnityEngine;

namespace TheRat.InventorySystem
{
    public class Item : ScriptableObject
    {
        [field: SerializeField] public string ItemID { get; private set; } 
        [field: SerializeField] public Rarity Rarity { get; private set; } 
        [field: SerializeField] public Sprite ItemSprite { get; private set; } 
    }
}