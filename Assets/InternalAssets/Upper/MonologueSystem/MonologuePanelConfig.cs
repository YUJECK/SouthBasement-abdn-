using UnityEngine;

namespace SouthBasement.MonologueSystem
{
    [CreateAssetMenu(menuName = AssetMenuHelper.MonologueSystem + nameof(MonologuePanelConfig))]
    public sealed class MonologuePanelConfig : ScriptableObject
    {
        [SerializeField] private int textTypingSpeed = 5;
        [SerializeField] private int panelMoveSpeed = 7;

        public float TextTypingSpeed => (float)textTypingSpeed / 100;
        public float PanelMoveSpeed => (float)panelMoveSpeed / 100;
    }
}