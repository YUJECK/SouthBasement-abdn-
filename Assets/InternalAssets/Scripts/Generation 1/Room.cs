using UnityEngine;

namespace TheRat.LocationGeneration
{
    public class Room : MonoBehaviour
    {
        [field: SerializeField] public int chance { get; private set; }
        [field: SerializeField] public RoomFactory[] factories { get; private set; }
    }
}