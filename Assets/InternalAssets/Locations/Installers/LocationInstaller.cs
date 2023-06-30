using SouthBasement.Characters;
using SouthBasement.Generation;
using SouthBasement.Helpers;
using SouthBasement.CameraHandl;
using SouthBasement.Characters.Rat;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace SouthBasement.Locations
{
    public sealed class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Transform startPoint;
        [FormerlySerializedAs("roomsContainer")] [SerializeField] private LevelConfig levelConfig;
        [SerializeField] private ContainersHelper containersHelper;

        [SerializeField] private CursorService cursorService;

        public override void InstallBindings()
        {
            BindCharacter();
            BindRoomContainer();
            BindGeneration();
            BindCameras();

            Container
                .Bind<CursorService>()
                .FromInstance(cursorService)
                .AsSingle();
            Container
                .Bind<ContainersHelper>()
                .FromInstance(containersHelper)
                .AsSingle();
        }

        private void BindCameras()
        {
            var camerasContainerPrefab = Resources.Load<CamerasContainer>(ResourcesPathHelper.CamerasContainer);
            var camerasReactingPrefab = Resources.Load<CameraGameStatesReacting>(ResourcesPathHelper.CamerasReacting);
            
            var cameraContainer = Container.InstantiatePrefabForComponent<CamerasContainer>(camerasContainerPrefab, startPoint);

            var cameraHandler = new CameraHandler(cameraContainer);

            Container
                .Bind<CameraHandler>()
                .FromInstance(cameraHandler)
                .AsSingle();
            
            Container.InstantiatePrefabForComponent<CameraGameStatesReacting>(camerasReactingPrefab, startPoint);
        }

        private void BindRoomContainer()
        {
            Container
                .Bind<LevelConfig>()
                .FromInstance(levelConfig)
                .AsSingle();
        }

        private void BindCharacter()
        {
            Character characterPrefab = Resources.Load<Character>(ResourcesPathHelper.RatPrefab);

            Character character =
                Container
                    .InstantiatePrefab(characterPrefab, startPoint.position, startPoint.rotation, null)
                    .GetComponent<Character>();

            Container
                .Bind<Character>()
                .FromInstance(character)
                .AsSingle();
            
            Container
                .BindInterfacesTo<RatCharacter>()
                .FromInstance(character)
                .AsSingle();
        }

        private void BindGeneration()
        {
            Container
                .BindInterfacesAndSelfTo<GenerationController>()
                .FromInstance(new GenerationController(levelConfig, Container, startPoint))
                .AsSingle();
        }
    }
}