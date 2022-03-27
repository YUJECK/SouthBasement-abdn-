using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Transform player;
    private bool flippedOnRight = true;

    private void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Transform>();
    }
    
    void FixedUpdate()
    {   
        if(player.position.x > transform.position.x & !flippedOnRight)
        {
            Flip();
            flippedOnRight = true;
        }
        if(player.position.x < transform.position.x & flippedOnRight)
        {
            Flip(); 
            flippedOnRight = false;
        }
    }
    private void Flip()
    {
        transform.Rotate(0f,180f,0f);
    }
}