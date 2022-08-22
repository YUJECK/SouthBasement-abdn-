using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Generation
{
    [RequireComponent(typeof(RoomsLists))]
    public class GenerationManager : MonoBehaviour
    {
        [SerializeField] private RoomTemplate startRoom;  
        [Header("Настройки комнат")]
        private List<RoomTemplate> rooms = new List<RoomTemplate>(); //Лист вмех заспавненных комнат
        [Range(2, 50)] [SerializeField] private int roomsCount = 10; //Кол-во обычных комнат
        [Range(0, 25)] [SerializeField] private int passagesCount = 2; //Кол-во комнат-проходов
        [Range(0, 20)] [SerializeField] private int npcRoomsCount = 1; //Кол-во комнат с НПС
        [Range(0, 25)] [SerializeField] private int boxesOnLevel = 1; //Кол-во коробок на уровне
        [SerializeField] private bool isTraderWillSpawn = true; //Будет ли спавнится торговец
        [Header("События")]
        public UnityEvent afterSpawned = new UnityEvent();
        private bool isRoomsSpawned = false;

        //Другое
        [SerializeField] private float roomsSpawnOffset = 0.05f; //Изначальный промежуток спавна комнат
        private Rooms[] roomsMap; //Карта комнат
        private RoomsLists roomsLists; //Ссылка на листы со всемы комнатами
        private LevelInformatoin levelInformatoin; //Ссылка на информацию о уровне

        //Геттеры, сеттеры
        public float RoomsSpawnOffset => roomsSpawnOffset; //Изначальный промежуток спавна комнат
        public Rooms[] RoomsMap => roomsMap; //Карта комнат
        public RoomsLists RoomsLists => roomsLists; //Ссылка на листы со всемы комнатами
        public LevelInformatoin LevelInformatoin => levelInformatoin; //Ссылка на информацию о уровне
        public bool IsSpawned => isRoomsSpawned; //Закончен ли спавн всех комнат
        public void SetIsSpawned() { if (!isRoomsSpawned) { isRoomsSpawned = true; afterSpawned.Invoke(); Debug.Log("[Info]: Rooms have been spawned"); } }
        public int AllRoomsCount => roomsCount + npcRoomsCount + boxesOnLevel + passagesCount + 1; //Получить общее кол-во комнат
        public int NowSpawnedRoomsCount => rooms.Count; //Сколько комнат заспавнено сейчас
        public void AddRoomToList(RoomTemplate newRoom) => rooms.Add(newRoom); //Добавить комнату в список всех заспавненных комнат
        public void RemoveRoomFromList(RoomTemplate removableRoom) => rooms.Remove(removableRoom); //Убрать комнату из списка всех заспавненных комнат

        //Другое
        private void CheckSpawnedRoomsCount()
        {
            if (rooms.Count < roomsMap.Length)
            {
                Debug.Log("[Info]: Rooms count less than normal");
                isRoomsSpawned = false;
                startRoom.GetPassage(RoomTemplate.Directions.Up)?.RegenerateRoom();
                startRoom.GetPassage(RoomTemplate.Directions.Down)?.RegenerateRoom();
                startRoom.GetPassage(RoomTemplate.Directions.Left)?.RegenerateRoom();
                startRoom.GetPassage(RoomTemplate.Directions.Right)?.RegenerateRoom();
            }
        }
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
                        Utility.CheckNumber(ref i, 0, 0, Utility.CheckNumberVariants.Less);
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
                    Utility.CheckNumber(ref i, 0, 0, Utility.CheckNumberVariants.Less);
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
                    Utility.CheckNumber(ref i, 0, 0, Utility.CheckNumberVariants.Less);
                }
            }
            //Выход
            roomsMap[roomsMap.Length - 1] = Rooms.Exit;
        }
        public RoomTemplate.Directions GetOppositeDirection(RoomTemplate.Directions direction)//Получить противоположное направравление
        {
            switch (direction)
            {
                case RoomTemplate.Directions.Up:
                    return RoomTemplate.Directions.Down;
                case RoomTemplate.Directions.Down:
                    return RoomTemplate.Directions.Up;
                case RoomTemplate.Directions.Left:
                    return RoomTemplate.Directions.Right;
                case RoomTemplate.Directions.Right:
                    return RoomTemplate.Directions.Left;
            }
            Debug.Log("![Error]!");
            return RoomTemplate.Directions.Up;
        }

        //Юнитивские методы
        private void Awake()
        {
            roomsLists = GetComponent<RoomsLists>();
            GenerateRoomsMap();
            afterSpawned.AddListener(CheckSpawnedRoomsCount);
        }
    }
}