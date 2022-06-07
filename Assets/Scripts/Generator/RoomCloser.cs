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
    [SerializeField] private int enemyesCount;
    
    private void Awake() { Doors.SetActive(false); if (enemyesCount > 0) enemyesCount = 0; }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isCleaned)
        {
             Doors.SetActive(true);
            _isWent = true;
        }
    }
    
    public void EnemyCounterTunDown() 
    { 
        enemyesCount--; 
        if (enemyesCount <= 0)
        {
            Doors.SetActive(false);
            isCleaned = true;
        }
    }
    public void EnemyCounterTunUp() { enemyesCount++; }
}
