using UnityEngine;

namespace SouthBasement.Generation
{
    [CreateAssetMenu]
    public class LevelConfig : ScriptableObject
    {
        public int TotalRoomsCount => FightRoomsCount + TraderRoomsCount + NPCRoomsCount;
        
        public int FightRoomsCount;
        public int TraderRoomsCount;
        public int NPCRoomsCount;
        
        public Room[] StartRooms;
        public Room[] FightRooms;
        public Room[] TraderRooms;
        public Room[] NPCRooms;
        public Room[] ExitRooms;
        
        public Room GetRandomRoom(RoomType roomType)
        {
            return roomType switch
            {
                RoomType.StartRoom => StartRooms[Random.Range(0, StartRooms.Length)],
                RoomType.FightRoom => FightRooms[Random.Range(0, FightRooms.Length)],
                RoomType.NPCRoom => NPCRooms[Random.Range(0, NPCRooms.Length)],
                RoomType.ExitRoom => ExitRooms[Random.Range(0, ExitRooms.Length)],
                RoomType.TraderRoom => TraderRooms[Random.Range(0, TraderRooms.Length)],
            };
        }
    }
}