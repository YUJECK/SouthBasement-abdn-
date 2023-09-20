using SouthBasement.Infrastucture;
using SouthBasement.Characters;
using SouthBasement.Economy;
using SouthBasement;
using SouthBasement.InputServices;
using SouthBasement.InventorySystem;
using SouthBasement.Helpers;
using UnityEngine;
using Zenject;

public sealed class BootstrapInstaller : MonoInstaller
{
    public CoroutineRunner CoroutineRunnerPrefab;
    public Material DefaultMaterial;
    
    private IInputService _inputService;
    private ICoroutineRunner _coroutineRunner;
    private Inventory _inventory;
    private CharacterStats _characterStats;

    public override void InstallBindings()
    {
        BindCoroutineRunner();
        BindInputMap();
        BindEconomy();
        BindMaterialHelper();
        BindRunController();
    }

    private void BindMaterialHelper()
    {
        Container.Bind<MaterialHelper>().FromInstance(new MaterialHelper(DefaultMaterial));
    }
    private void BindRunController()
    {
        var RunDatabase = new RunDatabase(Container.Resolve<DiContainer>(), _inputService);
        
        Container.Bind<RunDatabase>().FromInstance(RunDatabase).AsSingle();
        Container.Bind<RunController>().FromNew().AsSingle();
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
            .FromInstance(new CheeseService(Container))
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