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
    public CheeseServiceConfig CheeseServiceConfig;
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
        BindRunStarter();
        BindMaterialHelper();
    }

    private void BindMaterialHelper()
    {
        Container.Bind<MaterialHelper>().FromInstance(new MaterialHelper(DefaultMaterial));
    }
    private void BindRunStarter()
    {
        Container.Bind<RunStarter>().FromInstance(new RunStarter(Container, _coroutineRunner)).AsSingle();
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