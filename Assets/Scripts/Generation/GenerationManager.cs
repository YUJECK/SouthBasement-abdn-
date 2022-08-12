using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Generation
{
    [RequireComponent(typeof(RoomsLists))]
    public class GenerationManager : MonoBehaviour
    {
        [SerializeField] private string locationName = "Basement"; //Имя локации
        [Header("Настройки комнат")]
        private List<Room> rooms = new List<Room>(); //Лист вмех заспавненных комнат
        [Range(1, 100)] [SerializeField] private int roomsCount = 10; //Кол-во обычных комнат
        [Range(0, 100)] [SerializeField] private int passagesCount = 2; //Кол-во комнат-проходов
        [Range(0, 100)] [SerializeField] private int npcRoomsCount = 1; //Кол-во комнат с НПС
        [Range(0, 100)] [SerializeField] private int boxesOnLevel = 1; //Кол-во коробок на уровне
        [SerializeField] private bool isTraderWillSpawn = true; //Будет ли спавнится торговец
        [Header("События")]
        public UnityEvent afterSpawned = new UnityEvent();
        private bool isRoomsSpawned = false;

        //Другое
        private float roomsSpawnOffset = 0.05f; //Изначальный промежуток спавна комнат
        private Rooms[] roomsMap; //Карта комнат
        private RoomsLists roomsLists; //Ссылка на листы со всемы комнатами

        //Геттеры, сеттеры
        public float RoomsSpawnOffset => roomsSpawnOffset; //Изначальный промежуток спавна комнат
        public string LocationName => locationName; //Имя локации
        public Rooms[] RoomsMap => roomsMap; //Карта комнат
        public RoomsLists RoomsLists => roomsLists; //Ссылка на листы со всемы комнатами
        public bool IsSpawned => isRoomsSpawned; //Закончен ли спавн всех комнат
        public void SetIsSpawned() { if (!isRoomsSpawned) { isRoomsSpawned = true; afterSpawned.Invoke(); Debug.Log("[Info]: Rooms have been spawned"); } }
        public int AllRoomsCount => roomsCount + npcRoomsCount + boxesOnLevel + 1; //Получить общее кол-во комнат
        public int NowSpawnedRoomsCount => rooms.Count; //Сколько комнат заспавнено сейчас
        public void AddRoomToList(Room newRoom) => rooms.Add(newRoom); //Добавить комнату в список всех заспавненных комнат
        public void RemoveRoomFromList(Room removableRoom) => rooms.Remove(removableRoom); //Убрать комнату из списка всех заспавненных комнат

        //Другое
        private void GenerateRoomsMap() //Сгенерировать карту комнат
        {
            roomsMap = new Rooms[AllRoomsCount];

            //Просто комнаты с НПС
            if (RoomsLists.GetRoomsList(Rooms.NPC).Count != 0) for (int i = 0; i < npcRoomsCount; i++)
                {
                    int tmp = Random.Range(0, roomsMap.Length - 1);

                    if (roomsMap[tmp] == Rooms.Default) roomsMap[tmp] = Rooms.NPC;
                    else
                    {
                        i--;
                        Utility.ChechNumber(ref i, 0, 0, Utility.CheckNumber.Less);
                    }
                }
            //Торговец
            if (isTraderWillSpawn && RoomsLists.GetRoomsList(Rooms.Trader).Count != 0) for (int i = 0; i < 1; i++)
                {
                    int traderRoomIndex = Random.Range(0, roomsMap.Length - 1);
                    if (roomsMap[traderRoomIndex] == Rooms.Default) roomsMap[traderRoomIndex] = Rooms.Trader;
                    else i--;
                }
            //Коробка
            if (RoomsLists.GetRoomsList(Rooms.Box).Count != 0) for (int i = 0; i < boxesOnLevel; i++)
                {
                    int boxRoomIndex = Random.Range(0, roomsMap.Length - 1);
                    if (roomsMap[boxRoomIndex] == Rooms.Default) roomsMap[boxRoomIndex] = Rooms.Box;
                    else i--;
                }
            //Обязательные комнаты 
            for (int i = 0; i < RoomsLists.GetRoomsList(Rooms.MustSpawn).Count; i++)
            {
                int tmp = Random.Range(0, roomsMap.Length - 1);

                if (roomsMap[tmp] == Rooms.Default) roomsMap[tmp] = Rooms.MustSpawn;
                else
                {
                    i--;
                    Utility.ChechNumber(ref i, 0, 0, Utility.CheckNumber.Less);
                }
            }
            //Комнаты коридоры
            for (int i = 0; i < passagesCount; i++)
            {
                int tmp = Random.Range(0, roomsMap.Length - 5);

                if (roomsMap[tmp] == Rooms.Default) roomsMap[tmp] = Rooms.Passage;
                else
                {
                    i--;
                    Utility.ChechNumber(ref i, 0, 0, Utility.CheckNumber.Less);
                }
            }
            //Выход
            roomsMap[roomsMap.Length - 1] = Rooms.Exit;
        }
        public Room.Directions GetOppositeDirection(Room.Directions direction)//Получить противоположное направравление
        {
            switch (direction)
            {
                case Room.Directions.Up:
                    return Room.Directions.Down;
                case Room.Directions.Down:
                    return Room.Directions.Up;
                case Room.Directions.Left:
                    return Room.Directions.Right;
                case Room.Directions.Right:
                    return Room.Directions.Left;
            }
            return Room.Directions.Up;
        }

        //Юнитивские методы
        private void Awake()
        {
            roomsLists = GetComponent<RoomsLists>();
            GenerateRoomsMap();
        }
    }
}