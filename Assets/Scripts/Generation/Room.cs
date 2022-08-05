using System.Collections.Generic;
using UnityEngine;

namespace Generation
{
    public class Room : MonoBehaviour
    {
        public enum Directories
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
        [SerializeField] private bool _isStartRoom = false;
        [Header("Проходы")]
        [SerializeField] private RoomSpawner upPassage;
        [SerializeField] private Vector2 instantiatePositionUp = new Vector2(0f, 18f);
        [SerializeField] private RoomSpawner downPassage;
        [SerializeField] private Vector2 instantiatePositionDown = new Vector2(0f, -18f);
        [SerializeField] private RoomSpawner leftPassage;
        [SerializeField] private Vector2 instantiatePositionLeft = new Vector2(-18f, 0f);
        [SerializeField] private RoomSpawner rightPassage;
        [SerializeField] private Vector2 instantiatePositionRight = new Vector2(18f, 0f);
        [SerializeField] private List<Transform> pointsForSomething;

        private List<Directories> _passagesMustSpawned = new List<Directories>();
        private RoomSpawner _spawnPoint;

        //Геттеры
        public bool isStartRoom { get => _isStartRoom; set => _isStartRoom = value; }
        public RoomSpawner spawnPoint { get => _spawnPoint; set => _spawnPoint = value; }
        public List<Directories> passagesMustSpawned { get => _passagesMustSpawned; set => _passagesMustSpawned = value; }
        public Vector2 GetInstantiatePosition(Directories direction)
        {
            switch (direction)
            {
                case Directories.Up:
                    return instantiatePositionUp;
                case Directories.Down:
                    return instantiatePositionDown;
                case Directories.Left:
                    return instantiatePositionLeft;
                case Directories.Right:
                    return instantiatePositionRight;
            }
            return new Vector2(18f, 18f);
        }
        public RoomSpawner GetPassage(Directories direction)
        {
            switch (direction)
            {
                case Directories.Up:
                    return upPassage;
                case Directories.Down:
                    return downPassage;
                case Directories.Left:
                    return leftPassage;
                case Directories.Right:
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
                if (index == 0 && upPassage.GetState() == RoomSpawnerState.Open)
                { 
                    upPassage.Close(true);
                    continue;
                } 
                if (index == 1 && downPassage.GetState() == RoomSpawnerState.Open)
                {
                    downPassage.Close(true); 
                    continue;
                }
                if (index == 2 && leftPassage.GetState() == RoomSpawnerState.Open)
                {
                    leftPassage.Close(true);
                    continue;
                }
                if (index == 3 && rightPassage.GetState() == RoomSpawnerState.Open)
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
    }
}