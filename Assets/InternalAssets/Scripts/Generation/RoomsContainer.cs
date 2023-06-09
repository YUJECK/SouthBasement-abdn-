using UnityEngine;

namespace TheRat.Generation
{
    [CreateAssetMenu]
    public class RoomsContainer : ScriptableObject
    {
        public int RoomsCount;
        
        public Room[] StartRooms;
        public Room[] FightRooms;
        public Room[] NPCRooms;
        public Room[] ExitRooms;
        
        public Room GetRandomRoom(RoomType roomType)
        {
            switch (roomType)
            {
                case RoomType.StartRoom:
                    return StartRooms[Random.Range(0, StartRooms.Length)];
                case RoomType.FightRoom:
                    return FightRooms[Random.Range(0, FightRooms.Length)];
                case RoomType.NPCRoom:
                    return NPCRooms[Random.Range(0, NPCRooms.Length)];
                case RoomType.ExitRoom:
                    return ExitRooms[Random.Range(0, ExitRooms.Length)];
            }

            return null;
        }
    }
}