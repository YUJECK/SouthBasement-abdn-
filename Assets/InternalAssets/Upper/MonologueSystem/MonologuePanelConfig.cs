using UnityEngine;

namespace SouthBasement.MonologueSystem
{
    [CreateAssetMenu(menuName = AssetMenuHelper.MonologueSystem + nameof(MonologuePanelConfig))]
    public sealed class MonologuePanelConfig : ScriptableObject
    {
        public float textSpeed;
        public float panelSpeed;
    }
}