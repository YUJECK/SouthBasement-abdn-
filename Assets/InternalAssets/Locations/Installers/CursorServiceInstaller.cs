using Zenject;

namespace SouthBasement.Locations
{
    public sealed class CursorServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<CursorService>()
                .FromNew()
                .AsSingle();
        }
    }
}