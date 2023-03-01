using TheRat;
using UnityEngine;
using Zenject;

public sealed class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindInputMap();
        BindCharacterStats();
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
        InputMap inputs = new InputMap();
        inputs.Enable();

        Container
            .Bind<InputMap>()
            .FromInstance(inputs)
            .AsSingle()
            .NonLazy();
    }
}