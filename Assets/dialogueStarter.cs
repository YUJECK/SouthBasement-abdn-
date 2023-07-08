using System;
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
        
        public event Action<IInteractive> OnDetected;
        public event Action<IInteractive> OnInteracted;
        public event Action<IInteractive> OnDetectionReleased;

        [Inject]
        private void Construct(IDialogueService dialogueService)
        {
            _dialogueService = dialogueService;
        }

        public void Detect()
            => OnDetected?.Invoke(this);

        public void Interact()
        {
            _dialogueService.StartDialogue(DialogueContainer, null);
            
            OnInteracted?.Invoke(this);
        }

        public void DetectionReleased()
            => OnDetectionReleased?.Invoke(this);
    }
}
