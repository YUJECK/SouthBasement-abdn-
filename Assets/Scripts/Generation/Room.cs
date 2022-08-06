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
        [Header("Проходы")]
        [SerializeField] private RoomSpawner upPassage;
        [SerializeField] private Vector2 instantiatePositionUp = new Vector2(0f, 18f);
        [SerializeField] private RoomSpawner downPassage;
        [SerializeField] private Vector2 instantiatePositionDown = new Vector2(0f, -18f);
        [SerializeField] private RoomSpawner leftPassage;
        [SerializeField] private Vector2 instantiatePositionLeft = new Vector2(-18f, 0f);
        [SerializeField] private RoomSpawner rightPassage;
        [SerializeField] private Vector2 instantiatePositionRight = new Vector2(18f, 0f);

        [HideInInspector] public RoomSpawner startingSpawnPoint;

        //Геттеры
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
                    upPassage.Close(true);
                    continue;
                } 
                if (index == 1 && downPassage.State == RoomSpawnerState.Open)
                {
                    downPassage.Close(true); 
                    continue;
                }
                if (index == 2 && leftPassage.State == RoomSpawnerState.Open)
                {
                    leftPassage.Close(true);
                    continue;
                }
                if (index == 3 && rightPassage.State == RoomSpawnerState.Open)
                {
                    rightPassage.Close(true);
                    continue;
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

        private void Awake() { if (randomizePassagesOnAwake) RandomizePassages(); }
        private void OnDestroy()
        {
            if (upPassage != null && upPassage.SpawnedRoom != null)
                upPassage.SpawnedRoom.GetComponent<Room>().downPassage.ForcedClose();
            if (downPassage != null && downPassage.SpawnedRoom != null)
                downPassage.SpawnedRoom.GetComponent<Room>().upPassage.ForcedClose();
            if (leftPassage != null && leftPassage.SpawnedRoom != null)
                leftPassage.SpawnedRoom.GetComponent<Room>().rightPassage.ForcedClose();
            if (rightPassage != null && rightPassage.SpawnedRoom != null)
                rightPassage.SpawnedRoom.GetComponent<Room>().leftPassage.ForcedClose();
            startingSpawnPoint.ForcedClose();
        }
    }
}