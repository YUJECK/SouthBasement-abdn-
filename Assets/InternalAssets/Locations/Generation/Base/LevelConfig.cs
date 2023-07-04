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
        
        public Room GetRandomRoomFor(RoomType roomType, Direction toDirection)
        {
            return roomType switch
            {
                RoomType.StartRoom => RoomGetRandomIn(StartRooms, toDirection),
                RoomType.FightRoom =>  RoomGetRandomIn(FightRooms, toDirection),
                RoomType.NPCRoom => RoomGetRandomIn(NPCRooms, toDirection),
                RoomType.ExitRoom => RoomGetRandomIn(ExitRooms, toDirection),
                RoomType.TraderRoom =>  RoomGetRandomIn(TraderRooms, toDirection),
            };
        }
        public Room GetRandomRoom(RoomType roomType)
        {
            return roomType switch
            {
                RoomType.StartRoom => RoomGetRandom(StartRooms),
                RoomType.FightRoom =>  RoomGetRandom(FightRooms),
                RoomType.NPCRoom => RoomGetRandom(NPCRooms),
                RoomType.ExitRoom => RoomGetRandom(ExitRooms),
                RoomType.TraderRoom =>  RoomGetRandom(TraderRooms),
            };
        }

        private Room RoomGetRandom(Room[] rooms) => rooms[Random.Range(0, rooms.Length)];
        private Room RoomGetRandomIn(Room[] rooms, Direction toDirection)
        {
            var randomRoom = rooms[Random.Range(0, rooms.Length)];
            
            while (!randomRoom.PassageHandler.Contains(DirectionHelper.GetOpposite(toDirection)))
                randomRoom = rooms[Random.Range(0, rooms.Length)];
            
            return randomRoom;
        }
    }
}