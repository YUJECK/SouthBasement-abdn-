using System;

namespace SouthBasement.InventorySystem
{
    public interface IInventoryContainer
    {
        Type ContainerType { get; }
        int ItemsCount { get; }
        
        event Action<Type, Item> OnAdded;
        event Action<Type, Item> OnRemoved;
        
        void Init<TContainerType>() where TContainerType : Item;
        Item GetItem(string id);
        bool TryGetItem(string id, out Item item);
        Item[] GetAllInContainer();
        bool RemoveItem(string id, Action<Type, Item> callback = null);
        bool TryAddItem(Item item, Action<Type, Item> callback = null);
    }
}