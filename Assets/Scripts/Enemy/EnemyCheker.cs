using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheker : MonoBehaviour
{
    public EnemyRatAttack enemy;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
            enemy.isOnTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {   
        if(other.tag == "Player")
            enemy.isOnTrigger = false;
    }
}
