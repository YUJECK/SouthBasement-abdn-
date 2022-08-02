using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Generation
{
    [RequireComponent(typeof(RoomsLists))]
    public class GenerationManager : MonoBehaviour
    {
        public RoomsLists.Rooms[] roomsMap;
        [Header("Настройки комнат")]
        [SerializeField] private int roomsCount = 10;
        private int nowSpawnedRoomsCount = 0;
        [Header("Настройка НПС")]
        [SerializeField] private int npcRoomsCount = 1;
        [SerializeField] private int boxesOnLevel = 1;
        [SerializeField] private bool isTraderWillSpawn = true;

        [Header("События")]
        [SerializeField] private UnityEvent afterSpawned = new UnityEvent();
        private bool isRoomsSpawned = false;
        //Другое
        private RoomsLists _roomsLists;
        public RoomsLists roomsLists
        {
            get { return _roomsLists; }
            private set { _roomsLists = value; }
        }


        //Геттеры, сеттеры
        private void GenerateRoomsList()
        {
            roomsMap = new RoomsLists.Rooms[roomsCount + npcRoomsCount + boxesOnLevel];

            //Просто комнаты с НПС
            if(roomsLists.GetNpcRoomsList().Count != 0) for (int i = 0; i < npcRoomsCount; i++)
            {
                int tmp = Random.Range(0, roomsMap.Length - 1);

                if (roomsMap[tmp] == RoomsLists.Rooms.Default) roomsMap[tmp] = RoomsLists.Rooms.NPC;
                else
                {
                    i--;
                    Utility.ChechNumber(ref i, 0, 0, Utility.CheckNumber.Less);
                }
            }
            //Обязательные комнаты 
            for (int i = 0; i < roomsLists.GetMustSpawnRoomsList().Count; i++)
            {
                int tmp = Random.Range(0, roomsMap.Length - 1);

                if (roomsMap[tmp] == RoomsLists.Rooms.Default) roomsMap[tmp] = RoomsLists.Rooms.MustSpawn;
                else
                {
                    i--;
                    Utility.ChechNumber(ref i, 0, 0, Utility.CheckNumber.Less);
                }
            }
            //Торговец
            if(isTraderWillSpawn && roomsLists.GetNpcRoomsList().Count != 0) for (int i = 0; i < 1; i++)
            {
                int traderRoomIndex = Random.Range(0, roomsMap.Length - 1);
                if (roomsMap[traderRoomIndex] == RoomsLists.Rooms.Default) roomsMap[traderRoomIndex] = RoomsLists.Rooms.Trader;
                else i--;
            }
            //Коробка
            if(roomsLists.GetBoxRoomsList().Count != 0) for (int i = 0; i < 1; i++)
            {
                int boxRoomIndex = Random.Range(0, roomsMap.Length-1);
                if (roomsMap[boxRoomIndex] == RoomsLists.Rooms.Default) roomsMap[boxRoomIndex] = RoomsLists.Rooms.Box;
                else i--;
            }
            //Сделать комнаты которые обязательно должны заспавнится

            //Выход
            roomsMap[roomsMap.Length - 1] = RoomsLists.Rooms.Exit;
        }
        public bool GetIsSpawned() { return isRoomsSpawned; }
        public void SetIsSpawned() { if (!isRoomsSpawned) { isRoomsSpawned = true; afterSpawned.Invoke(); } }
        public int GetAllRoomsCount() { return roomsCount + npcRoomsCount + boxesOnLevel; }
        public int GetNowSpawnedRoomsCount() { return nowSpawnedRoomsCount; }
        public void IncreaseSpawnedRoomsCount() { nowSpawnedRoomsCount++; }
        
        //Юнитивские методы
        private void Awake()
        {
            _roomsLists = GetComponent<RoomsLists>();
            GenerateRoomsList();
        }
    }
}