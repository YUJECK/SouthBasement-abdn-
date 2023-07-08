using System;
using Cysharp.Threading.Tasks;
using SouthBasement.Helpers;
using SouthBasement.Interactions;
using TheRat;
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

        [Inject]
        private void Construct(MaterialHelper materialHelper)
        {
            _materialHelper = materialHelper;
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
            SceneManager.LoadScene("FirstLevel");
        }

        public void DetectionReleased()
            => _spriteRenderer.material = _materialHelper.DefaultMaterial;
    }
}