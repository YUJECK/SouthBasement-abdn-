using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryRatAI : MonoBehaviour
{
    [Header("AttackSettings")]
    private float nextAttackTime;
    public float attackRate = 4f;
    public int damage = 1;
    [HideInInspector] public Transform player;
    private bool flippedOnRight = true;
    private Pathfinding pathManager; 
    public TriggerCheker attackTrigger;
    private Animator anim;
    [SerializeField] float runSpeed = 2f; // Скорость при беге
    [SerializeField] float walkSpeed = 1f; // Скорость при ходьбе
    [SerializeField] private Transform[] targetPoints; // Список точек для перемещения   
    private bool isPlayerTarget; // Идет ли объект к цели

    private void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Transform>();
        anim = GetComponent<Animator>();
        pathManager = GetComponent<Pathfinding>();
    }
    private void OnTriggerStay2D(Collider2D coll)
    {   
        if(coll.tag == "Player" && !isPlayerTarget) 
        {
            pathManager.SetTarget(coll.transform);
            pathManager.speed = runSpeed; //Ставим скорость на сокрость при беге
            isPlayerTarget = true;
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {   
        if(coll.tag == "Player" && isPlayerTarget) 
        {
            if(targetPoints.Length != 0)
            {
                pathManager.SetTarget(targetPoints[Random.Range(0, targetPoints.Length)]);
                pathManager.speed = walkSpeed; //Ставим скорость на сокрость при беге
                isPlayerTarget = false;
            }
        }
    }
    void FixedUpdate()
    {   
        if(attackTrigger.isOnTrigger)
        {
            if (Time.time >= nextAttackTime)
                anim.SetTrigger("isAttack");
        }

        if(pathManager.target != null)//Поворот врага к игроку
        {
            //Поворот врага
            if(pathManager.target.position.x > transform.position.x & !flippedOnRight)
            {
                Flip();
                flippedOnRight = true;
            }
            else if(pathManager.target.position.x < transform.position.x & flippedOnRight)
            {
                Flip(); 
                flippedOnRight = false;
            }
        }
        if(pathManager.target == null && !isPlayerTarget) pathManager.SetTarget(targetPoints[Random.Range(0, targetPoints.Length)]);
        
        //Анимация 
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
            nextAttackTime = Time.time + 1f / attackRate;
        }
        anim.ResetTrigger("isAttack");
    }
    private void Flip(){transform.Rotate(0f,180f,0f);}
}