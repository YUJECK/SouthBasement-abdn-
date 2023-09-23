using UnityEngine;

namespace SouthBasement.MonologueSystem
{
    [CreateAssetMenu(menuName = AssetMenuHelper.MonologueSystem + nameof(Monologue))]
    public sealed class Monologue : ScriptableObject
    {
        [TextArea(0, 30)] public string[] phrases;
    }
}