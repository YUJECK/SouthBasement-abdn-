using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Generation
{
    public class Room : MonoBehaviour
    {
        public enum Directions //Список направлений
        {
            Up,
            Down,
            Left,
            Right
        }

        [Header("Настройки")]
        [SerializeField] private int passagesCountMin = 2; //Минимальное, максимальное кол-во проходов при их рандоме
        [SerializeField] public int passagesCountMax = 3;
        [SerializeField] private bool randomizePassagesOnAwake = false; //Будут ли рандомится проходы при вызове Awake()
        [SerializeField] private bool isStartRoom = false; //Начальная ли комната
        [SerializeField] private bool isPassageRoom = false; //Если это комната-коридор
        [SerializeField] private List<Transform> pointsForSomething; //Точки для спавна различных вещей
        [Header("Проходы")] //Проходы
        [SerializeField] private RoomSpawner upPassage; //Верхний проход
        [SerializeField] private RoomSpawner downPassage; //Нижний проход
        [SerializeField] private RoomSpawner leftPassage; //Левый проход
        [SerializeField] private RoomSpawner rightPassage; //Правый проход
        [Header("Настройки спавна")] //Позиции для точки спавна
        [SerializeField] private Vector2 instantiatePositionUp = new Vector2(0f, 18f);
        [SerializeField] private Vector2 instantiatePositionDown = new Vector2(0f, -18f);
        [SerializeField] private Vector2 instantiatePositionLeft = new Vector2(-18f, 0f);
        [SerializeField] private Vector2 instantiatePositionRight = new Vector2(18f, 0f);

        //Ссылки на другие вещи
        private List<Room> spawnedRooms = new List<Room>(); // Заспавненные здесь комнаты
        private RoomSpawner startingSpawnPoint; // Точка откуда была заспавнена эта комната

        //Геттеры и сеттеры
        public List<Room> SpawnedRooms => spawnedRooms; //Кол-во заспавненых здесь комнат
        public bool IsStartRoom => isStartRoom; //Начальная ли комната
        public bool IsPassageRoom => isPassageRoom; //Комната-коридор
        public RoomSpawner StartingSpawnPoint => startingSpawnPoint; // Точка откуда была заспавнена эта комната
        public void AddSpawnedRoom(Room newRoom) => spawnedRooms.Add(newRoom); //Добавить заспавненную комнату
        public void SetStartingSpawnPoint(RoomSpawner point) //Поставить начальную точку
        {
            startingSpawnPoint = point;
            startingSpawnPoint.onClose.AddListener(startingSpawnPoint.DestroyRoom);
        } 
        public Vector2 GetInstantiatePosition(Directions direction) //Получить позицию спавна по направлению
        {
            switch (direction)
            {
                case Directions.Up:
                    return instantiatePositionUp;
                case Directions.Down:
                    return instantiatePositionDown;
                case Directions.Left:
                    return instantiatePositionLeft;
                case Directions.Right:
                    return instantiatePositionRight;
            }
            return new Vector2(18f, 18f);
        }
        public RoomSpawner GetPassage(Directions direction) //Получить проход по направлению
        {
            switch (direction)
            {
                case Directions.Up:
                    return upPassage;
                case Directions.Down:
                    return downPassage;
                case Directions.Left:
                    return leftPassage;
                case Directions.Right:
                    return rightPassage;
            }
            return null;
        }

        //Другое
        //Выполение методы при определенном кол-ве заспавненых здесь комнаты
        private void CheckSpawnedRoomsCount(int roomsCount, UnityAction action) { if (spawnedRooms.Count == roomsCount) action.Invoke(); }
        public void SpawnSomething(GameObject something) //Спавн чего-то в комнате
        {
            int randomPlace = Random.Range(0, pointsForSomething.Count);
            Instantiate(something, pointsForSomething[randomPlace].position, Quaternion.identity, pointsForSomething[randomPlace]);
            pointsForSomething.RemoveAt(randomPlace);
        }
        public void RandomizePassages() //Рандомизация проходов
        {

            //Проверка кол-ва проходов
            if (passagesCountMin <= 0) passagesCountMin = 1;
            if (passagesCountMax > 4) passagesCountMax = 4;

            int passagesCount = Random.Range(passagesCountMin, passagesCountMax + 1);

            for (int i = 0; i < 4 - passagesCount; i++)
            {
                int index = Random.Range(0, 4);
                if (index == 0 && upPassage.State == RoomSpawnerState.Open)
                {
                    bool isClosed = upPassage.Close(true);
                    if (isClosed) continue;
                }
                if (index == 1 && downPassage.State == RoomSpawnerState.Open)
                {
                    bool isClosed = downPassage.Close(true);
                    if (isClosed) continue;
                }
                if (index == 2 && leftPassage.State == RoomSpawnerState.Open)
                {
                    bool isClosed = leftPassage.Close(true);
                    if (isClosed) continue;
                }
                if (index == 3 && rightPassage.State == RoomSpawnerState.Open)
                {
                    bool isClosed = rightPassage.Close(true);
                    if (isClosed) continue;
                }
                i--;
            }
        }

        //Юнитивские методы
        private void Start()
        { 
            if (randomizePassagesOnAwake) RandomizePassages();

            //Спавн комнат
            if (upPassage != null)
            {
                upPassage.SetOwnRoom(this);
                upPassage.StartSpawnningRoom(ManagerList.GenerationManager.RoomsSpawnOffset + 0.05f);
                if(IsPassageRoom) upPassage.onSpawn.AddListener( () => { if (SpawnedRooms.Count == 0) startingSpawnPoint.DestroyRoom(); });
            }
            if (downPassage != null)
            {
                downPassage.SetOwnRoom(this);
                downPassage.StartSpawnningRoom(ManagerList.GenerationManager.RoomsSpawnOffset + 0.07f);
                if(IsPassageRoom) downPassage.onSpawn.AddListener(() => { if (SpawnedRooms.Count == 0) startingSpawnPoint.DestroyRoom(); });
            }
            if (leftPassage != null)
            {
                leftPassage.SetOwnRoom(this);
                leftPassage.StartSpawnningRoom(ManagerList.GenerationManager.RoomsSpawnOffset + 0.09f);
                if (IsPassageRoom) leftPassage.onSpawn.AddListener(() => { if (SpawnedRooms.Count == 0) startingSpawnPoint.DestroyRoom(); });
            }
            if (rightPassage != null)
            {
                rightPassage.SetOwnRoom(this);
                rightPassage.StartSpawnningRoom(ManagerList.GenerationManager.RoomsSpawnOffset + 0.11f);
                if (IsPassageRoom) rightPassage.onSpawn.AddListener(() => { if (SpawnedRooms.Count == 0) startingSpawnPoint.DestroyRoom(); });
            }
        }
        private void OnDestroy()
        {
            //Уничтожение заспавненных здесь комнат и закрытие начального прохода
            if (upPassage != null && upPassage.SpawnedRoom != null)
                upPassage.DestroyRoom();
            if (downPassage != null && downPassage.SpawnedRoom != null)
                downPassage.DestroyRoom();
            if (leftPassage != null && leftPassage.SpawnedRoom != null)
                leftPassage.DestroyRoom();
            if (rightPassage != null && rightPassage.SpawnedRoom != null)
                rightPassage.DestroyRoom();
            if (StartingSpawnPoint != null) StartingSpawnPoint.ForcedClose();
        }
    }
}