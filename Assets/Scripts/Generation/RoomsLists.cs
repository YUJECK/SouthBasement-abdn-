using System.Collections.Generic;
using UnityEngine;

namespace Generation
{
    public enum RoomSpawnerState
    {
        Open,
        Close,
        StaticOpen,
        StaticClose
    }
    [System.Serializable] public class RoomObject
    {
        public GameObject room;
        public int chance;
    }
    public enum Rooms
    {
        Default,
        NPC,
        Trader,
        Box,
        MustSpawn,
        Exit
    }
    
    public class RoomsLists : MonoBehaviour
    {
        [SerializeField] private List<RoomObject> simpleRooms = new List<RoomObject>();
        [SerializeField] private List<RoomObject> npcRooms = new List<RoomObject>();
        [SerializeField] private List<RoomObject> traderRooms = new List<RoomObject>();
        [SerializeField] private List<RoomObject> boxRooms = new List<RoomObject>();
        [SerializeField] private List<RoomObject> mustSpawnRooms = new List<RoomObject>();
        [SerializeField] private List<RoomObject> exitRooms = new List<RoomObject>();

        //Геттеры

        //На листы
        public List<RoomObject> GetRoomsList(Rooms roomType)
        {
            switch (roomType)
            {
                case Rooms.Default:
                    return simpleRooms;
                case Rooms.NPC:
                    return npcRooms;
                case Rooms.Trader:
                    return traderRooms;
                case Rooms.Box:
                    return boxRooms;
                case Rooms.MustSpawn:
                    return mustSpawnRooms;
                case Rooms.Exit:
                    return exitRooms;
            }
            return new List<RoomObject>();
        }

        //На рандомную комнату
        public GameObject GetRandomRoomInChance(Rooms roomType, int chance, bool remove)
        {
            List<RoomObject> roomsInChance = new List<RoomObject>();
            ref List<RoomObject> roomsList = ref simpleRooms;
            switch (roomType)
            {
                case Rooms.Default:
                    roomsList = ref simpleRooms;
                    break;
                case Rooms.NPC:
                    roomsList = ref npcRooms;
                    break;
                case Rooms.Trader:
                    roomsList = ref traderRooms;
                    break;
                case Rooms.Box:
                    roomsList = ref boxRooms;
                    break;
                case Rooms.MustSpawn:
                    roomsList = ref mustSpawnRooms;
                    break;
                case Rooms.Exit:
                    roomsList = ref exitRooms;
                    break;
            }

            foreach (RoomObject roomToCheck in roomsList)
                if (roomToCheck.chance >= chance) roomsInChance.Add(roomToCheck);
            int roomIndex = Random.Range(0, roomsInChance.Count);
            if(remove) roomsList.Remove(roomsInChance[roomIndex]);

            return roomsInChance[roomIndex].room;
        }
    }
}
