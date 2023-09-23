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

        [Inject]
        private void Construct(MonologuePanelConfig monologuePanelConfig)
            => _monologuePanelConfig = monologuePanelConfig;


        public void UpdateText(string text)
        {
            this.text.Show(text, _monologuePanelConfig.textSpeed);
        }
    }
}