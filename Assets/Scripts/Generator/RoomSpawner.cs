using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject room;
    public RoomGenerationManager generationManager;
    public CloseRoom walls;
    public RoomInfo RoomInfoObj;
    public Directories openingDirection;
    int rand;
    public bool StartRoom = false;
    public bool isNextRoomSpawn = false;
    public bool isFunctionStart = false;
    public bool isChecked = false;
    public bool isAnotherRoomSpawned = false; //Если просто другая комната
    public bool isConnectedRoomSpawned = false; //Если с комнатой можно соединиться
    public bool isClose = false;
    public bool isOpen = true;
    public bool isAnyObjOnTrigger = false;

    private void Start()
    {
        //Определяем все переменные
        generationManager = FindObjectOfType<RoomGenerationManager>();
        walls = GetComponent<CloseRoom>();

        //Запускаем спавн комнаты
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (generationManager.NowSpawnedRooms <= generationManager.RoomsCount)
        {
            //Если комната не закрыта и уже не заспавнена 
            if (!isNextRoomSpawn & !isAnotherRoomSpawned & !isClose)
            {
                int isNPCRoomWillBeSpawned = Random.Range(0,2);

                if(isNPCRoomWillBeSpawned == 0 & generationManager.NowSpawnedNPCsRooms <= generationManager.NPCsRoomsCount)
                {
                    if (openingDirection == Directories.Down)
                    {
                        rand = Random.Range(0, generationManager.topNPC.Length);
                        room = Instantiate(generationManager.topNPC[rand], transform.position, transform.rotation/* , ParentObject.transform */);
                    }
                    else if (openingDirection == Directories.Up)
                    {
                        rand = Random.Range(0, generationManager.downNPC.Length);
                        room = Instantiate(generationManager.downNPC[rand], transform.position, transform.rotation/* , ParentObject.transform */);
                    }
                    else if (openingDirection == Directories.Left)
                    {
                        rand = Random.Range(0, generationManager.rightNPC.Length);
                        room = Instantiate(generationManager.rightNPC[rand], transform.position, transform.rotation/* , ParentObject.transform */);
                    }
                    else if (openingDirection == Directories.Right)
                    {
                        rand = Random.Range(0, generationManager.leftNPC.Length);
                        room = Instantiate(generationManager.leftNPC[rand], transform.position, transform.rotation/* , ParentObject.transform */);
                    }
                    
                    generationManager.NowSpawnedNPCsRooms++;
                }
                else
                {
                    if (openingDirection == Directories.Down)
                    {
                        rand = Random.Range(0, generationManager.top.Length);
                        room = Instantiate(generationManager.top[rand], transform.position, transform.rotation/* , ParentObject.transform */);
                    }
                    else if (openingDirection == Directories.Up)
                    {
                        rand = Random.Range(0, generationManager.down.Length);
                        room = Instantiate(generationManager.down[rand], transform.position, transform.rotation/* , ParentObject.transform */);
                    }
                    else if (openingDirection == Directories.Left)
                    {
                        rand = Random.Range(0, generationManager.right.Length);
                        room = Instantiate(generationManager.right[rand], transform.position, transform.rotation/* , ParentObject.transform */);
                    }
                    else if (openingDirection == Directories.Right)
                    {
                        rand = Random.Range(0, generationManager.left.Length);
                        room = Instantiate(generationManager.left[rand], transform.position, transform.rotation/* , ParentObject.transform */);
                    }
                    generationManager.NowSpawnedSimpleRooms++;
                }
                
                generationManager.NowSpawnedRooms++;
                isNextRoomSpawn = true;
                isConnectedRoomSpawned = true;
            }
        }
        isFunctionStart = true;
    }

    private void OnTriggerEnter2D(Collider2D otherRoom)
    {
        if (otherRoom.tag == "Spawner")
        {
            isAnyObjOnTrigger = true;
            if (!isConnectedRoomSpawned)
                isAnotherRoomSpawned = true;
        }
        if(otherRoom.tag == "RoomInfo")
        {
            isAnyObjOnTrigger = true;
            RoomInfo anotherRoom = otherRoom.gameObject.GetComponent<RoomInfo>();
            isChecked = true;

            if (openingDirection == Directories.Down & anotherRoom.direction_2 == Directories.Up)
            { isConnectedRoomSpawned = true;
              isAnotherRoomSpawned = true; }

            else if (openingDirection == Directories.Up & anotherRoom.direction_1 == Directories.Down)
            { isConnectedRoomSpawned = true;
              isAnotherRoomSpawned = true; }

            else if (openingDirection == Directories.Right & anotherRoom.direction_3 == Directories.Left)
            { isConnectedRoomSpawned = true;
              isAnotherRoomSpawned = true; }

            else if (openingDirection == Directories.Left & anotherRoom.direction_4 == Directories.Right)
            { isConnectedRoomSpawned = true;
              isAnotherRoomSpawned = true; }

            if (anotherRoom.isStartRoom)
                isConnectedRoomSpawned = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherRoom)
    {
        if (otherRoom.tag != "Player")
        {
            isAnyObjOnTrigger = false;

            if(walls != null)
            {
                walls.Close();
                isClose = true;
                isOpen = false;
            }
        }
    }

    private void Update()
    {
        if(isConnectedRoomSpawned && isClose)
        {
            if(walls!=null)
            {
                walls.Open();
                isClose = false;
                isOpen = true;
            }
        }
        if (!isNextRoomSpawn & isFunctionStart & room == null & walls != null & !isConnectedRoomSpawned & !isClose)
        {
            if (!isAnyObjOnTrigger || isAnotherRoomSpawned)
            {
                walls.Close();
                isClose = true;
                isOpen = false;
            }
        }
    }
    public enum Directories
    {
        None,
        Down,
        Up,
        Left,
        Right
    }
}
