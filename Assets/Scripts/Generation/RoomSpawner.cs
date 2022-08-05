using UnityEngine;

namespace Generation
{
    public class RoomSpawner : MonoBehaviour
    {
        public Room.Directories openingDirection;
        private RoomSpawnerState state;
        public Vector2 ownInstantiateDifference = new Vector2(0f, 0f); //Оклонение относительно центра
        [SerializeField] private bool isStartSpawnPoint = false;
        [SerializeField] private GameObject passage; //Проход
        [SerializeField] private GameObject wall; //Закрывающая стенка

        //Геттеры
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
        private GameObject _room;
        private GameObject friendlyPassage;
        private bool _isSpawned = false;

        //Ссылки на другие скрипты
        private GenerationManager generationManager;

        //Публичные методы
        public void SpawnRoom()
        {
            if (!generationManager.GetIsSpawned())
            {
                //Если комната не закрыта и еще не заспавнена 
                if (state == RoomSpawnerState.Open && !_isSpawned)
                {
                    //Спавн
                    GameObject randomRoom = GetRoom();
                    SetSpawnPointPosition(randomRoom.GetComponent<Room>());

                    _room = Instantiate(randomRoom, transform.position, Quaternion.identity, generationManager.transform);
                    Room newRoom = _room.GetComponent<Room>();
                    newRoom.spawnPoint = this;

                    //Настраиваем комнату
                    MakeThePassageFriendly(newRoom);
                    newRoom.RandomizePassages();
                    Open(true);
                    generationManager.IncreaseSpawnedRoomsCount();
                    if (generationManager.GetNowSpawnedRoomsCount() >= generationManager.GetAllRoomsCount()) generationManager.SetIsSpawned();

                    generationManager.AddRoom(newRoom);
                    _isSpawned = true;
                }
            }
            else Close(true);
        }
        public void RegenerateRoom()
        {
            DestroyRoom();

            SpawnRoom();
        }
        public void DestroyRoom()
        {
            Debug.Log("Room tryied to destroy");
            if (_room != null)
            {
                Debug.Log("Room has been destroyd");
                Destroy(_room);
                Close(true);
                generationManager.ReduceSpawnedRoomsCount();
            }
        }
        public void Close(bool isStatic)
        {
            if(!_isSpawned && state != RoomSpawnerState.StaticOpen)
            {
                if(isStatic)
                {                 
                    state = RoomSpawnerState.StaticClose;
                    passage.SetActive(false);
                    wall.SetActive(true);
                    if (!isStartSpawnPoint) Destroy(gameObject);
                    return;
                }
                if (state == RoomSpawnerState.Open)
                {
                    state = RoomSpawnerState.Close;
                    passage.SetActive(false);
                    wall.SetActive(true);
                }
            }
        }
        public void Open(bool isStatic)
        {
            if(isStatic && state != RoomSpawnerState.StaticClose)
            {
                state = RoomSpawnerState.StaticOpen;
                passage.SetActive(true);
                wall.SetActive(false);
                return;
            }
            if (state == RoomSpawnerState.Close)
            {
                state = RoomSpawnerState.Open;
                passage.SetActive(true);
                wall.SetActive(false);
            }
        }
        public RoomSpawnerState GetState() { return state; }

        //Приватные методы для спавна
        private GameObject GetRoom()
        {
            int chance = Random.Range(0, 101);
            Rooms thisRoom = generationManager.roomsMap[generationManager.GetNowSpawnedRoomsCount()];
            return generationManager.roomsLists.GetRandomRoomInChance(thisRoom, chance, false);
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
                    room.GetPassage(Room.Directories.Down).Open(true);
                    break;
                case Room.Directories.Down:
                    room.passagesMustSpawned.Add(Room.Directories.Up);
                    room.GetPassage(Room.Directories.Up).Open(true);
                    break;
                case Room.Directories.Left:
                    room.passagesMustSpawned.Add(Room.Directories.Right);
                    room.GetPassage(Room.Directories.Right).Open(true);
                    break;
                case Room.Directories.Right:
                    room.passagesMustSpawned.Add(Room.Directories.Left);
                    room.GetPassage(Room.Directories.Left).Open(true);
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
            if (!_isSpawned && collision.CompareTag("Room") || collision.CompareTag("Spawner")) Close(true);
            if (_isSpawned && collision.CompareTag("Spawner") && collision.GetComponent<RoomSpawner>().isSpawned) DestroyRoom();
        }
    }
}