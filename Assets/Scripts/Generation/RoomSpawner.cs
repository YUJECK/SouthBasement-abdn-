using UnityEngine;
using UnityEngine.Events;

namespace Generation
{
    public class RoomSpawner : MonoBehaviour
    {
        public Room.Directions openingDirection; //Направление
        public Vector2 ownInstantiateDifference = new Vector2(0f, 0f); //Оклонение относительно центра
        [SerializeField] private bool isStartSpawnPoint = false;
        [SerializeField] private Room ownRoom; //Комната в которой находится этот проход
        [SerializeField] private GameObject passage; //Проход
        [SerializeField] private GameObject wall; //Закрывающая стенка

        //Другие приватные поля
        private RoomSpawnerState state; //Состояние прохода
        private GameObject spawnedRoom; //Заспавненная комната
        private bool isSpawned = false; //Заспавнена ли

        //Ивенты
        [Header("События")]
        public UnityEvent onSpawn = new UnityEvent(); //При вызове SpawnRoom()
        public UnityEvent onClose = new UnityEvent(); //При закрытии 
        public UnityEvent onOpen = new UnityEvent(); //При открытии

        //Геттеры, сеттеры
        public RoomSpawnerState State => state; //Состояние
        public GameObject SpawnedRoom => spawnedRoom; //Заспавненная комната
        public Room OwnRoom => ownRoom; //Эта комната
        public bool IsSpawned => isSpawned; //Заспавнена ли
        public void SetOwnRoom(Room ownRoom) => this.ownRoom = ownRoom; 
        public bool Close(bool isStatic) //Закрыть
        {
            if(!isSpawned && state != RoomSpawnerState.StaticOpen)
            {
                if(isStatic)
                {                 
                    state = RoomSpawnerState.StaticClose;
                    passage.SetActive(false);
                    wall.SetActive(true);
                    onClose.Invoke();
                    if (!isStartSpawnPoint) Destroy(gameObject);
                    return true;
                }
                if (state == RoomSpawnerState.Open)
                {
                    state = RoomSpawnerState.Close;
                    passage.SetActive(false);
                    wall.SetActive(true);
                    onClose.Invoke();
                    return true;
                }
                return false;
            }
            return false;
        }
        public void ForcedClose() //Мгновенное закрытие
        {
            state = RoomSpawnerState.Close;
            if (passage != null) passage.SetActive(false);
            if (wall != null) wall.SetActive(true);
        }
        public bool Open(bool isStatic) //Открыть
        {
            if(isStatic && state != RoomSpawnerState.StaticClose)
            {
                state = RoomSpawnerState.StaticOpen;
                passage.SetActive(true);
                wall.SetActive(false);
                onOpen.Invoke();
                return true;
            }
            if (state == RoomSpawnerState.Close)
            {
                state = RoomSpawnerState.Open;
                passage.SetActive(true);
                wall.SetActive(false);
                onOpen.Invoke();
                return true;
            }
            return false;
        }
        private GameObject GetRoom() //Получить комнату
        {
            int chance = PlayerStats.GenerateChance();
            Rooms thisRoom = ManagerList.GenerationManager.RoomsMap[ManagerList.GenerationManager.NowSpawnedRoomsCount];
            return ManagerList.GenerationManager.RoomsLists.GetRandomRoomInChance(thisRoom, chance, false);
        }
        private void SetSpawnPointPosition(Room room) => transform.localPosition = room.GetInstantiatePosition(ManagerList.GenerationManager.GetOppositeDirection(openingDirection)) + ownInstantiateDifference;

        //Методы спавна
        public void StartSpawnningRoom(float offset = 0.1f) => Invoke("SpawnRoom", offset); //Начать спавн
        private void SpawnRoom() //Спавн комнаты
        {
            if (!ManagerList.GenerationManager.IsSpawned)
            {
                //Если комната не закрыта и еще не заспавнена 
                if (state == RoomSpawnerState.Open && !isSpawned)
                {
                    //Спавн
                    GameObject randomRoom = GetRoom();
                    SetSpawnPointPosition(randomRoom.GetComponent<Room>());

                    spawnedRoom = Instantiate(randomRoom, transform.position, Quaternion.identity, ManagerList.GenerationManager.transform);
                    Room newRoom = spawnedRoom.GetComponent<Room>();
                    newRoom.SetStartingSpawnPoint(this);

                    //Настраиваем комнату
                    MakeThePassageFriendly(newRoom);
                    //newRoom.RandomizePassages();
                    Open(true);
                    ManagerList.GenerationManager.AddRoomToList(newRoom);
                    if(ownRoom != null) ownRoom.AddSpawnedRoom(newRoom);
                    isSpawned = true;

                    if (ManagerList.GenerationManager.NowSpawnedRoomsCount >= ManagerList.GenerationManager.RoomsMap.Length) ManagerList.GenerationManager.SetIsSpawned();
                }
            }
            else Close(true);
            onSpawn.Invoke();
        }
        public void RegenerateRoom() //Заново заспавнить комнату
        {
            DestroyRoom();

            SpawnRoom();
        }
        public void DestroyRoom() //Уничтожить заспавненную комнату
        {
            if (spawnedRoom != null)
            {
                Debug.Log("[Info]: Room has been destroyed");
                ManagerList.GenerationManager.RemoveRoomFromList(spawnedRoom.GetComponent<Room>());
                ForcedClose();
                Destroy(spawnedRoom);
            }
        }
        private void MakeThePassageFriendly(Room room) //Сделать какой-то проход открытым
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

        //Юнитивские методы
        private void OnTriggerEnter2D(Collider2D collision) 
        {
            if (!isSpawned && collision.CompareTag("Room")) Close(true);
            else if (isSpawned && collision.CompareTag("Spawner") && collision.GetComponent<RoomSpawner>().IsSpawned) DestroyRoom();
            else if (!isSpawned && collision.CompareTag("Spawner") && collision.GetComponent<RoomSpawner>().IsSpawned) Close(true);
            else if (!isSpawned && collision.CompareTag("Spawner") && !collision.GetComponent<RoomSpawner>().IsSpawned) Close(true);
        }
    }
}