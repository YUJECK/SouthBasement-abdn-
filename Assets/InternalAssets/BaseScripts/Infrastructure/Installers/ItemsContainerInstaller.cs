using TheRat.InventorySystem;
using TheRat.Items;
using Zenject;

namespace TheRat
{
    public sealed class ItemsContainerInstaller : MonoInstaller
    {
        public Item[] items;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ItemsContainer>()
                .FromInstance(new ItemsContainer(items, Container))
                .AsSingle();
        }
    }
}