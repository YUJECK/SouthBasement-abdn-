using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRatAttack : MonoBehaviour
{
    private float nextTime;
    public float attackRate = 4f;
    public bool isOnTrigger; // Задаётся через EnemyCheker
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
                anim.SetTrigger("isAttack");
        }
    }
    public void HitRat()
    {
        if(isOnTrigger)
        {
            //Бьём врага
            player.GetComponent<Health>().TakeHit(HitCount);
            nextTime = Time.time + 1f / attackRate;
        }
        anim.ResetTrigger("isAttack");
    }
}
