using UnityEngine;
using System.Collections.Generic;

public class RoomsNPCList : MonoBehaviour
{
    public List<GameObject> NPCs; 
    public GameObject Trader;
    public int BoxesInLevel = 1;
    public GameObject Box;

    private RoomGenerationManager generationManager;
    public List<NPCsSpawner> NPCRooms = new List<NPCsSpawner>();   
    public int SpawnedNPC;

    private void Awake()
    {
        generationManager = GetComponent<RoomGenerationManager>();
        Invoke("StartSpawning", 10f);
    }

    private void StartSpawning()
    {          
        Debug.Log("Starting Spawning");
        for(int i = 0; i < generationManager.NowSpawnedRooms; i++)
        {
            if(generationManager.roomsList[i].spawnerNPC != null)
            {
                NPCRooms.Add(generationManager.roomsList[i].spawnerNPC);
                Debug.Log("RoomAdd");
            }     
        }

        //Спавним коробку и торговца
        if(NPCRooms[generationManager.BoxIndex-1] != null)
        {
            NPCRooms[generationManager.BoxIndex-1].SpawnNPC(Box);
            NPCRooms.Remove(NPCRooms[generationManager.BoxIndex-1]);    
        }
        if(NPCRooms[generationManager.TraderIndex-1] != null)
        {
            NPCRooms[generationManager.TraderIndex-1].SpawnNPC(Trader);
            NPCRooms.Remove(NPCRooms[generationManager.TraderIndex-1]);
        }
        
        for(int i = 0; i < NPCRooms.Count; i++)
        {
            Debug.Log("SpawnNpcFunc");
            if(NPCRooms[i] != null)
                NPCRooms[i].SpawnNPC();
        }
    }
}
