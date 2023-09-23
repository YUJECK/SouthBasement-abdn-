using UnityEngine;

namespace SouthBasement.Localization
{
    [System.Serializable]
    public sealed class LocalizedString
    {
        [TextArea(1, 20)] public string rus = "";
        [TextArea(1, 20)] public string eng = "";

        public string GetLocalized()
        {
            if (LocalizationManager.CurrentLanguage == Languages.Russian)
                return rus;
            if (LocalizationManager.CurrentLanguage == Languages.English)
                return eng;

            return "";
        }
    }
}