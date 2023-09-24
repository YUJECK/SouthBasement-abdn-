using SouthBasement.Extensions;
using TMPro;
using UnityEngine;  
using Zenject;

namespace SouthBasement.MonologueSystem
{
    public sealed class MonologuePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        private MonologuePanelConfig _monologuePanelConfig;
        private TextTypingCoroutine _textTypingCoroutine;

        [Inject]
        private void Construct(MonologuePanelConfig monologuePanelConfig)
            => _monologuePanelConfig = monologuePanelConfig;


        public void UpdateText(string text)
        {
            if(_textTypingCoroutine != null)
                this.text.StopTypingText(_textTypingCoroutine);
            
            _textTypingCoroutine = this.text.TypeText(text, _monologuePanelConfig.textSpeed);
        }
    }
}