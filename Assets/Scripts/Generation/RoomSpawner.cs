using UnityEngine;

namespace Generation
{
    public class RoomSpawner : MonoBehaviour
    {
        public Room.Directories openingDirection;
        public Vector2 ownInstantiateDifference = new Vector2(0f, 0f); //Оклонение относительно центра
        [SerializeField] private bool isStartSpawnPoint = false;
        [SerializeField] private GameObject passage; //Проход
        [SerializeField] private GameObject wall; //Закрывающая стенка

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

        //Публичные методы
        public void SpawnRoom()
        {
            if (!generationManager.GetIsSpawned())
            {
                //Если комната не закрыта и еще не заспавнена 
                if (!_isClosed && !_isSpawned && !_staticPassage)
                {
                    //Спавн
                    GameObject randomRoom = GetRoom();
                    SetSpawnPointPosition(randomRoom.GetComponent<Room>());

                    _room = Instantiate(randomRoom, transform.position, Quaternion.identity);
                    Room newRoom = _room.GetComponent<Room>();
                    newRoom.spawnPoint = this;

                    //Настраиваем комнату
                    MakeThePassageFriendly(newRoom);
                    newRoom.RandomizePassages();
                    SetPassageToStatic();
                    generationManager.IncreaseSpawnedRoomsCount();
                    if (generationManager.GetNowSpawnedRoomsCount() >= generationManager.GetAllRoomsCount()) generationManager.SetIsSpawned();

                    generationManager.AddRoom(newRoom);
                    _isSpawned = true;
                }
            }
            else Close();
        }
        public void RegenerateRoom()
        {
        }
        public void DestroyRoom()
        {
            Debug.Log("Room tryied to destroy");
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
        public void SetPassageToStatic()
        {
            _staticPassage = true;
            if (_isClosed) Open();
            if (!isStartSpawnPoint) Destroy(gameObject);
        }
        public bool GetStatic() { return _staticPassage; }

        //Приватные методы для спавна
        private GameObject GetRoom()
        {
            int chance = Random.Range(0, 101);
            GameObject randomRoom = null;
            RoomsLists.Rooms thisRoom = generationManager.roomsMap[generationManager.GetNowSpawnedRoomsCount()];
            switch (thisRoom)
            {
                case RoomsLists.Rooms.Default:
                    randomRoom = generationManager.roomsLists.GetRandomRoomInChance(chance, false);
                    break;
                case RoomsLists.Rooms.NPC:
                    randomRoom = generationManager.roomsLists.GetRandomNpcRoomInChance(chance, false);
                    break;
                case RoomsLists.Rooms.Trader:
                    randomRoom = generationManager.roomsLists.GetRandomTraderRoomInChance(chance, false);
                    break;
                case RoomsLists.Rooms.Box:
                    randomRoom = generationManager.roomsLists.GetRandomBoxRoomInChance(chance, false);
                    break;
                case RoomsLists.Rooms.MustSpawn:
                    randomRoom = generationManager.roomsLists.GetRandomMustSpawnRoomInChance(chance, true);
                    break;
                case RoomsLists.Rooms.Exit:
                    randomRoom = generationManager.roomsLists.GetRandomExitRoomInChance(chance, false);
                    break;
            }
            return randomRoom;
        }
        private void SetSpawnPointPosition(Room room)
        {
            //Определение позиции спавна
            if (openingDirection == Room.Directories.Up)
                transform.localPosition = room.GetInstantiatePosition(Room.Directories.Down) + ownInstantiateDifference;
            if (openingDirection == Room.Directories.Down)
                transform.localPosition = room.GetInstantiatePosition(Room.Directories.Up) + ownInstantiateDifference;
            if (openingDirection == Room.Directories.Left)
                transform.localPosition = room.GetInstantiatePosition(Room.Directories.Right) + ownInstantiateDifference;
            if (openingDirection == Room.Directories.Right)
                transform.localPosition = room.GetInstantiatePosition(Room.Directories.Left) + ownInstantiateDifference;
        }
        private void MakeThePassageFriendly(Room room)
        {
            //Ставим соединяющий проход статичным, чтобы его нельзя было закрыть
            switch (openingDirection)
            {
                case Room.Directories.Up:
                    room.passagesMustSpawned.Add(Room.Directories.Down);
                    room.GetPassage(Room.Directories.Down).SetPassageToStatic();
                    break;
                case Room.Directories.Down:
                    room.passagesMustSpawned.Add(Room.Directories.Up);
                    room.GetPassage(Room.Directories.Up).SetPassageToStatic();
                    break;
                case Room.Directories.Left:
                    room.passagesMustSpawned.Add(Room.Directories.Right);
                    room.GetPassage(Room.Directories.Right).SetPassageToStatic();
                    break;
                case Room.Directories.Right:
                    room.passagesMustSpawned.Add(Room.Directories.Left);
                    room.GetPassage(Room.Directories.Left).SetPassageToStatic();
                    break;
            }
        }


        //Юнитивские методы
        private void Start()
        {
            generationManager = FindObjectOfType<GenerationManager>();
            Invoke("SpawnRoom", Random.Range(0.05f, 0.3f));
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_isSpawned && collision.CompareTag("Room") || collision.CompareTag("Spawner")) Close();
            if (_isSpawned && collision.CompareTag("Spawner") && collision.GetComponent<RoomSpawner>().isSpawned) DestroyRoom();
        }
    }
}