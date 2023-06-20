using SouthBasement.Infrastucture;
using SouthBasement;
using SouthBasement.Characters;
using SouthBasement.Economy;
using SouthBasement.InputServices;
using SouthBasement.InventorySystem;
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
        BindEconomy();
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

    private void BindInputMap()
    {
        _inputService = new InputSystemService();
        
        Container
            .BindInterfacesTo<InputSystemService>()
            .FromInstance(_inputService)
            .AsSingle()
            .NonLazy();
    }
}