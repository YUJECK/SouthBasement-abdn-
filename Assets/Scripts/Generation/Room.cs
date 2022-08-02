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

        public bool isStartRoom { get => _isStartRoom; set => _isStartRoom = value; }
        [SerializeField] private bool _isStartRoom = false;
        public int passagesCountMin = 2;
        public int passagesCountMax = 3;

        [Header("Проходы")]
        public List<Directories> passagesMustSpawned = new List<Directories>();
        [SerializeField] private bool randomizePassagesOnAwake = false;
        public RoomSpawner spawnPoint;
        [SerializeField] private List<Transform> pointsForSomething;
        public RoomSpawner upPassage;
        public Vector2 instantiatePositionUp = new Vector2(0f, 18f);
        public RoomSpawner downPassage;
        public Vector2 instantiatePositionDown = new Vector2(0f, -18f);
        public RoomSpawner leftPassage;
        public Vector2 instantiatePositionLeft = new Vector2(-18f, 0f);
        public RoomSpawner rightPassage;
        public Vector2 instantiatePositionRight = new Vector2(18f, 0f);


        public void RandomizePassages() //Рандомизация проходов
        {
            //Проверка кол-ва проходов
            if (passagesCountMin <= 0) passagesCountMin = 1;
            if (passagesCountMax > 4) passagesCountMax = 4;

            int passagesCount = Random.Range(passagesCountMin, passagesCountMax + 1);

            for (int i = 0; i < 4 - passagesCount; i++)
            {
                int index = Random.Range(0, 4);
                if (index == 0 && !upPassage.GetStatic()) upPassage.Close();
                if (index == 1 && !downPassage.GetStatic()) downPassage.Close();
                if (index == 2 && !leftPassage.GetStatic()) leftPassage.Close();
                if (index == 3 && !rightPassage.GetStatic()) rightPassage.Close();
            }
        }
        public void SpawnSomething(GameObject something) 
        {
            int randomPlace = Random.Range(0, pointsForSomething.Count);
            Instantiate(something, pointsForSomething[randomPlace].position, Quaternion.identity, pointsForSomething[randomPlace]);
            pointsForSomething.RemoveAt(randomPlace);
        }

        private void Awake()
        {
            if (randomizePassagesOnAwake) RandomizePassages();
        }
    }
}
