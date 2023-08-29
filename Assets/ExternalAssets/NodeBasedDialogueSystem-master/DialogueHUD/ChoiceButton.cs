using Subtegral.DialogueSystem.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace SouthBasement.DialogueHUD
{
    [RequireComponent(typeof(Button))]
    public sealed class ChoiceButton : MonoBehaviour
    {
        private TMP_Text _text;
        private Button _button;

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
            _button = GetComponent<Button>();
        }

        public void SetChoice(DialogueChoice choice, UnityAction<DialogueChoice> onClick)
        {
            _text.text = choice.Text;
            
            _text.GetComponent<LocalizeStringEvent>().SetTable(choice.TableName);  
            _text.GetComponent<LocalizeStringEvent>().SetEntry(choice.Text);  
            
            _text.GetComponent<LocalizeStringEvent>().RefreshString();   
            _button.onClick.AddListener(() => onClick?.Invoke(choice));
        }

        public void Clear()
        {
            _text.text = "";
            _button.onClick.RemoveAllListeners();
        }
    }
}