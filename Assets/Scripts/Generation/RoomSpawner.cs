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

        //Другие приватные поля
        private RoomSpawnerState state;
        private GameObject room;
        private bool isSpawned = false;

        //Ссылки на другие скрипты
        private GenerationManager generationManager;

        //Публичные методы
        public RoomSpawnerState GetState() => state; 
        public GameObject GetSpawnedRoom() => room; 
        public bool GetIsSpawned() => isSpawned; 
        public void Close(bool isStatic)
        {
            if(!isSpawned && state != RoomSpawnerState.StaticOpen)
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
        public void ForcedClose()
        {
            state = RoomSpawnerState.Close;
            passage.SetActive(false);
            wall.SetActive(true);
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
        public void SpawnRoom()
        {
            if (!generationManager.GetIsSpawned())
            {
                //Если комната не закрыта и еще не заспавнена 
                if (state == RoomSpawnerState.Open && !isSpawned)
                {
                    //Спавн
                    GameObject randomRoom = GetRoom();
                    SetSpawnPointPosition(randomRoom.GetComponent<Room>());

                    room = Instantiate(randomRoom, transform.position, Quaternion.identity, generationManager.transform);
                    Room newRoom = room.GetComponent<Room>();
                    newRoom.spawnPoint = this;

                    //Настраиваем комнату
                    MakeThePassageFriendly(newRoom);
                    newRoom.RandomizePassages();
                    Open(true);
                    generationManager.IncreaseSpawnedRoomsCount();
                    if (generationManager.GetNowSpawnedRoomsCount() >= generationManager.GetAllRoomsCount()) generationManager.SetIsSpawned();

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
                Debug.Log("[Info]: Room has been destroyd");
                generationManager.GetNowSpawnedRooms().Remove(room.GetComponent<Room>());
                ForcedClose();
                generationManager.ReduceSpawnedRoomsCount();
                Destroy(room);
            }
        }

        //Приватные методы для спавна
        private GameObject GetRoom()
        {
            int chance = Random.Range(0, 101);
            Rooms thisRoom = generationManager.GetRoomsMap()[generationManager.GetNowSpawnedRoomsCount()];
            return generationManager.GetRoomsLists.GetRandomRoomInChance(thisRoom, chance, false);
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
            if (!isSpawned && collision.CompareTag("Room") || collision.CompareTag("Spawner")) Close(true);
            if (isSpawned && collision.CompareTag("Spawner") && collision.GetComponent<RoomSpawner>().GetIsSpawned()) DestroyRoom();
        }
    }
}