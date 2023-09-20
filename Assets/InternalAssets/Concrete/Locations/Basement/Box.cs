using System;
using SouthBasement.Helpers;
using SouthBasement.Interactions;
using SouthBasement.Items;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Box : MonoBehaviour, IInteractive
    {
        public event Action<IInteractive> OnDetected;
        public event Action<IInteractive> OnInteracted;
        public event Action<IInteractive> OnDetectionReleased;
        
        private ItemsContainer _itemsContainer;
        private Animator _animator;
        private MaterialHelper _materialHelper;
        private SpriteRenderer _spriteRenderer;

        private bool _opened;

        [Inject]
        private void Construct(ItemsContainer itemsContainer, MaterialHelper materialHelper)
        {
            _itemsContainer = itemsContainer;
            _materialHelper = materialHelper;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Detect()
        {
            if (_opened) return;
            
            _spriteRenderer.material = _materialHelper.OutlineMaterial;
            OnDetected?.Invoke(this);
        }

        public void Interact()
        {
            if (_opened) return;
                var item =_itemsContainer.SpawnItem(_itemsContainer.GetRandomInRarity(ChanceSystem.GetRandomRarity()),
                transform.position, transform);
            _animator.Play("BoxOpen");
            
            item.PlayMove(Vector2.down, 7f);
            
            _opened = true;
            _spriteRenderer.material = _materialHelper.DefaultMaterial;
            OnInteracted?.Invoke(this);
        }

        public void DetectionReleased()
        {
            if (_opened) return;
            
            _spriteRenderer.material = _materialHelper.DefaultMaterial;
            OnDetectionReleased?.Invoke(this);
        }
    }
}