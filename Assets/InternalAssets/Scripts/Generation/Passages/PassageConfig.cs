using UnityEngine;

namespace TheRat.LocationGeneration
{
    [System.Serializable]
    public class PassageConfig
    {
        [Tooltip("Passage look direction")]
        [field: SerializeField] public Directions Direction { get; private set; }
        
        [Tooltip("Passage offset from center")]
        [field: SerializeField] public Vector2 SpawnOffset { get; private set; }
    }
}