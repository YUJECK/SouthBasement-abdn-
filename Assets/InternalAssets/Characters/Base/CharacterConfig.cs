using TheRat.InventorySystem;
using UnityEngine;

namespace TheRat
{
    [CreateAssetMenu()]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public string CharacterName { get; private set; }
        [field: SerializeField] public string CharacterDescription { get; private set; }
        [field: SerializeField] public Sprite CharacterIcon { get; private set; }
        [field: SerializeField] public Item[] CharacterItems { get; private set; }
        [field: SerializeField] public CharacterStatsConfig DefaultStats { get; private set; }
    }
}