using UnityEngine;

namespace SouthBasement.Economy
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Infrastructure + "CheeseServiceConfig")]
    public sealed class CheeseServiceConfig : ScriptableObject
    {
        [field: SerializeField] public CheeseObject CheesePrefab { get; private set; }

        [field: SerializeField] public int StartCheeseAmount { get; private set; }
    }
}
