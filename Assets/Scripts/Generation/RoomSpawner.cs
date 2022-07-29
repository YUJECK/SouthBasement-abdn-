using UnityEngine;

namespace Generation
{
    public class RoomSpawner : MonoBehaviour
    {
        public Room.Directories openingDirection;
        public Vector2 ownInstantiateDifference = new Vector2(0f, 0f);
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
        public void SpawnRoom()
        {
            if (!generationManager.GetIsSpawned())
            {
                //Если комната не закрыта и еще не заспавнена 
                if (!_isClosed && !_isSpawned && !_staticPassage)
                {
                    int chance = Random.Range(0, 101);
                    GameObject randomRoom = null;
                    if (generationManager.roomsMap[generationManager.GetNowSpawnedRoomsCount()] == 0) randomRoom = generationManager.GetRandomRoomInChance(chance);
                    else if(generationManager.roomsMap[generationManager.GetNowSpawnedRoomsCount()] == 1) randomRoom = generationManager.GetRandomNpcRoomInChance(chance);
                    

                    //Определение позиции спавна
                    if (openingDirection == Room.Directories.Up) 
                        spawnPoint.localPosition = randomRoom.GetComponent<Room>().instantiatePositionDown + ownInstantiateDifference;
                    if (openingDirection == Room.Directories.Down) 
                        spawnPoint.localPosition = randomRoom.GetComponent<Room>().instantiatePositionUp + ownInstantiateDifference;
                    if (openingDirection == Room.Directories.Left) 
                        spawnPoint.localPosition = randomRoom.GetComponent<Room>().instantiatePositionRight + ownInstantiateDifference;
                    if (openingDirection == Room.Directories.Right) 
                        spawnPoint.localPosition = randomRoom.GetComponent<Room>().instantiatePositionLeft + ownInstantiateDifference;

                    //Спавн
                    _room = Instantiate(randomRoom, spawnPoint.position, Quaternion.identity);
                    Room newRoom = _room.GetComponent<Room>();
                    newRoom.spawnPoint = this;

                    //Ставим соединяющий проход статичным, чтобы его нельзя было закрыть
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
                    
                    //Рандомизируем проходы в заспавненой комнате и делаем этот проход статичным 
                    newRoom.RandomizePassages();
                    SetPassageToStatic();
                    generationManager.IncreaseSpawnedRoomsCount();
                    if (generationManager.GetNowSpawnedRoomsCount() >= generationManager.GetRoomsCount()) generationManager.SetIsSpawned();
                    _isSpawned = true;
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
            Invoke("SpawnRoom", Random.Range(0.05f, 0.3f));
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_isSpawned && collision.CompareTag("Room")) Close();
            if (collision.CompareTag("Spawner") && !_isSpawned) Close();
        }
    }
}