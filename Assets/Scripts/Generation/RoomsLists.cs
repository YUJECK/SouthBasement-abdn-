using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generation
{

    public class RoomsLists : MonoBehaviour
    {
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
        
        [SerializeField] private List<RoomObject> simpleRooms = new List<RoomObject>();
        [SerializeField] private List<RoomObject> npcRooms = new List<RoomObject>();
        [SerializeField] private List<RoomObject> traderRooms = new List<RoomObject>();
        [SerializeField] private List<RoomObject> boxRooms = new List<RoomObject>();
        [SerializeField] private List<RoomObject> exitRooms = new List<RoomObject>();

        //Геттеры

        //На листы
        public List<RoomObject> GetSimpleRoomsList() { return simpleRooms; }
        public List<RoomObject> GetNpcRoomsList() { return npcRooms; }
        public List<RoomObject> GetTraderRoomsList() { return traderRooms; }
        public List<RoomObject> GetBoxRoomsList() { return boxRooms; }
        public List<RoomObject> GetExitRoomsList() { return exitRooms; }

        //На рандомную комнату
        public GameObject GetRandomRoomInChance(int chance, bool remove)
        {
            List<RoomObject> roomsInChance = new List<RoomObject>();

            foreach (RoomObject roomToCheck in simpleRooms)
                if (roomToCheck.chance >= chance) roomsInChance.Add(roomToCheck);
            return roomsInChance[Random.Range(0, roomsInChance.Count)].room;
        }
        public GameObject GetRandomNpcRoomInChance(int chance, bool remove)
        {
            List<RoomObject> roomsInChance = new List<RoomObject>();

            foreach (RoomObject roomToCheck in npcRooms)
                if (roomToCheck.chance >= chance) roomsInChance.Add(roomToCheck);
            return roomsInChance[Random.Range(0, roomsInChance.Count)].room;

        }
        public GameObject GetRandomTraderRoomInChance(int chance, bool remove)
        {
            List<RoomObject> roomsInChance = new List<RoomObject>();

            foreach (RoomObject roomToCheck in traderRooms)
                if (roomToCheck.chance >= chance) roomsInChance.Add(roomToCheck);
            return roomsInChance[Random.Range(0, roomsInChance.Count)].room;

        }
        public GameObject GetRandomBoxRoomInChance(int chance, bool remove)
        {
            List<RoomObject> roomsInChance = new List<RoomObject>();

            foreach (RoomObject roomToCheck in boxRooms)
                if (roomToCheck.chance >= chance) roomsInChance.Add(roomToCheck);
            return roomsInChance[Random.Range(0, roomsInChance.Count)].room;

        }
        public GameObject GetRandomExitRoomInChance(int chance, bool remove)
        {
            List<RoomObject> roomsInChance = new List<RoomObject>();

            foreach (RoomObject roomToCheck in exitRooms)
                if (roomToCheck.chance >= chance) roomsInChance.Add(roomToCheck);
            return roomsInChance[Random.Range(0, roomsInChance.Count)].room;

        }
    }
}
