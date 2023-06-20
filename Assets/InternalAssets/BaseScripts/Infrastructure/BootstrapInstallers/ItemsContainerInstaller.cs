using SouthBasement.InventorySystem;
using SouthBasement.Items;
using Zenject;

namespace SouthBasement
{
    public sealed class ItemsContainerInstaller : MonoInstaller
    {
        public ItemPicker ItemPickerPrefab;
        public Item[] items;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ItemsContainer>()
                .FromInstance(new ItemsContainer(items, ItemPickerPrefab, Container))
                .AsSingle();
        }
    }
}