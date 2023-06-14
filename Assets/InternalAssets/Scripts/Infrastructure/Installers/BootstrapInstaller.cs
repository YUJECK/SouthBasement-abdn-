using TheRat;
using TheRat.Economy;
using TheRat.InputServices;
using TheRat.InventorySystem;
using Zenject;

public sealed class BootstrapInstaller : MonoInstaller
{
    public CheeseServiceConfig CheeseServiceConfig;

    private IInputService _inputService;
    private Inventory _inventory;
    
    public override void InstallBindings()
    {
        BindInputMap();
        BindCharacterStats();
        BindInventory();
        BindEconomy();
        BindActiveItemUsager();
    }

    private void BindEconomy()
    {
        Container
            .Bind<CheeseService>()
            .FromInstance(new CheeseService(CheeseServiceConfig, Container))
            .AsSingle();
    }

    private void BindCharacterStats()
    {
        Container
            .Bind<CharacterStats>()
            .AsSingle()
            .NonLazy();
    }

    private void BindInputMap()
    {
        _inputService = new InputSystemService();
        
        Container
            .BindInterfacesTo<InputSystemService>()
            .FromInstance(_inputService)
            .AsSingle()
            .NonLazy();
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
}