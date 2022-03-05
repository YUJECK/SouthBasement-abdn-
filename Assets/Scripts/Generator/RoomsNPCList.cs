using UnityEngine;
using System.Collections.Generic;

public class RoomsNPCList : MonoBehaviour
{
    public List<GameObject> NPCs; //Лист всех нпс которые могут заспавнится на уровне
    public GameObject Trader; //Префаб торговца
    public bool IsTraderSpawning = true; // Будет ли спавниться торговец
    public int BoxesInLevel = 1; // Определяет сколько будет коробок в уровне(пока что не используется)
    public GameObject Box; //Префаб коробки

    private RoomGenerationManager generationManager;
    public List<NPCsSpawner> NPCRooms = new List<NPCsSpawner>(); //Список всех комнат для нпс
    public int SpawnedNPC; //Счетчик заспавненных нпс

    private void Awake()
    {
        generationManager = GetComponent<RoomGenerationManager>();
        Invoke("StartSpawning", 4f);//Через 4 секунды начнут спавниться нпс
    }

    private void StartSpawning()
    {          
        Debug.Log("Starting Spawning");
        //Находим и добавляем все комнаты для нпс в лист
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
        if(IsTraderSpawning & NPCRooms[generationManager.TraderIndex-1] != null)
        {
            NPCRooms[generationManager.TraderIndex-1].SpawnNPC(Trader);
            NPCRooms.Remove(NPCRooms[generationManager.TraderIndex-1]);
        }
        
        //Если нпс будет много то наверное лучше будет поменять на NPCRooms.Count
        for(int i = 0; i < NPCs.Count; i++)
        {
            Debug.Log("Index i = " + i + "; NPCsRoomsCount = " + NPCRooms.Count);
            if(NPCRooms[i] != null)
                NPCRooms[i].SpawnNPC();
        }
    }
}
