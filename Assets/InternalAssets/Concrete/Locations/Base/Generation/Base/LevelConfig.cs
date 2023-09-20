using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement.Generation
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Infrastructure + "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public int TotalRoomsCount => FightRoomsCount + TraderRoomsCount + NPCRoomsCount;
        
        [field: SerializeField] public int FightRoomsCount { get; private set; }
        [field: SerializeField] public int TraderRoomsCount { get; private set; }
        [field: SerializeField] public int NPCRoomsCount { get; private set; }
        
        [field: SerializeField] public List<Room> StartRooms { get; private set; }
        [field: SerializeField] public List<Room> FightRooms { get; private set; }
        [field: SerializeField] public List<Room> TraderRooms { get; private set; }
        [field: SerializeField] public List<Room> NPCRooms { get; private set; }
        [field: SerializeField] public List<Room> ExitRooms { get; private set; }
        
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

        public void Remove(Room room, RoomType roomType)
        {
            switch(roomType)
            { 
                case RoomType.StartRoom:
                    StartRooms.Remove(room);
                    break;
                    
                case RoomType.FightRoom: 
                    FightRooms.Remove(room);
                    break;
                
                case RoomType.NPCRoom:
                    NPCRooms.Remove(room);
                    break;
                    
                case RoomType.ExitRoom:
                    ExitRooms.Remove(room);
                    break;
                
                case RoomType.TraderRoom:
                    TraderRooms.Remove(room);
                    break;
            };
        }
        
        private Room RoomGetRandom(List<Room> rooms) => rooms[Random.Range(0, rooms.Count)];
        private Room RoomGetRandomIn(List<Room> rooms, Direction toDirection)
        {
            var randomRoom = rooms[Random.Range(0, rooms.Count)];

            int iterations = 0;
            
            while (!randomRoom.PassageHandler.Contains(DirectionHelper.GetOpposite(toDirection)))
            {
                randomRoom = rooms[Random.Range(0, rooms.Count)];
                
                iterations++;
                
                if (iterations > 500)
                {
                    Debug.LogWarning($"Cannot find room for {toDirection} direction");
                    break;
                }
            }
            
            return randomRoom;
        }
    }
}