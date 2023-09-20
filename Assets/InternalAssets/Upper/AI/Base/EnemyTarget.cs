using UnityEngine;

namespace SouthBasement.AI
{
    public sealed class EnemyTarget : MonoBehaviour
    {
        [field: SerializeField] public int Priority { get; private set; } 
    }
}