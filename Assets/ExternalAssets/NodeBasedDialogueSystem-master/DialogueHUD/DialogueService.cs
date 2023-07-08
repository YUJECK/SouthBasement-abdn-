using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
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
        [SerializeField] private TMP_Text dialogueName;
        [SerializeField] private ChoiceButtonController _buttonController;

        [SerializeField] private Transform onEnable;
        [SerializeField] private Transform onDisable;

        public bool CurrentlyTalk { get; private set; }
        private readonly DialogueParser _parser = new();

        [ReadOnly, SerializeField] private DialogueContainer _currentDialogue;

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

            dialogueName.text = dialogueContainer.Name;
            _currentDialogue = dialogueContainer;
            
            OpenWindow();
            BuildPhrase(dialogueContainer.NodeLinks.First().TargetNodeGUID);
            
            CurrentlyTalk = true;
        }

        private void OpenWindow() => _dialoguePanel.DOMove(onEnable.position, 0.7f);

        private async void BuildPhrase(string target)
        {
            var newText = _parser.Get(_currentDialogue, target);

            _buttonController.ClearButtons();

            await PrintText(newText.Text);

            if (newText.DialogueChoices.Length == 0)
            {
                BuildCloseButton();
                return;
            }
            BuildButtons(newText.DialogueChoices);
        }

        private async UniTask PrintText(string newText)
        {
            dialogueText.text = "";
            foreach (var letter in newText)
            {
                dialogueText.text += letter;
                await UniTask.Delay(TimeSpan.FromSeconds(0.05f));
            }
        }

        private void BuildButtons(DialogueChoice[] choices) => _buttonController.Build(choices, (choice) => BuildPhrase(choice.Target));

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