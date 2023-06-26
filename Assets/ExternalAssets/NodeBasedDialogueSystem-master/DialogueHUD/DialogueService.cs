using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using SouthBasement.DialogueHUD;
using Subtegral.DialogueSystem.DataContainers;
using Subtegral.DialogueSystem.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SouthBasement.Dialogues
{
    public sealed class DialogueService : MonoBehaviour, IDialogueService
    {
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private ChoiceButtonController _buttonController;

        public bool CurrentlyTalk { get; private set; }
        private readonly DialogueParser _parser = new();

        [SerializeField] private DialogueContainer _currentDialogue;

        private void Start()
        {
            StartDialogue(_currentDialogue);
        }

        public void StartDialogue(DialogueContainer dialogueContainer)
        {
            PrintText(dialogueContainer.NodeLinks.First().TargetNodeGUID);
        }
        
        private async void PrintText(string target)
        {
            dialogueText.text = "";
            var newText = _parser.GetNext(_currentDialogue, target);

            _buttonController.ClearButtons();
            
            foreach (var letter in newText.Text)
            {
                dialogueText.text += letter;
                await UniTask.Delay(50);
            }
            
            BuildButtons(newText.DialogueChoices);
        }

        private void BuildButtons(DialogueChoice[] choices)
        {
            _buttonController.Build(choices, (choice) => PrintText(choice.Target));
        }

        public void StopDialogue(DialogueContainer dialogueContainer)
        {
            
        }
    }
} 