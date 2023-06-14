using System;
using TheRat.InventorySystem;
using UnityEngine;

namespace TheRat.Tests
{
    [CreateAssetMenu]
    public sealed class TestItem : ActiveItem
    {
        [SerializeField] private string _message;
        
        public event Action OnUsed;
        
        public override void Use()
        {
            Debug.Log(_message);
            OnUsed?.Invoke();
        }
    }
}