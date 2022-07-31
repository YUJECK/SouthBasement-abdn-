using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Generation
{
    // 0 - обычная комната
    // 1 - комната с НПС
    // 2 - комната торговца
    // 3 - комната с коробкой
    // 4 - комната из листа roomsMustSpawn
    // 5 - комната выхода/с боссом
    public class GenerationManager : MonoBehaviour
    {
        [System.Serializable]
        public class RoomObject
        {
            public GameObject room;
            public int chance;
        }

        [Header("Настройки комнат")]
        public int[] roomsMap;
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

            //Просто комнаты с НПС
            if(npcRooms.Count != 0) for (int i = 0; i < npcRoomsCount; i++)
            {
                int tmp = Random.Range(0, roomsMap.Length);

                if (roomsMap[tmp] == 0) roomsMap[tmp] = 1;
                else
                {
                    i--;
                    Utility.ChechNumber(ref i, 0, 0, Utility.CheckNumber.Less);
                }
            }
            //Торговец
            if(traderRooms.Count != 0) for (int i = 0; i < 1; i++)
            {
                int traderRoomIndex = Random.Range(0, roomsMap.Length);
                if (roomsMap[traderRoomIndex] == 0) roomsMap[traderRoomIndex] = 2;
                else i--;
            }
            //Коробка
            if(boxRooms.Count != 0) for (int i = 0; i < 1; i++)
            {
                int boxRoomIndex = Random.Range(0, roomsMap.Length);
                if (roomsMap[boxRoomIndex] == 0) roomsMap[boxRoomIndex] = 3;
                else i--;
            }

            //Сделать комнаты которые обязательно должны заспавнится
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
        public GameObject GetRandomNpcRoomInChance(int chance)
        {
            List<RoomObject> roomsInChance = new List<RoomObject>();

            foreach (RoomObject roomToCheck in npcRooms)
                if (roomToCheck.chance >= chance) roomsInChance.Add(roomToCheck);
            return roomsInChance[Random.Range(0, roomsInChance.Count)].room;

        }
        public GameObject GetRandomTraderRoomInChance(int chance)
        {
            List<RoomObject> roomsInChance = new List<RoomObject>();

            foreach (RoomObject roomToCheck in traderRooms)
                if (roomToCheck.chance >= chance) roomsInChance.Add(roomToCheck);
            return roomsInChance[Random.Range(0, roomsInChance.Count)].room;

        }
        public GameObject GetRandomBoxRoomInChance(int chance)
        {
            List<RoomObject> roomsInChance = new List<RoomObject>();

            foreach (RoomObject roomToCheck in boxRooms)
                if (roomToCheck.chance >= chance) roomsInChance.Add(roomToCheck);
            return roomsInChance[Random.Range(0, roomsInChance.Count)].room;

        }
        
        //Юнитивские методы
        private void Awake()
        {
            GenerateRoomsList();
        }
    }
}