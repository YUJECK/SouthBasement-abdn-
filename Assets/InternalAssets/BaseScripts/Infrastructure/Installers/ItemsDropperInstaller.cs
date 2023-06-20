using SouthBasement.InventorySystem;
using SouthBasement.Items;
using Zenject;

namespace SouthBasement
{
    public sealed class ItemsDropperInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            ItemDropper itemsDropper = new(Container.Resolve<Inventory>(), Container.Resolve<ItemsContainer>());

            Container
                .Bind<ItemDropper>()
                .FromInstance(itemsDropper)
                .AsSingle();
        }
    }
}