using TMPro;
using UnityEngine;

namespace SouthBasement.Localization
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizatedText : MonoBehaviour
    {
        public LocalizedString text;
        
        private TMP_Text _text;
        
        private void Awake()
        {
            if (_text == null)
            {
                _text = GetComponent<TMP_Text>();
            }
            
            LocalizationManager.OnLocalized += OnLocalized;
            _text.text = text.GetLocalized();
        }

        private void OnValidate()
        {
            if (_text == null)
                _text = GetComponent<TMP_Text>();
            
            _text.text = text.rus;
        }

        private void OnLocalized(Languages language)
            => _text.text = text.GetLocalized();
    }
}