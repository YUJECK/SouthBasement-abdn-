using System;
using Cysharp.Threading.Tasks;
using SouthBasement.Helpers;
using SouthBasement.Interactions;
using SouthBasement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SouthBasement.Characters
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class DoorInteraction : MonoBehaviour, IInteractive
    {
        public event Action<IInteractive> OnDetected;
        public event Action<IInteractive> OnInteracted;
        public event Action<IInteractive> OnDetectionReleased;
        
        private MaterialHelper _materialHelper;
        private SpriteRenderer _spriteRenderer;
        private RunController _runController;

        [Inject]
        private void Construct(MaterialHelper materialHelper, RunController runController)
        {
            _materialHelper = materialHelper;
            _runController = runController;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Detect()
            => _spriteRenderer.material = _materialHelper.OutlineMaterial;

        public async void Interact()
        {
            FindObjectOfType<BlackoutTransition>().PlayBlackout();
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            _runController.StartRun();
        }

        public void DetectionReleased()
            => _spriteRenderer.material = _materialHelper.DefaultMaterial;
    }
}