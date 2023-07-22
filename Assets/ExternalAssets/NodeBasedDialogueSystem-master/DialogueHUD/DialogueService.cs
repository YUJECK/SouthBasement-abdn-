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
using UnityEngine.Localization;

namespace SouthBasement.Dialogues
{
    public sealed class DialogueService : MonoBehaviour, IDialogueService
    {
        [SerializeField] private Transform _dialoguePanel;
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private TMP_Text dialogueName;
        [SerializeField] private AudioSource textSound;
        [SerializeField] private ChoiceButtonController _buttonController;

        [SerializeField] private DialogueChoice leaveButton;
        
        [SerializeField] private Transform onEnable;
        [SerializeField] private Transform onDisable;

        [ReadOnly, SerializeField] private DialogueContainer currentDialogue;
        
        private readonly DialogueParser _parser = new();
        public bool CurrentlyTalk { get; private set; }


        private Action _onStopped;
        
        private void Awake()
        {
            if(!CurrentlyTalk)
                StopDialogue();
        }

        public void StartDialogue(DialogueContainer dialogueContainer, Action onStopped)
        {
            if(dialogueContainer == null || CurrentlyTalk)
                return;

            this._onStopped = onStopped;

            dialogueName.text = new LocalizedString(dialogueContainer.TableName, dialogueContainer.Name).GetLocalizedString();
            currentDialogue = dialogueContainer;
            
            OpenWindow();
            BuildPhrase(dialogueContainer.NodeLinks.First().TargetNodeGUID);
            
            CurrentlyTalk = true;
        }

        private void OpenWindow()
        {
            _dialoguePanel.gameObject.SetActive(true);
            _dialoguePanel.DOMove(onEnable.position, 0.7f);
        }

        private async void BuildPhrase(string target)
        {
            var newText = _parser.Get(currentDialogue, target);

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
                textSound.Play();
                await UniTask.Delay(TimeSpan.FromSeconds(0.05f));
            }
        }

        private void BuildButtons(DialogueChoice[] choices) => _buttonController.Build(choices, (choice) => BuildPhrase(choice.Target));

        private void BuildCloseButton()
        {
            DialogueChoice[] closeButton = { leaveButton };
            _buttonController.Build(closeButton, (choice) => StopDialogue());
        }

        public void StopDialogue()
        {
            _dialoguePanel.DOMove(onDisable.position, 0.7f).OnComplete(() => _dialoguePanel.gameObject.SetActive(false));
            _onStopped?.Invoke();
            CurrentlyTalk = false;
        }
    }
} 