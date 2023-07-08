using SouthBasement.CameraHandl;
using UnityEngine;
using Zenject;

namespace SouthBasement.Locations
{
    public sealed class MainCameraInstaller : MonoInstaller
    {
        [SerializeField] private Camera mainCamera;
        
        public override void InstallBindings()
        {
            Container.Bind<IMainCameraContainer>().FromInstance(new MainCameraContainer(mainCamera));
        }
    }
}