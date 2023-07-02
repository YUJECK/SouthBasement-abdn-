using SouthBasement.Dialogues;
using SouthBasement.Helpers;
using SouthBasement.TraderItemDescriptionHUD;
using UnityEngine;
using Zenject;

namespace SouthBasement.Locations
{
    public sealed class LocationHUDInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private Transform startPoint;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LocationHUDInstaller>().FromInstance(this).AsSingle();
            
            var DialogueWindowPrefab = GetDialogueWindowPrefab();

            var dialogueBehaviour = Container.InstantiatePrefabForComponent<IDialogueService>(DialogueWindowPrefab, startPoint.position,
                startPoint.rotation, null);
            
            var traderHUD = Container.InstantiatePrefabForComponent<TraderHUD>(GetTraderHUDPrefab(), startPoint.position, startPoint.rotation, null);

            Container
                .Bind<IDialogueService>()
                .FromInstance(dialogueBehaviour)
                .AsSingle();

            Container
                .Bind<TraderHUD>()
                .FromInstance(traderHUD)
                .AsSingle();
        }

        private GameObject GetHUDPrefab() => Resources.Load<GameObject>(ResourcesPathHelper.HUD);
        private GameObject GetDialogueWindowPrefab() => Resources.Load<GameObject>(ResourcesPathHelper.DialogueHUD);
        private GameObject GetTraderHUDPrefab() => Resources.Load<GameObject>(ResourcesPathHelper.TraderHUD);
        public void Initialize() => Container.InstantiatePrefab(GetHUDPrefab());
    }
}