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

        public bool isStartRoom { get => isStartRoom; set => isStartRoom = value; }
        [SerializeField] private bool _isStartRoom = false;
        public int passagesCountMin = 2;
        public int passagesCountMax = 3;

        [Header("Проходы")]
        public List<Directories> passagesMustSpawned = new List<Directories>();
        [SerializeField] private bool randomizePassagesOnAwake = false;
        public RoomSpawner spawnPoint;
        public RoomSpawner upPassage;
        public RoomSpawner downPassage;
        public RoomSpawner leftPassage;
        public RoomSpawner rightPassage;


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

        private void Awake()
        {
            if (randomizePassagesOnAwake) RandomizePassages();
            //Это надо удалить
            gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 356), Random.Range(0, 356), Random.Range(0, 356));
        }
    }
}
