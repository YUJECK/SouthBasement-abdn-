using SouthBasement.Dialogues;
using SouthBasement.Helpers;
using UnityEngine;
using Zenject;

namespace SouthBasement.Locations
{
    public sealed class DialogueHUDInstaller : MonoInstaller
    {
        [SerializeField] private Transform startPoint;
        
        public override void InstallBindings()
        {
            var DialogueWindowPrefab = GetDialogueWindowPrefab();
            
            var dialogueBehaviour = Container.InstantiatePrefabForComponent<IDialogueService>(DialogueWindowPrefab, startPoint.position,
                startPoint.rotation, null);

            Container
                .Bind<IDialogueService>()
                .FromInstance(dialogueBehaviour)
                .AsSingle();
        }
        
        private GameObject GetDialogueWindowPrefab() => Resources.Load<GameObject>(ResourcesPathHelper.DialogueHUD);
    }
}