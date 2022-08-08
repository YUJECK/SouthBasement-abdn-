using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generation;


public class GenerationTransitionManager : MonoBehaviour
{
    public class RoomInformation
    {
        public RoomObject roomObject;
        public string locationName = "Basement";
    }

    [SerializeField] private List<RoomInformation> nextSimpleRooms = new List<RoomInformation>();
    [SerializeField] private List<RoomInformation> nextNpcRooms = new List<RoomInformation>();
    [SerializeField] private List<RoomInformation> nextTraderRooms = new List<RoomInformation>();
    [SerializeField] private List<RoomInformation> nextBoxRooms = new List<RoomInformation>();
    [SerializeField] private List<RoomInformation> nexteMustSpawnRooms = new List<RoomInformation>();
    [SerializeField] private List<RoomInformation> nexteExitRooms = new List<RoomInformation>();

    public void AddRoom(Rooms roomType, RoomInformation roomInformation)
    {
        switch (roomType)
        {
            case Rooms.Default:
                nextSimpleRooms.Add(roomInformation);
                break;
            case Rooms.NPC:
                nextNpcRooms.Add(roomInformation);
                break;
            case Rooms.Trader:
                nextTraderRooms.Add(roomInformation);
                break;
            case Rooms.Box:
                nextBoxRooms.Add(roomInformation);
                break;
            case Rooms.MustSpawn:
                nexteMustSpawnRooms.Add(roomInformation);
                break;
            case Rooms.Exit:
                nexteExitRooms.Add(roomInformation);
                break;
        }
    }

    private void OnLevelWasLoaded()
    {
        GenerationManager newGenerationManager = FindObjectOfType<GenerationManager>();

        FindObjectOfType<RatConsole>().DisplayText("Enter to the " + newGenerationManager.LocationName, Color.green, RatConsole.Mode.ConsoleMessege, "[YUJECKMessege]");

        //Добавление комнат при переходе на следующий уровень
        foreach (RoomInformation room in nextSimpleRooms)
        {
            if (room.locationName == newGenerationManager.LocationName)
                newGenerationManager.RoomsLists.GetRoomsList(Rooms.Default).Add(room.roomObject);
        }
        foreach (RoomInformation room in nextNpcRooms)
        {
            if (room.locationName == newGenerationManager.LocationName)
                newGenerationManager.RoomsLists.GetRoomsList(Rooms.Default).Add(room.roomObject);
        }
        foreach (RoomInformation room in nextTraderRooms)
        {
            if (room.locationName == newGenerationManager.LocationName)
                newGenerationManager.RoomsLists.GetRoomsList(Rooms.Default).Add(room.roomObject);
        }
        foreach (RoomInformation room in nextBoxRooms)
        {
            if (room.locationName == newGenerationManager.LocationName)
                newGenerationManager.RoomsLists.GetRoomsList(Rooms.Default).Add(room.roomObject);
        }
        foreach (RoomInformation room in nexteMustSpawnRooms)
        {
            if (room.locationName == newGenerationManager.LocationName)
                newGenerationManager.RoomsLists.GetRoomsList(Rooms.Default).Add(room.roomObject);
        }
        foreach (RoomInformation room in nexteExitRooms)
        {
            if (room.locationName == newGenerationManager.LocationName)
                newGenerationManager.RoomsLists.GetRoomsList(Rooms.Default).Add(room.roomObject);
        }
    }
}