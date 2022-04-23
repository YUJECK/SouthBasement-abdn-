using UnityEngine;
using System.Collections.Generic;
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
        if (generationManager.NowSpawnedRooms < generationManager.RoomsCount)
        {
            //Если комната не закрыта и уже не заспавнена 
            if (!isNextRoomSpawn && !isAnotherRoomSpawned & !isClose)
            {

                if(generationManager.rooms[generationManager.NowSpawnedRooms] == 1)
                {
                    if (openingDirection == Directories.Down) SpawnRoom(generationManager.topNPC);
                    else if (openingDirection == Directories.Up) SpawnRoom(generationManager.downNPC);
                    else if (openingDirection == Directories.Left) SpawnRoom(generationManager.rightNPC);
                    else if (openingDirection == Directories.Right) SpawnRoom(generationManager.leftNPC);
                    
                    generationManager.NowSpawnedNPCsRooms++;
                }
                else
                {
                    if (openingDirection == Directories.Down) SpawnRoom(generationManager.top);
                    else if (openingDirection == Directories.Up) SpawnRoom(generationManager.down);
                    else if (openingDirection == Directories.Left) SpawnRoom(generationManager.right);
                    else if (openingDirection == Directories.Right) SpawnRoom(generationManager.left);
                    
                    generationManager.NowSpawnedSimpleRooms++;
                }
                
                generationManager.NowSpawnedRooms++;
                isNextRoomSpawn = true;
                isConnectedRoomSpawned = true;
            }
        }
        else Close();
        isFunctionStart = true;
    }

    private void SpawnRoom(GameObject[] roomsList) //Спавн комнаты
    {
        rand = Random.Range(0, roomsList.Length);
        room = Instantiate(roomsList[rand], transform.position, transform.rotation);
    }
    public void Close()
    {
        if(walls != null)
        {
            walls.Close();
            isClose = true; 
            isOpen = false;
        }
    }
    public void Open()
    {
        if(walls != null)
        {
            walls.Open();
            isClose = false;
            isOpen = true;
        }
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
            Close();
        }
    }

    private void Update()
    {
        if(isConnectedRoomSpawned && isClose) Open();
        
        if (!isNextRoomSpawn && isFunctionStart && room == null && !isConnectedRoomSpawned && !isClose)
        {
            if (!isAnyObjOnTrigger || isAnotherRoomSpawned)
                Close();
        }
    }
    public enum Directories { None, Down, Up, Left, Right}
}
