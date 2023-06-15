using TheRat.Infrastucture;
using TheRat;
using TheRat.Characters;
using TheRat.Economy;
using TheRat.InputServices;
using TheRat.InventorySystem;
using Zenject;

public sealed class BootstrapInstaller : MonoInstaller
{
    public CheeseServiceConfig CheeseServiceConfig;
    public CoroutineRunner CoroutineRunnerPrefab;
    
    private IInputService _inputService;
    private ICoroutineRunner _coroutineRunner;
    private Inventory _inventory;
    private CharacterStats _characterStats;

    public override void InstallBindings()
    {
        BindCoroutineRunner();
        BindInputMap();
        BindCharacterStats();
        BindInventory();
        BindEconomy();
        BindActiveItemUsager();
        BindWeaponsItemUsager();
    }

    private void BindCoroutineRunner()
    {
        _coroutineRunner = Container.InstantiatePrefabForComponent<ICoroutineRunner>(CoroutineRunnerPrefab);
        Container.Bind<ICoroutineRunner>().FromInstance(_coroutineRunner);
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
        _characterStats = new CharacterStats();
        
        Container
            .Bind<CharacterStats>()
            .FromInstance(_characterStats)
            .AsSingle()
            .NonLazy();

        Container
            .Bind<StaminaController>()
            .FromInstance(new StaminaController(_characterStats, _coroutineRunner))
            .AsSingle();
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
    private void BindWeaponsItemUsager()
    {
        Container
            .Bind<WeaponsUsage>()
            .FromInstance(new WeaponsUsage(_inputService, _inventory, _characterStats))
            .AsSingle();
    }
}