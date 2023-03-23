using System.Collections.Generic;
using UnityEngine;

namespace TheRat.LocationGeneration
{
    [CreateAssetMenu()]
    public sealed class RoomsStorager : ScriptableObject
    {
        [field: SerializeField] public List<Room> EnemyRooms { get; private set; }
        [field: SerializeField] public List<Room> StartRooms { get; private set; }
    }
}