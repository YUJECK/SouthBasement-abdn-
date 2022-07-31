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
    [RequireComponent(typeof(RoomsLists))]
    public class GenerationManager : MonoBehaviour
    {
        public int[] roomsMap;
        [Header("Настройки комнат")]
        [SerializeField] private int roomsCount = 10;
        private int nowSpawnedRoomsCount = 0;
        [Header("Настройка НПС")]
        [SerializeField] private int npcRoomsCount = 1;
        private int nowSpawnedNpcRoomsCount = 0;
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
            roomsMap = new int[roomsCount + npcRoomsCount + boxesOnLevel];

            //Просто комнаты с НПС
            if(roomsLists.GetNpcRoomsList().Count != 0) for (int i = 0; i < npcRoomsCount; i++)
            {
                int tmp = Random.Range(0, roomsMap.Length - 1);

                if (roomsMap[tmp] == 0) roomsMap[tmp] = 1;
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
                if (roomsMap[traderRoomIndex] == 0) roomsMap[traderRoomIndex] = 2;
                else i--;
            }
            //Коробка
            if(roomsLists.GetBoxRoomsList().Count != 0) for (int i = 0; i < 1; i++)
            {
                int boxRoomIndex = Random.Range(0, roomsMap.Length-1);
                if (roomsMap[boxRoomIndex] == 0) roomsMap[boxRoomIndex] = 3;
                else i--;
            }
            //Сделать комнаты которые обязательно должны заспавнится

            //Выход
            roomsMap[roomsMap.Length - 1] = 5;
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