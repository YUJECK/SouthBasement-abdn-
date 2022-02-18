using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRatAttack : MonoBehaviour
{
    private float nextTime;
    public float attackRate = 4f;
    public bool isOnTrigger;
    public Animator anim;
    public int HitCount = 1;
    public Rigidbody2D player;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>().GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if(isOnTrigger)
        {
            if (Time.time >= nextTime)
            {
                anim.SetTrigger("isAttack");
                nextTime = Time.time + 1f / attackRate;
            }
        }
        if (Time.time >= nextTime)
        {
            anim.ResetTrigger("isAttack");
        }
    }
    public void HitRat()
    {
        //Hit the player
        player.GetComponent<Health>().TakeHit(HitCount);
    }
}
