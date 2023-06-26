using Subtegral.DialogueSystem.Runtime;
using UnityEngine;
using UnityEngine.Events;

namespace SouthBasement.DialogueHUD
{
    public sealed class ChoiceButtonController : MonoBehaviour
    {
        private ChoiceButton[] _choiceButtons;

        public void Awake()
            => _choiceButtons = GetComponentsInChildren<ChoiceButton>();

        public void Build(DialogueChoice[] choices, UnityAction<DialogueChoice> onClick)
        {
            ClearButtons();
            
            for (var i = 0; i < choices.Length; i++)
            {
                var choice = choices[i];

                _choiceButtons[i].gameObject.SetActive(true);
                _choiceButtons[i].SetChoice(choice, onClick);
            }
        }

        public void ClearButtons()
        {
            foreach (var button in _choiceButtons)
            {
                button.Clear();
                button.gameObject.SetActive(false);
            }
        }
    }
}