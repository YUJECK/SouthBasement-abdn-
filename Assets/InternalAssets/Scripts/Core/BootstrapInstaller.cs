using TheRat;
using TheRat.InputServices;
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
        Container
            .BindInterfacesTo<InputSystemService>()
            .FromInstance(new InputSystemService())
            .AsSingle()
            .NonLazy();
    }
}