using System.Collections.Generic;
using NaughtyAttributes;
using SouthBasement.InventorySystem;
using SouthBasement.Items;
using Zenject;

namespace SouthBasement
{
    public sealed class ItemsContainerInstaller : MonoInstaller
    {
        public List<Item> items;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ItemsContainer>()
                .FromInstance(new ItemsContainer(items.ToArray(), Container))
                .AsSingle();
        }

        [Button()]
        public void ClearNulls()
        {
            items.RemoveAll((item) => item == null);
        }
    }
}