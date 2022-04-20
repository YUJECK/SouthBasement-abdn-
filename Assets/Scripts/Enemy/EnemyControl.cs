using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [Header("AttackSettings")]
    private float nextTime;
    public float attackRate = 4f;
    public int damage = 1;
    [HideInInspector] public Transform player;
    private bool flippedOnRight = true;
    private Pathfinding pathManager; 
    public TriggerCheker attackTrigger;
    private Animator anim;

    private void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Transform>();
        anim = GetComponent<Animator>();
        pathManager = GetComponent<Pathfinding>();
    }
    
    void FixedUpdate()
    {   
        if(attackTrigger.isOnTrigger)
        {
            if (Time.time >= nextTime)
                anim.SetTrigger("isAttack");
        }
        if(pathManager.target!=null)
        {
            if(pathManager.target.position.x > transform.position.x & !flippedOnRight)
            {
                Flip();
                flippedOnRight = true;
            }
            if(pathManager.target.position.x < transform.position.x & flippedOnRight)
            {
                Flip(); 
                flippedOnRight = false;
            }
        }
        
        if(pathManager.isNowGoingToTarget)
            anim.SetBool("IsRun", true);
        else
            anim.SetBool("IsRun",false);
    }

    public void Hit()
    {
        if(attackTrigger.isOnTrigger)
        {
            //Бьём врага
            player.GetComponent<Health>().TakeHit(damage);
            nextTime = Time.time + 1f / attackRate;
        }
        anim.ResetTrigger("isAttack");
    }
    private void Flip()
    {
        transform.Rotate(0f,180f,0f);
    }
}