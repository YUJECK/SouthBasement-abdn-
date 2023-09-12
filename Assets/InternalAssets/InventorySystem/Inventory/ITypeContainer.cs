using System;
using System.Collections.Generic;
using SouthBasement.InventorySystem.ItemBase;

namespace SouthBasement.InventorySystem
{
    public interface ITypeContainer
    {
        Type ContainerType { get; }
        int ItemsCount { get; }
        event Action<Item> OnAdded;
        event Action<string> OnRemoved;

        void Init<TContainerType>()
            where TContainerType : Item;

        void Init(Type type);
        bool TryGetItem(string id, out Item item);
        Item[] GetAllInContainer();
        bool RemoveItem(string id);
        bool TryAddItem(Item item, Action<Item> callback = null);
    }
}