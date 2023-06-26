using SouthBasement.Dialogues;
using SouthBasement.Interactions;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using Zenject;

namespace TheRat
{
    public class dialogueStarter : MonoBehaviour, IInteractive
    {
        [SerializeField] private DialogueContainer DialogueContainer;
        
        private IDialogueService _dialogueService;

        [Inject]
        private void Construct(IDialogueService dialogueService)
        {
            _dialogueService = dialogueService;
        }

        public void Detect()
        {
            
        }

        public void Interact()
        {
            Debug.Log("sdkl;f");
            _dialogueService.StartDialogue(DialogueContainer);
        }

        public void DetectionReleased()
        {
            
        }
    }
}
