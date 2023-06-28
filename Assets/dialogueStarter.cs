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

        [Inject]
        private void Construct(IDialogueService dialogueService, Character character)
        {
            _dialogueService = dialogueService;
            _character = character;
        }

        public void Detect()
        {
            
        }

        public void Interact()
        {
            _dialogueService.StartDialogue(DialogueContainer, () => _character.Components.Get<ICharacterMovable>().CanMove = true);
            _character.Components.Get<ICharacterMovable>().CanMove = false;
        }

        public void DetectionReleased()
        {
            
        }
    }
}
