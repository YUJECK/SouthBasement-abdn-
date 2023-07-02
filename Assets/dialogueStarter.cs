using System;
using SouthBasement.Characters;
using SouthBasement.Characters.Components;
using SouthBasement.Dialogues;
using SouthBasement.Interactions;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public class dialogueStarter : MonoBehaviour, IInteractive
    {
        [SerializeField] private DialogueContainer DialogueContainer;
        
        private IDialogueService _dialogueService;
        private Character _character;
        
        public event Action<IInteractive> OnDetected;
        public event Action<IInteractive> OnInteracted;
        public event Action<IInteractive> OnDetectionReleased;

        [Inject]
        private void Construct(IDialogueService dialogueService, Character character)
        {
            _dialogueService = dialogueService;
            _character = character;
        }

        public void Detect()
        {
            OnDetected?.Invoke(this);
        }

        public void Interact()
        {
            _dialogueService.StartDialogue(DialogueContainer, () => _character.Components.Get<ICharacterMovable>().CanMove = true);
            _character.Components.Get<ICharacterMovable>().CanMove = false;
            
            OnInteracted?.Invoke(this);
        }

        public void DetectionReleased()
        {
            OnDetectionReleased?.Invoke(this);
        }
    }
}
