using UnityEngine;

namespace TheRat.LocationGeneration
{
    public class Room : MonoBehaviour
    {
        [field: SerializeField] public int Chance { get; private set; }
        [field: SerializeField] public RoomFactory[] Factories { get; private set; }
    }
}