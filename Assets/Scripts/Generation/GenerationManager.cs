using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Generation
{
    [RequireComponent(typeof(RoomsLists))]
    public class GenerationManager : MonoBehaviour
    {
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
        
        //Другое
        private Rooms[] roomsMap;
        private RoomsLists roomsLists;

        //Геттеры, сеттеры
        public Rooms[] GetRoomsMap() => roomsMap;
        public RoomsLists GetRoomsLists => roomsLists;  
        public bool GetIsSpawned() => isRoomsSpawned; 
        public void SetIsSpawned() { if (!isRoomsSpawned) { isRoomsSpawned = true; afterSpawned.Invoke(); } }
        public int GetAllRoomsCount() => roomsCount + npcRoomsCount + boxesOnLevel; 
        public int GetNowSpawnedRoomsCount() => nowSpawnedRoomsCount; 
        public List<Room> GetNowSpawnedRooms() => rooms; 
        public void IncreaseSpawnedRoomsCount() => nowSpawnedRoomsCount++; 
        public void ReduceSpawnedRoomsCount() => nowSpawnedRoomsCount--; 
        public void AddRoom(Room newRoom) => rooms.Add(newRoom); 

        //Другое
        private void GenerateRoomsMap()
        {
            roomsMap = new Rooms[roomsCount + npcRoomsCount + boxesOnLevel];

            //Просто комнаты с НПС
            if (GetRoomsLists.GetRoomsList(Rooms.NPC).Count != 0) for (int i = 0; i < npcRoomsCount; i++)
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
            for (int i = 0; i < GetRoomsLists.GetRoomsList(Rooms.MustSpawn).Count; i++)
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
            if (isTraderWillSpawn && GetRoomsLists.GetRoomsList(Rooms.Trader).Count != 0) for (int i = 0; i < 1; i++)
                {
                    int traderRoomIndex = Random.Range(0, roomsMap.Length - 1);
                    if (roomsMap[traderRoomIndex] == Rooms.Default) roomsMap[traderRoomIndex] = Rooms.Trader;
                    else i--;
                }
            //Коробка
            if (GetRoomsLists.GetRoomsList(Rooms.Box).Count != 0) for (int i = 0; i < boxesOnLevel; i++)
                {
                    int boxRoomIndex = Random.Range(0, roomsMap.Length - 1);
                    if (roomsMap[boxRoomIndex] == Rooms.Default) roomsMap[boxRoomIndex] = Rooms.Box;
                    else i--;
                }
            //Сделать комнаты которые обязательно должны заспавнится

            //Выход
            roomsMap[roomsMap.Length - 1] = Rooms.Exit;
        }
        
        //Юнитивские методы
        private void Awake()
        {
            roomsLists = GetComponent<RoomsLists>();
            GenerateRoomsMap();
        }
    }
}