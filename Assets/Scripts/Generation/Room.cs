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
        [SerializeField] private List<Transform> pointsForSomething;
        [SerializeField] private bool isEnemyRoom = false;
        [Header("Настройки вражеской комнаты")]
        [SerializeField] private Door doors;
        [Header("Проходы")]
        [SerializeField] private RoomSpawner upPassage;
        [SerializeField] private Vector2 instantiatePositionUp = new Vector2(0f, 18f);
        [SerializeField] private RoomSpawner downPassage;
        [SerializeField] private Vector2 instantiatePositionDown = new Vector2(0f, -18f);
        [SerializeField] private RoomSpawner leftPassage;
        [SerializeField] private Vector2 instantiatePositionLeft = new Vector2(-18f, 0f);
        [SerializeField] private RoomSpawner rightPassage;
        [SerializeField] private Vector2 instantiatePositionRight = new Vector2(18f, 0f);

        private RoomSpawner startingSpawnPoint;
        private GenerationManager generationManager;

        //Геттеры
        public void SetStartingSpawnPoint(RoomSpawner point) 
        {
            startingSpawnPoint = point;
            startingSpawnPoint.onClose.AddListener(startingSpawnPoint.DestroyRoom);
        }
        public RoomSpawner StartingSpawnPoint => startingSpawnPoint;
        public bool IsStartRoom => isStartRoom;
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
                    if(isClosed) continue;
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
        public void SpawnSomething(GameObject something) 
        {
            int randomPlace = Random.Range(0, pointsForSomething.Count);
            Instantiate(something, pointsForSomething[randomPlace].position, Quaternion.identity, pointsForSomething[randomPlace]);
            pointsForSomething.RemoveAt(randomPlace);
        }

        private void Awake() { generationManager = FindObjectOfType<GenerationManager>(); if (randomizePassagesOnAwake) RandomizePassages(); }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(isEnemyRoom && collision.CompareTag("Player") && !doors.IsClosed)
                Utility.InvokeMethod(doors.CloseDoors, 0.5f);
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
    }
}