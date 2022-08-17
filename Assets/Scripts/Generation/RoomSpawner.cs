using UnityEngine;
using UnityEngine.Events;

namespace Generation
{
    public class RoomSpawner : MonoBehaviour
    {
        public RoomTemplate.Directions openingDirection; //Направление
        [SerializeField] private bool isStartSpawnPoint = false;
        [SerializeField] private RoomTemplate ownRoom; //Комната в которой находится этот проход
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
        public RoomTemplate OwnRoom => ownRoom; //Эта комната
        public bool IsSpawned => isSpawned; //Заспавнена ли
        public void SetOwnRoom(RoomTemplate ownRoom) => this.ownRoom = ownRoom;
        public bool Close(bool isStatic) //Закрыть
        {
            if (!isSpawned && state != RoomSpawnerState.StaticOpen)
            {
                if (isStatic)
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
            if (isStatic && state != RoomSpawnerState.StaticClose)
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
        private Vector2 GetSpawnPosition(RoomTemplate otherRoom) => Utility.InvertVector2(otherRoom.GetInstantiatePosition(ManagerList.GenerationManager.GetOppositeDirection(openingDirection))) + ownRoom.GetInstantiatePosition(openingDirection);

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
                    transform.localPosition = GetSpawnPosition(randomRoom.GetComponent<RoomTemplate>());

                    spawnedRoom = Instantiate(randomRoom, transform.position, Quaternion.identity, ManagerList.GenerationManager.transform);
                    RoomTemplate newRoom = spawnedRoom.GetComponent<RoomTemplate>();
                    newRoom.SetStartingSpawnPoint(this);

                    //Настраиваем комнату
                    MakeThePassageFriendly(newRoom);
                    Open(true);
                    ManagerList.GenerationManager.AddRoomToList(newRoom);
                    if (ownRoom != null) ownRoom.AddSpawnedRoom(newRoom);
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
                ManagerList.GenerationManager.RemoveRoomFromList(spawnedRoom.GetComponent<RoomTemplate>());
                ForcedClose();
                Destroy(spawnedRoom);
            }
        }
        private void MakeThePassageFriendly(RoomTemplate room) //Сделать какой-то проход открытым
        {
            //Ставим соединяющий проход статичным, чтобы его нельзя было закрыть
            switch (openingDirection)
            {
                case RoomTemplate.Directions.Up:
                    room.GetPassage(RoomTemplate.Directions.Down).Open(true);
                    break;
                case RoomTemplate.Directions.Down:
                    room.GetPassage(RoomTemplate.Directions.Up).Open(true);
                    break;
                case RoomTemplate.Directions.Left:
                    room.GetPassage(RoomTemplate.Directions.Right).Open(true);
                    break;
                case RoomTemplate.Directions.Right:
                    room.GetPassage(RoomTemplate.Directions.Left).Open(true);
                    break;
            }
        }

        //Юнитивские методы
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(state == RoomSpawnerState.Open || state == RoomSpawnerState.StaticClose)
            {
                if (!isSpawned && collision.CompareTag("Room")) ForcedClose();
                else if (isSpawned && collision.CompareTag("Spawner") && collision.GetComponent<RoomSpawner>().IsSpawned) DestroyRoom();
                else if (!isSpawned && collision.CompareTag("Spawner") && collision.GetComponent<RoomSpawner>().IsSpawned) Close(true);
                else if (!isSpawned && collision.CompareTag("Spawner") && !collision.GetComponent<RoomSpawner>().IsSpawned) Close(true);
            }
        }
    }
}