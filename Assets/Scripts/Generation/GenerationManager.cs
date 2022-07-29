using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Generation
{
    public class GenerationManager : MonoBehaviour
    {
        [System.Serializable]
        public class RoomObject
        {
            public GameObject room;
            public int chance;
        }

        [Header("Настройки комнат")]
        [SerializeField] private int[] roomsMap;
        [SerializeField] private int roomsCount = 10;
        [SerializeField] private int nowSpawnedRoomsCount = 0;
        [SerializeField] private List<RoomObject> rooms = new List<RoomObject>();

        [Header("Настройка НПС")]
        [SerializeField] private int npcRoomsCount = 1;
        [SerializeField] private int nowSpawnedNpcRoomsCount = 0;
        [SerializeField] private int boxesOnLevel = 1;
        [SerializeField] private bool isTraderWillSpawn = true;
        [SerializeField] private List<RoomObject> npcRooms = new List<RoomObject>();
        [SerializeField] private List<RoomObject> traderRooms = new List<RoomObject>();
        [SerializeField] private List<RoomObject> boxRooms = new List<RoomObject>();

        [Header("События")]
        [SerializeField] private UnityEvent afterSpawned = new UnityEvent();
        private bool isRoomsSpawned = false;

        //Геттеры, сеттеры
        private void GenerateRoomsList()
        {
            roomsMap = new int[roomsCount + npcRoomsCount + boxesOnLevel];
            List<bool> isEmploy = new List<bool>(roomsMap.Length);

            for (int i = 0; i < npcRoomsCount; i++)
            {
                int tmp = Random.Range(0, roomsMap.Length);
             
                if(!isEmploy[tmp] == true)
                {
                    roomsMap[tmp] = 1;
                    isEmploy[tmp] = true;
                }
            }
        }
        public bool GetIsSpawned() { return isRoomsSpawned; }
        public void SetIsSpawned() { if (!isRoomsSpawned) { isRoomsSpawned = true; afterSpawned.Invoke(); } }
        public int GetRoomsCount() { return roomsCount; }
        public int GetNowSpawnedRoomsCount() { return nowSpawnedRoomsCount; }
        public void IncreaseSpawnedRoomsCount() { nowSpawnedRoomsCount++; }
        public GameObject GetRandomRoomInChance(int chance)
        {
            List<RoomObject> roomsInChance = new List<RoomObject>();

            foreach (RoomObject roomToCheck in rooms)
                if (roomToCheck.chance >= chance) roomsInChance.Add(roomToCheck);
            return roomsInChance[Random.Range(0, roomsInChance.Count)].room;
        }

        private void Awake()
        {
            GenerateRoomsList();
        }
    }
}
