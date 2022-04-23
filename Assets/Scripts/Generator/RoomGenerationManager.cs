using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerationManager : MonoBehaviour
{
    [Header("Генерация уровней")]

    public int RoomsCount = 15;
    public int[] rooms;
    public int ExitRoomIndex;
    public int NPCsRoomsCount;
    public int NowSpawnedSimpleRooms;
    public int NowSpawnedNPCsRooms;
    public int NowSpawnedRooms;
    public int TraderIndex;
    public int BoxIndex;

    [Header("")] 
    public List<RoomInfo> roomsList;

    [Header("Обычные комнаты")]
    public GameObject[] left;
    public GameObject[] right;
    public GameObject[] top;
    public GameObject[] down;

    [Header("Для NPC")]
    public GameObject[] leftNPC; 
    public GameObject[] rightNPC;
    public GameObject[] topNPC;
    public GameObject[] downNPC;

    void Awake()
    {
        Invoke("ActivateExitRoom",5f);
        Debug.Log(NPCsRoomsCount);

        createRoomsList:
        
        rooms = new int[RoomsCount];
        int npcCount = 0;
        
        for(int i = 0; i < RoomsCount; i++)
        {
            rooms[i] = Random.Range(0,2);
            if(rooms[i] == 1)
            {
                npcCount++;   
                if(npcCount > NPCsRoomsCount) rooms[i] = 0;
            }
        }
        if(npcCount == 0) goto createRoomsList;

        //Индексация торговца и коробки
        TraderIndex = Random.Range(1,NowSpawnedNPCsRooms);
        BoxIndex = Random.Range(1,NowSpawnedNPCsRooms); 
        NPCsRoomsCount = FindObjectOfType<RoomsNPCList>().NPCs.Count + 2;
        if(BoxIndex == TraderIndex) //Если коробка с торговцем в одной комнате
        {
            int i = 0;

            while(true)
            {
                i++;    
                BoxIndex = Random.Range(1,NPCsRoomsCount);

                if(BoxIndex != TraderIndex || i==100)
                    break;
            }
        }
    }

    void ActivateExitRoom()
    {
        ExitRoomIndex = Random.Range(1,NowSpawnedRooms-1);
        roomsList[ExitRoomIndex].ActivateExit();
    }
}
