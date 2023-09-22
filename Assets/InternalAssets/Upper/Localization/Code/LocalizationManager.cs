using System;

namespace SouthBasement.Localization
{
    public static class LocalizationManager
    {
        public static Languages CurrentLanguage { get; private set; }
        public static event Action<Languages> OnLocalized;

        public static void SetLanguage(Languages language)
        {
            CurrentLanguage = language;
            OnLocalized?.Invoke(language);
        }
    }
}