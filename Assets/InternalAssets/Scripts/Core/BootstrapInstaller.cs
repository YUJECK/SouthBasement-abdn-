using TheRat;
using Zenject;

public sealed class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InputMap inputs = new InputMap();
        inputs.Enable();

        Container
            .Bind<InputMap>()
            .FromInstance(inputs)
            .AsSingle()
            .NonLazy();
    }
}