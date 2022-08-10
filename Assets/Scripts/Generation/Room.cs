using System.Collections.Generic;
using UnityEngine;

namespace Generation
{
    public class Room : MonoBehaviour
    {
        public enum Directions
        {
            Up,
            Down,
            Left,
            Right
        }

        [Header("Настройки")]
        [SerializeField] private int passagesCountMin = 2;
        [SerializeField] public int passagesCountMax = 3;
        [SerializeField] private bool randomizePassagesOnAwake = false;
        [SerializeField] private bool isStartRoom = false;
        [SerializeField] private bool isPassageRoom = false;
        [SerializeField] private List<Transform> pointsForSomething;
        [Header("Проходы")]
        [SerializeField] private RoomSpawner upPassage;
        [SerializeField] private RoomSpawner downPassage;
        [SerializeField] private RoomSpawner leftPassage;
        [SerializeField] private RoomSpawner rightPassage;
        [Header("Настройки спавна")]
        [SerializeField] private Vector2 instantiatePositionUp = new Vector2(0f, 18f);
        [SerializeField] private Vector2 instantiatePositionDown = new Vector2(0f, -18f);
        [SerializeField] private Vector2 instantiatePositionLeft = new Vector2(-18f, 0f);
        [SerializeField] private Vector2 instantiatePositionRight = new Vector2(18f, 0f);

        //Info
        private List<Room> spawnedRooms = new List<Room>();
        private RoomSpawner startingSpawnPoint;
        private GenerationManager generationManager;

        //Геттеры и сеттеры
        public List<Room> SpawnedRooms => spawnedRooms;
        public bool IsStartRoom => isStartRoom;
        public bool IsPassageRoom => isPassageRoom;
        public RoomSpawner StartingSpawnPoint => startingSpawnPoint;
        public void AddSpawnedRoom(Room newRoom) => spawnedRooms.Add(newRoom);
        public void SetStartingSpawnPoint(RoomSpawner point)
        {
            startingSpawnPoint = point;
            startingSpawnPoint.onClose.AddListener(startingSpawnPoint.DestroyRoom);
        }
        public Vector2 GetInstantiatePosition(Directions direction)
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
        public RoomSpawner GetPassage(Directions direction)
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
        public void SpawnSomething(GameObject something)
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
            generationManager = FindObjectOfType<GenerationManager>();
            if (randomizePassagesOnAwake) RandomizePassages();

            if (upPassage != null)
            {
                upPassage.SetOwnRoom(this);
                upPassage.StartSpawnningRoom(generationManager.roomSpawnOffset + 0.05f);
            }
            if (downPassage != null)
            {
                downPassage.SetOwnRoom(this);
                downPassage.StartSpawnningRoom(generationManager.roomSpawnOffset + 0.06f);
            }

            if (leftPassage != null)
            {
                leftPassage.SetOwnRoom(this);
                leftPassage.StartSpawnningRoom(generationManager.roomSpawnOffset + 0.07f);
            }
            if (rightPassage != null)
            {
                rightPassage.SetOwnRoom(this);
                rightPassage.StartSpawnningRoom(generationManager.roomSpawnOffset + 0.08f);
            }

            if (isPassageRoom && spawnedRooms.Count == 0) Destroy(gameObject); 
        }
        private void OnDestroy()
        {
            generationManager.ReduceSpawnedRoomsCount();
            if (upPassage != null && upPassage.SpawnedRoom != null)
                Destroy(upPassage.SpawnedRoom);
            if (downPassage != null && downPassage.SpawnedRoom != null)
                Destroy(downPassage.SpawnedRoom);
            if (leftPassage != null && leftPassage.SpawnedRoom != null)
                Destroy(leftPassage.SpawnedRoom);
            if (rightPassage != null && rightPassage.SpawnedRoom != null)
                Destroy(rightPassage.SpawnedRoom);
            startingSpawnPoint.ForcedClose();
        }
        private void OnTriggerEnter2D(Collider2D collision) { if (collision.CompareTag("Room")) startingSpawnPoint.DestroyRoom(); }
    }
}