using TMPro;
using UnityEngine;

namespace SouthBasement.Localization
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizatedText : MonoBehaviour
    {
        public LocalizatedString text;
        
        private TMP_Text _text;
        
        private void Awake()
        {
            _text.text = text.GetLocalized();
            LocalizationManager.OnLocalized += OnLocalized;
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