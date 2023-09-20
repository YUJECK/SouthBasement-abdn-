using SouthBasement.CameraHandl;
using SouthBasement.Helpers;
using UnityEngine;
using Zenject;

namespace SouthBasement.Locations
{
    public sealed class CamerasInstaller : MonoInstaller
    {
        [SerializeField] private Transform startPoint;
        
        public override void InstallBindings()
        {
            var camerasContainerPrefab = Resources.Load<CamerasContainer>(ResourcesPathHelper.CamerasContainer);
            var camerasReactingPrefab = Resources.Load<CameraGameStatesReacting>(ResourcesPathHelper.CamerasReacting);
            
            var cameraContainer = Container.InstantiatePrefabForComponent<CamerasContainer>(camerasContainerPrefab, startPoint);

            var cameraHandler = new CameraHandler(cameraContainer);

            Container
                .BindInterfacesAndSelfTo<CameraHandler>()
                .FromInstance(cameraHandler)
                .AsSingle();
            
            Container.InstantiatePrefabForComponent<CameraGameStatesReacting>(camerasReactingPrefab, startPoint);
        }
    }
}