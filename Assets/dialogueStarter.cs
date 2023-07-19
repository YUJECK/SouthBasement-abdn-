using System;
using SouthBasement.Dialogues;
using SouthBasement.Helpers;
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
        private MaterialHelper _materialHelper;
        private SpriteRenderer _spriteRenderer;

        public event Action<IInteractive> OnDetected;
        public event Action<IInteractive> OnInteracted;
        public event Action<IInteractive> OnDetectionReleased;

        [Inject]
        private void Construct(IDialogueService dialogueService, MaterialHelper materialHelper)
        {
            _dialogueService = dialogueService;
            _materialHelper = materialHelper;
        }

        private void Awake()
            => _spriteRenderer = GetComponent<SpriteRenderer>();

        public void Detect()
        {
            OnDetected?.Invoke(this);
            _spriteRenderer.material = _materialHelper.OutlineMaterial;
        }

        public void Interact()
        {
            _dialogueService.StartDialogue(DialogueContainer, null);
            
            OnInteracted?.Invoke(this);
        }

        public void DetectionReleased()
        {
            OnDetectionReleased?.Invoke(this);
            _spriteRenderer.material = _materialHelper.DefaultMaterial;
        }
    }
}
