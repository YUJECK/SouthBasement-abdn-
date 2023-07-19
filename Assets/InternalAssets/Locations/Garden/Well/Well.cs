using System;
using SouthBasement.Helpers;
using SouthBasement.Interactions;
using UnityEngine;
using Zenject;

namespace SouthBasement.Garden.Interactions
{
    public sealed class Well : MonoBehaviour, IInteractive
    {
        public event Action<IInteractive> OnDetected;
        public event Action<IInteractive> OnInteracted;
        public event Action<IInteractive> OnDetectionReleased;
        
        private MaterialHelper _materialHelper;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private bool _pulled = true;
        private static readonly int Pulled = Animator.StringToHash( "Pulled");

        [Inject]
        private void Construct(MaterialHelper materialHelper) 
            => _materialHelper = materialHelper;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        public void Detect() => _spriteRenderer.material = _materialHelper.OutlineMaterial;
        public void DetectionReleased() => _spriteRenderer.material = _materialHelper.DefaultMaterial;

        public void Interact()
        {
            _pulled = !_pulled;
            _animator.SetBool(Pulled, _pulled);
        }
    }
}