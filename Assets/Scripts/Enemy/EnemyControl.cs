using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Transform player;
    private bool flippedOnRight = true;
    [SerializeField] private Pathfinding pathManager;
    [SerializeField] private Animator anim;

    private void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Transform>();
        anim = GetComponent<Animator>();
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
        
        if(pathManager.isRun)
            anim.SetBool("isRun", true);
        else
            anim.SetBool("isRun",false);
    }
    private void Flip()
    {
        transform.Rotate(0f,180f,0f);
    }
}