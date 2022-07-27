using UnityEngine;

namespace Generation
{
    public class RoomSpawner : MonoBehaviour
    {
        public Room.Directories openingDirection;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject passage;
        [SerializeField] private GameObject wall;

        //Геттеры
        public bool isClosed
        {
            get { return _isClosed; }
            private set => _isClosed = value;
        }
        public bool isSpawned
        {
            get { return _isSpawned; }
            private set => _isSpawned = value;
        }
        public GameObject room
        {
            get { return _room; }
            private set => _room = value;
        }

        //Другие приватные поля
        private bool _staticPassage = false;
        private bool _isClosed = false;
        private bool _isSpawned = false;
        private GameObject _room;
        private GameObject friendlyPassage;

        //Ссылки на другие скрипты
        private GenerationManager generationManager;

        public void SetPassageToStatic()
        {
            _staticPassage = true;
            if (_isClosed) Open();
        }
        public bool GetStatic() { return _staticPassage; }
        public void DestroyRoom()
        {
            if (_room != null)
            {
                Debug.Log("Room has been destroyd");
                Destroy(_room);
                Close();
            }
        }
        public void Close()
        {
            if (!_isClosed && !_staticPassage)
            {
                if (_staticPassage) Debug.Log("Static closed");
                _isClosed = true;
                passage.SetActive(false);
                wall.SetActive(true);
            }
        }
        public void Open()
        {
            if (_isClosed)
            {
                _isClosed = false;
                passage.SetActive(true);
                wall.SetActive(false);
            }
        }
        public void SpawnRoom()
        {
            if (generationManager.nowSpawnedRoomsCount < generationManager.roomsCount)
            {
                //Если комната не закрыта и еще не заспавнена 
                if (!_isClosed && !_isSpawned && !_staticPassage)
                {
                    _room = Instantiate(generationManager.rooms[Random.Range(0, generationManager.rooms.Count)]
                        , spawnPoint.position, Quaternion.identity);
                    generationManager.nowSpawnedRoomsCount++;
                    Room newRoom = _room.GetComponent<Room>();
                    newRoom.spawnPoint = this;

                    switch (openingDirection)
                    {
                        case Room.Directories.Up:
                            newRoom.passagesMustSpawned.Add(Room.Directories.Down);
                            newRoom.downPassage.SetPassageToStatic();
                            break;
                        case Room.Directories.Down:
                            newRoom.passagesMustSpawned.Add(Room.Directories.Up);
                            newRoom.upPassage.SetPassageToStatic();
                            break;
                        case Room.Directories.Left:
                            newRoom.passagesMustSpawned.Add(Room.Directories.Right);
                            newRoom.rightPassage.SetPassageToStatic();
                            break;
                        case Room.Directories.Right:
                            newRoom.passagesMustSpawned.Add(Room.Directories.Left);
                            newRoom.leftPassage.SetPassageToStatic();
                            break;
                    }
                    newRoom.RandomizePassages();
                }
            }
            else Close();
        }
        public void RegenerateRoom()
        {
        }

        //Юнитивские методы
        private void Start()
        {
            generationManager = FindObjectOfType<GenerationManager>();
            SpawnRoom();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isSpawned && collision.CompareTag("Room"))
            {
                Debug.Log("Enter " + collision.name);
                if (collision.gameObject != room && !collision.GetComponent<Room>().isStartRoom)
                    collision.GetComponent<Room>().spawnPoint.DestroyRoom();
            }
        }
    }
}