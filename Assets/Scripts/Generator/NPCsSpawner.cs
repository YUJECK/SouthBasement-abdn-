using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsSpawner : MonoBehaviour
{
    public RoomsNPCList list;
    public int NPCSpawned;
    public bool isSpawned;
    private void Start()
    {
        list = FindObjectOfType<RoomsNPCList>();
    }

    public void SpawnNPC(GameObject Npc = null)
    {
        if(!isSpawned)
        {
            Debug.Log("SpawnNPC ");
            if(Npc == null)
            {
                Debug.Log("SpawnNPC Null"); 
                NPCSpawned = Random.Range(0,list.NPCs.Count);
                Instantiate(list.NPCs[NPCSpawned], transform.position, transform.rotation, transform);
                list.NPCs.Remove(list.NPCs[NPCSpawned]);
            }
            else if(Npc != null)
            {
                Instantiate(Npc, transform.position, transform.rotation, transform);
                Debug.Log("Spawn Npc not null");
            }

            list.SpawnedNPC++;
            isSpawned = true;
        }
    }
}
