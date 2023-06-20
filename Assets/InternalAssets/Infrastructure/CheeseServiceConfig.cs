using UnityEngine;

namespace SouthBasement.Economy
{
    [CreateAssetMenu(menuName = "Create CheeseServiceConfig", fileName = "CheeseServiceConfig", order = 0)]
    public sealed class CheeseServiceConfig : ScriptableObject
    {
        [field: SerializeField] public CheeseObject CheesePrefab { get; private set; }

        [field: SerializeField] public int StartCheeseAmount { get; private set; }
    }
}
