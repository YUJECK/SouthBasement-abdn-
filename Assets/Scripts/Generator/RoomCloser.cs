using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCloser : MonoBehaviour
{
    private bool isCleaned = false;
    private bool _isWent;
    public bool isWent
    {
        get { return _isWent; }
        private set { _isWent = value; }
    }

    public GameObject Doors;
    private int enemyesCount;
    
    private void Start() { Doors.SetActive(false); }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isCleaned)
        {
             Doors.SetActive(true);
            _isWent = true;
        }
    }
    private void Update()
    {
        if(enemyesCount <= 0)
        {
            Doors.SetActive(false);
            isCleaned = true;
        }
    }
    
    public void EnemyCounterTunDown() { enemyesCount--; }
    public void EnemyCounterTunUp() { enemyesCount++; }
}
