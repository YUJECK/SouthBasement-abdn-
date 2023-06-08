using UnityEngine;

namespace TheRat.Generation
{
    [CreateAssetMenu]
    public class RoomsContainer : ScriptableObject
    {
        public Room[] StartRooms;
        public Room[] EnemyRooms;
        public Room[] NPCRooms;

        public Room GetRandom()
        {
            int randomRoom = Random.Range(0, StartRooms.Length);
            return StartRooms[randomRoom];
        }
    }
}