using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCloser : MonoBehaviour
{
    public bool isWent = false;
    public bool isCleaned = false;
    public GameObject Doors;
    public int enemyesCount;
    
    private void Start()
    {
        Doors.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(!isCleaned)
            {
                isWent = true;
                Doors.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if(enemyesCount == 0)
        {
            Doors.SetActive(false);
            isCleaned = true;
        }
    }
}
