using SouthBasement.Dialogues;
using SouthBasement.Helpers;
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
            
            Container
                .Bind<IDialogueService>()
                .FromInstance(dialogueBehaviour)
                .AsSingle();
        }

        private GameObject GetHUDPrefab()
        {
            return Resources.Load<GameObject>(ResourcesPathHelper.HUD);
        }

        private GameObject GetDialogueWindowPrefab()
        {
            return Resources.Load<GameObject>(ResourcesPathHelper.DialogueHUD); 
        }
        
        public void Initialize()
        {
            Container.InstantiatePrefab(GetHUDPrefab());
        }
    }
}