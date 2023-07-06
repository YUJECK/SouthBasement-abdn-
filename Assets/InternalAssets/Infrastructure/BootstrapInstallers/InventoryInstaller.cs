using SouthBasement.InputServices;
using SouthBasement.InventorySystem;
using SouthBasement.Characters.Stats;
using Zenject;

namespace SouthBasement
{
    public sealed class InventoryInstaller : MonoInstaller
    {
        private Inventory _inventory;
        private CharacterAttackStats _attackStats;
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService, CharacterAttackStats attackStats)
        {
            _inputService = inputService;
            _attackStats = attackStats;
        }

        public override void InstallBindings()
        {
            BindInventory();
            BindActiveItemUsager();
            BindWeaponsItemUsager();
            BindPassiveItemUsager();
        }

        private void BindInventory()
        {
            _inventory = new Inventory(Container);
        
            Container
                .BindInterfacesAndSelfTo<Inventory>()
                .FromInstance(_inventory)
                .AsSingle();
        }
        private void BindActiveItemUsager()
        {
            Container
                .Bind<ActiveItemUsage>()
                .FromInstance(new ActiveItemUsage(_inputService, _inventory))
                .AsSingle();    
        }
        private void BindWeaponsItemUsager()
        {
            Container
                .Bind<WeaponsUsage>()
                .FromInstance(new WeaponsUsage(_inventory, _attackStats))
                .AsSingle();
        }
        private void BindPassiveItemUsager()
        {
            Container
                .BindInterfacesTo<PassiveItemsUsage>()
                .FromInstance(new PassiveItemsUsage(_inventory))
                .AsSingle();
        }

    }
}