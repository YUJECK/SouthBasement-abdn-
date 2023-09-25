using SouthBasement.Extensions;
using TMPro;
using UnityEngine;  
using Zenject;

namespace SouthBasement.MonologueSystem
{
    public sealed class MonologuePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private AudioSource typeSound;
        
        private MonologuePanelConfig _monologuePanelConfig;
        private TextTypingCoroutine _textTypingCoroutine;

        [Inject]
        private void Construct(MonologuePanelConfig monologuePanelConfig)
            => _monologuePanelConfig = monologuePanelConfig;


        public void UpdateText(string textToPrint)
        {
            if(_textTypingCoroutine is { TypingCoroutine: not null })
                text.StopTypingText(_textTypingCoroutine);
            
            _textTypingCoroutine = text.TypeText(textToPrint, _monologuePanelConfig.TextTypingSpeed, 
                () => typeSound.Play());
        }
    }
}