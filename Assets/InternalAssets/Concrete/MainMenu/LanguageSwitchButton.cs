using SouthBasement.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace SouthBasement
{
    [RequireComponent(typeof(Button))]
    public sealed class LanguageSwitchButton : MonoBehaviour
    {
        private Button _button;
        public Image _image;
        public Sprite rusSprite;
        public Sprite engSprite;

        private void Start()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(OnClick);
            
            if (LocalizationManager.CurrentLanguage == Languages.English)
            {
                _image.sprite = engSprite;
            }
            else
            {
                _image.sprite = rusSprite;
            } 
        }

        private void OnClick()
        {
            if (LocalizationManager.CurrentLanguage == Languages.English)
            {
                LocalizationManager.SetLanguage(Languages.Russian);
                _image.sprite = rusSprite;
            }
            else
            {
                LocalizationManager.SetLanguage(Languages.English);
                _image.sprite = engSprite;
            }
        }
    }
}