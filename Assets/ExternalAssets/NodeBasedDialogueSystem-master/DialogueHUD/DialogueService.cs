using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SouthBasement.DialogueHUD;
using Subtegral.DialogueSystem.DataContainers;
using Subtegral.DialogueSystem.Runtime;
using TMPro;
using UnityEngine;

namespace SouthBasement.Dialogues
{
    public sealed class DialogueService : MonoBehaviour, IDialogueService
    {
        [SerializeField] private Transform _dialoguePanel;
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private ChoiceButtonController _buttonController;

        [SerializeField] private Transform onEnable;
        [SerializeField] private Transform onDisable;

        public bool CurrentlyTalk { get; private set; }
        private readonly DialogueParser _parser = new();

        [SerializeField] private DialogueContainer _currentDialogue;

        private Action onStopped;
        
        private void Awake()
        {
            if(!CurrentlyTalk)
                StopDialogue();
        }

        public void StartDialogue(DialogueContainer dialogueContainer, Action onStopped)
        {
            if(dialogueContainer == null || CurrentlyTalk)
                return;

            this.onStopped = onStopped;
            
            _dialoguePanel.DOMove(onEnable.position, 0.7f);
            PrintText(dialogueContainer.NodeLinks.First().TargetNodeGUID);
            
            CurrentlyTalk = true;
        }
        
        private async void PrintText(string target)
        {
            dialogueText.text = "";
            var newText = _parser.Get(_currentDialogue, target);

            _buttonController.ClearButtons();
            
            foreach (var letter in newText.Text)
            {
                dialogueText.text += letter;
                await UniTask.Delay(50);
            }

            if (newText.DialogueChoices.Length == 0)
            {
                BuildCloseButton();
                return;
            }
            BuildButtons(newText.DialogueChoices);
        }

        private void BuildButtons(DialogueChoice[] choices)
        {
            _buttonController.Build(choices, (choice) => PrintText(choice.Target));
        }

        private void BuildCloseButton()
        {
            DialogueChoice[] closeButton = new DialogueChoice[] { new DialogueChoice("Уйти", "") };
            _buttonController.Build(closeButton, (choice) => StopDialogue());
        }

        public void StopDialogue()
        {
            _dialoguePanel.DOMove(onDisable.position, 0.7f);
            onStopped?.Invoke();
            CurrentlyTalk = false;
        }
    }
} 