using System;
using TheRat.InputServices;
using TheRat.Items;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TheRat.Characters
{
    public sealed class CharacterChoosingService : MonoBehaviour
    {
        private ItemsContainer _itemsContainer;
        private event Action<Character> OnCharacterChosen;

        [Inject]
        private void Construct(IInputService inputService, ItemsContainer itemsContainer)
        {
            inputService.OnAttack += OnLeftMB;
            _itemsContainer = itemsContainer;
        }

        private void OnLeftMB()
        {
            var hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            foreach (var hit in hits)
            {
                if(hit.collider.gameObject.TryGetComponent<Character>(out var character))
                    Choose(character);
            }
        }

        public void Choose(Character character)
        {
            _itemsContainer.Add(character.CharacterConfig.CharacterItems);   
            OnCharacterChosen?.Invoke(character);

            SceneManager.LoadScene(2);
        }
    }
}