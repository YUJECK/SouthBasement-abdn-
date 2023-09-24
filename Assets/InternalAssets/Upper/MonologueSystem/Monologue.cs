using UnityEngine;

namespace SouthBasement.MonologueSystem
{
    [CreateAssetMenu(menuName = AssetMenuHelper.MonologueSystem + nameof(Monologue))]
    public sealed class Monologue : ScriptableObject
    {
        [field: SerializeField, TextArea(0, 30)] public string[] Phrases { get; private set; }

        public static int MinimumStage => 0;
        public int MaximumStage => Phrases.Length - 1;

        public bool IsCompleted(int stage) => stage > MaximumStage;

        public string GetPhrase(int stage)
        {
            if (stage < MinimumStage)
                stage = MinimumStage;
            
            if (stage > MaximumStage)
                stage = MaximumStage;
            
            return Phrases[stage];
        }
    }
}