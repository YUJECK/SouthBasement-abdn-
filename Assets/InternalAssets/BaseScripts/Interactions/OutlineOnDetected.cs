using SouthBasement.Helpers;
using UnityEngine;
using Zenject;

namespace SouthBasement.Interactions
{
    public sealed class OutlineOnDetected : MonoBehaviour, IInteractive
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private MaterialHelper _materialHelper;
        
        [Inject]
        private void Construct(MaterialHelper materialHelper) 
            => _materialHelper = materialHelper;

        public void Detect() => _spriteRenderer.material = _materialHelper.OutlineMaterial;
        public void Interact() { }
        public void DetectionReleased() => _spriteRenderer.material = _materialHelper.DefaultMaterial;
    }
}