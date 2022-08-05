using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Generation
{
    [RequireComponent(typeof(RoomsLists))]
    public class GenerationManager : MonoBehaviour
    {
        private Rooms[] _roomsMap;
        public Rooms[] roomsMap
        {
            get { return _roomsMap; }
            private set { _roomsMap = value; }
        }
        [Header("Настройки комнат")]
        private List<Room> rooms = new List<Room>();
        [SerializeField] private int roomsCount = 10;
        private int nowSpawnedRoomsCount = 0;
        [Header("Настройка НПС")]
        [SerializeField] private int npcRoomsCount = 1;
        [SerializeField] private int boxesOnLevel = 1;
        [SerializeField] private bool isTraderWillSpawn = true;

        [Header("События")]
        public UnityEvent afterSpawned = new UnityEvent();
        private bool isRoomsSpawned = false;
        public GameObject testPrefab;
        //Другое
        private RoomsLists _roomsLists;
        public RoomsLists roomsLists
        {
            get { return _roomsLists; }
            private set { _roomsLists = value; }
        }


        //Геттеры, сеттеры
        private void GenerateRoomsMap()
        {
            roomsMap = new Rooms[roomsCount + npcRoomsCount + boxesOnLevel];

            //Просто комнаты с НПС
            if (roomsLists.GetRoomsList(Rooms.NPC).Count != 0) for (int i = 0; i < npcRoomsCount; i++)
                {
                    int tmp = Random.Range(0, roomsMap.Length - 1);

                    if (roomsMap[tmp] == Rooms.Default) roomsMap[tmp] = Rooms.NPC;
                    else
                    {
                        i--;
                        Utility.ChechNumber(ref i, 0, 0, Utility.CheckNumber.Less);
                    }
                }
            //Обязательные комнаты 
            for (int i = 0; i < roomsLists.GetRoomsList(Rooms.MustSpawn).Count; i++)
            {
                int tmp = Random.Range(0, roomsMap.Length - 1);

                if (roomsMap[tmp] == Rooms.Default) roomsMap[tmp] = Rooms.MustSpawn;
                else
                {
                    i--;
                    Utility.ChechNumber(ref i, 0, 0, Utility.CheckNumber.Less);
                }
            }
            //Торговец
            if (isTraderWillSpawn && roomsLists.GetRoomsList(Rooms.Trader).Count != 0) for (int i = 0; i < 1; i++)
                {
                    int traderRoomIndex = Random.Range(0, roomsMap.Length - 1);
                    if (roomsMap[traderRoomIndex] == Rooms.Default) roomsMap[traderRoomIndex] = Rooms.Trader;
                    else i--;
                }
            //Коробка
            if (roomsLists.GetRoomsList(Rooms.Box).Count != 0) for (int i = 0; i < boxesOnLevel; i++)
                {
                    int boxRoomIndex = Random.Range(0, roomsMap.Length - 1);
                    if (roomsMap[boxRoomIndex] == Rooms.Default) roomsMap[boxRoomIndex] = Rooms.Box;
                    else i--;
                }
            //Сделать комнаты которые обязательно должны заспавнится

            //Выход
            roomsMap[roomsMap.Length - 1] = Rooms.Exit;
        }
        public bool GetIsSpawned() { return isRoomsSpawned; }
        public void SetIsSpawned() { if (!isRoomsSpawned) { isRoomsSpawned = true; rooms[Random.Range(0, rooms.Count)].SpawnSomething(testPrefab); afterSpawned.Invoke(); } }
        public int GetAllRoomsCount() { return roomsCount + npcRoomsCount + boxesOnLevel; }
        public int GetNowSpawnedRoomsCount() { return nowSpawnedRoomsCount; }
        public void IncreaseSpawnedRoomsCount() { nowSpawnedRoomsCount++; }
        public void ReduceSpawnedRoomsCount() { nowSpawnedRoomsCount--; }
        public void AddRoom(Room newRoom) { rooms.Add(newRoom); }

        //Юнитивские методы
        private void Awake()
        {
            _roomsLists = GetComponent<RoomsLists>();
            GenerateRoomsMap();
        }
    }
}