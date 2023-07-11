using System;
using SouthBasement.Helpers;
using SouthBasement.Interactions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SouthBasement.Tests
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TestExit : MonoBehaviour, IInteractive
    {
        private MaterialHelper _materialHelper;
        private SpriteRenderer _spriteRenderer;
        public event Action<IInteractive> OnDetected;
        public event Action<IInteractive> OnInteracted;
        public event Action<IInteractive> OnDetectionReleased;

        [Inject]
        private void Construct(MaterialHelper materialHelper)
        {
            _materialHelper = materialHelper;
        }
        
        public void Detect()
        {
            _spriteRenderer.material = _materialHelper.OutlineMaterial;
        }

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Interact()
        {
            FindObjectOfType<BlackoutTransition>().PlayBlackout();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void DetectionReleased()
        {
            _spriteRenderer.material = _materialHelper.DefaultMaterial;   
        }
    }
}