using UnityEngine;
using UnityEngine.Events;

namespace Generation
{
    public class RoomSpawner : MonoBehaviour
    {
        public Room.Directions openingDirection;
        public Vector2 ownInstantiateDifference = new Vector2(0f, 0f); //Оклонение относительно центра
        [SerializeField] private bool isStartSpawnPoint = false;
        [SerializeField] private GameObject passage; //Проход
        [SerializeField] private GameObject wall; //Закрывающая стенка

        //Другие приватные поля
        private RoomSpawnerState state;
        private GameObject room;
        private bool isSpawned = false;

        //Ивенты
        [Header("События")]
        public UnityEvent onSpawn = new UnityEvent();
        public UnityEvent onClose = new UnityEvent();
        public UnityEvent onOpen = new UnityEvent();

        //Ссылки на другие скрипты
        private GenerationManager generationManager;

        //Геттеры, сеттеры
        public RoomSpawnerState State => state; 
        public GameObject SpawnedRoom => room; 
        public bool IsSpawned => isSpawned; 
        public bool Close(bool isStatic)
        {
            if(!isSpawned && state != RoomSpawnerState.StaticOpen)
            {
                if(isStatic)
                {                 
                    state = RoomSpawnerState.StaticClose;
                    passage.SetActive(false);
                    wall.SetActive(true);
                    if (!isStartSpawnPoint) Destroy(gameObject);
                    return true;
                }
                if (state == RoomSpawnerState.Open)
                {
                    state = RoomSpawnerState.Close;
                    passage.SetActive(false);
                    wall.SetActive(true);
                    return true;
                }
                return false;
            }
            return false;
        }
        public void ForcedClose()
        {
            state = RoomSpawnerState.Close;
            passage.SetActive(false);
            wall.SetActive(true);
        }
        public bool Open(bool isStatic)
        {
            if(isStatic && state != RoomSpawnerState.StaticClose)
            {
                state = RoomSpawnerState.StaticOpen;
                passage.SetActive(true);
                wall.SetActive(false);
                return true;
            }
            if (state == RoomSpawnerState.Close)
            {
                state = RoomSpawnerState.Open;
                passage.SetActive(true);
                wall.SetActive(false);
                return true;
            }
            return false;
        }

        //Методы спавна
        public void SpawnRoom()
        {
            if (!generationManager.IsSpawned)
            {
                //Если комната не закрыта и еще не заспавнена 
                if (state == RoomSpawnerState.Open && !isSpawned)
                {
                    //Спавн
                    GameObject randomRoom = GetRoom();
                    SetSpawnPointPosition(randomRoom.GetComponent<Room>());

                    room = Instantiate(randomRoom, transform.position, Quaternion.identity, generationManager.transform);
                    Room newRoom = room.GetComponent<Room>();
                    newRoom.SetStartingSpawnPoint(this);

                    //Настраиваем комнату
                    MakeThePassageFriendly(newRoom);
                    newRoom.RandomizePassages();
                    Open(true);
                    generationManager.IncreaseSpawnedRoomsCount();
                    if (generationManager.NowSpawnedRoomsCount >= generationManager.AllRoomsCount) generationManager.SetIsSpawned();

                    generationManager.AddRoom(newRoom);
                    isSpawned = true;
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
            if (room != null)
            {
                Debug.Log("[Info]: Room has been destroyed");
                generationManager.NowSpawnedRooms.Remove(room.GetComponent<Room>());
                ForcedClose();
                generationManager.ReduceSpawnedRoomsCount();
                Destroy(room);
            }
        }
        private GameObject GetRoom()
        {
            int chance = Random.Range(0, 101);
            Rooms thisRoom = generationManager.RoomsMap[generationManager.NowSpawnedRoomsCount];
            return generationManager.RoomsLists.GetRandomRoomInChance(thisRoom, chance, false);
        }
        private void SetSpawnPointPosition(Room room) => transform.localPosition = room.GetInstantiatePosition(GetOppositeDirection(openingDirection)) + ownInstantiateDifference;
        private void MakeThePassageFriendly(Room room)
        {
            //Ставим соединяющий проход статичным, чтобы его нельзя было закрыть
            switch (openingDirection)
            {
                case Room.Directions.Up:
                    room.GetPassage(Room.Directions.Down).Open(true);
                    break;
                case Room.Directions.Down:
                    room.GetPassage(Room.Directions.Up).Open(true);
                    break;
                case Room.Directions.Left:
                    room.GetPassage(Room.Directions.Right).Open(true);
                    break;
                case Room.Directions.Right:
                    room.GetPassage(Room.Directions.Left).Open(true);
                    break;
            }
        }

        //Другое
        public Room.Directions GetOppositeDirection(Room.Directions direction)
        {
            switch (direction)
            {
                case Room.Directions.Up:
                    return Room.Directions.Down;
                case Room.Directions.Down:
                    return Room.Directions.Up;
                case Room.Directions.Left:
                    return Room.Directions.Right;
                case Room.Directions.Right:
                    return Room.Directions.Left;
            }
            return Room.Directions.Up;
        }

        //Юнитивские методы
        private void Start()
        {
            generationManager = FindObjectOfType<GenerationManager>();
            Invoke("SpawnRoom", Random.Range(0.05f, 0.3f));
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isSpawned && collision.CompareTag("Room")) Close(true);
            else if (isSpawned && collision.CompareTag("Spawner") && collision.GetComponent<RoomSpawner>().IsSpawned) DestroyRoom();
        }
    }
}