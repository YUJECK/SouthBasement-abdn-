using UnityEngine;

namespace TheRat.AI
{
    public sealed class EnemyTarget : MonoBehaviour
    {
        [field: SerializeField] public int Priority { get; private set; } 
    }
}