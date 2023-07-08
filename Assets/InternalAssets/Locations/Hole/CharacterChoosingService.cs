using System.Collections.Generic;
using SouthBasement.Characters.Components;
using SouthBasement.Helpers;
using SouthBasement.InputServices;
using SouthBasement.Items;
using UnityEngine;
using Zenject;
using System;

namespace SouthBasement.Characters
{
    public sealed class CharacterChoosingService : MonoBehaviour
    {
        private ItemsContainer _itemsContainer;
        private IInputService _inputService;
        private MaterialHelper _materialHelper;
        
        public event Action<Character> OnCharacterChosen;

        private GameObject _current;
        private bool _chosen;
        
        [Inject]
        private void Construct(IInputService inputService, ItemsContainer itemsContainer, MaterialHelper materialHelper)
        {
            _inputService = inputService;
            _itemsContainer = itemsContainer;
            _materialHelper = materialHelper;
        }

        private void Awake()
            => _inputService.OnAttack += OnLeftMB;

        private void OnDestroy()
         => _inputService.OnAttack -= OnLeftMB;

        private void OnLeftMB()
        {
            if(_chosen) return;
            
            var hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            foreach (var hit in hits)
            {
                if(hit.collider.gameObject.TryGetComponent<Character>(out var character))
                    Choose(character);
            }
        }

        private void Update()
        {
            if (_chosen) return;
            
            var hits = new List<RaycastHit2D>(Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero));
            var gos = new List<GameObject>();
            
            foreach (var hit in hits)
            {
                gos.Add(hit.transform.gameObject);
            }
            
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent<Character>(out var character))
                {
                    character.GetComponentInChildren<SpriteRenderer>().material = _materialHelper.OutlineMaterial;
                    _current = hit.transform.gameObject;
                }
            }
            if(_current != null && !gos.Contains(_current))
                _current.transform.GetComponentInChildren<SpriteRenderer>().material = _materialHelper.DefaultMaterial;
        }

        private void Choose(Character character)
        {
            _itemsContainer.Add(character.CharacterConfig.CharacterItems);   
            OnCharacterChosen?.Invoke(character);
            
            character.Components.Get<ICharacterMovable>().CanMove = true;
            _current.GetComponentInChildren<SpriteRenderer>().material = _materialHelper.DefaultMaterial;
            _chosen = true;
        }
    }
}