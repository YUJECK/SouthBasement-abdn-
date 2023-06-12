using TheRat;
using TheRat.Economy;
using TheRat.InputServices;
using Zenject;

public sealed class BootstrapInstaller : MonoInstaller
{
    public CheeseServiceConfig CheeseServiceConfig;
    
    public override void InstallBindings()
    {
        BindInputMap();
        BindCharacterStats();
        BindEconomy();
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
        Container
            .BindInterfacesTo<InputSystemService>()
            .FromInstance(new InputSystemService())
            .AsSingle()
            .NonLazy();
    }
}