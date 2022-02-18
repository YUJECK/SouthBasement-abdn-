using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Rigidbody2D player;
    private bool flippedOnRight = false;

    private void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {   
        if(player.position.x > transform.position.x & !flippedOnRight)
        {
            Flip();
              flippedOnRight = true;
        }
        if(player.position.x > transform.position.x & flippedOnRight)
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