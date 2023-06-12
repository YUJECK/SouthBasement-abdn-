using System;
using TheRat.InventorySystem;
using UnityEngine;

namespace TheRat.Tests
{
    [CreateAssetMenu]
    public sealed class TestItem : Item, IUsableItem
    {
        public event Action OnUsed;
        
        public void Use()
        {
            Debug.Log("DKLFjsd;lkfjda;slfjlk");
        }
    }
}