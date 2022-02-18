using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    public RoomSpawner.Directories direction_1;
    public RoomSpawner.Directories direction_2;
    public RoomSpawner.Directories direction_3;
    public RoomSpawner.Directories direction_4;
    public NPCsSpawner spawnerNPC;
    public RoomGenerationManager list;

    public GameObject room;
    public GameObject ExitInRoom;
    public bool isDestroyer = false;
    public bool isStartRoom = false;

    void Start()
    {
        list = FindObjectOfType<RoomGenerationManager>();

        //��������� ������� � ������ ���� ������
        list.roomsList.Add(GetComponent<RoomInfo>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spawner")
        {
            if (isDestroyer)
            {
                Destroy(collision.gameObject);
                collision.GetComponent<RoomSpawner>().isAnotherRoomSpawned = false;
            }
        }

        if (collision.tag == "RoomInfo")
            Destroy(collision.GetComponent<RoomInfo>().room);
    }
    public void ActivateExit()
    {
        ExitInRoom.SetActive(true);
    }
}
