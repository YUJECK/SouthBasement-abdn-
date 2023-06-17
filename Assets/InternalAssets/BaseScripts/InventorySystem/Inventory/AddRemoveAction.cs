using System;

namespace TheRat.InventorySystem
{
    public sealed class AddRemoveAction<TType> where TType : Item
    {
        public event Action<TType> OnAdded; 
        public event Action<TType> OnRemoved;

        //да простит меня Господь
        public void InvokeAdded(TType item) => OnAdded?.Invoke(item);
        public void InvokeRemoved(TType item) => OnRemoved?.Invoke(item);
    }
}