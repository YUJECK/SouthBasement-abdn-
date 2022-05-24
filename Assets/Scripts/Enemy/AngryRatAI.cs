using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryRatAI : MonoBehaviour
{
    [Header("AttackSettings")]
    private float nextAttackTime;
    public float attackRate = 4f;
    public int damage = 1;
    public List<GameObject> decorationWeapons;
    [SerializeField] private Transform firePoint; 
    [SerializeField] private Rigidbody2D pointRotation; 
    [HideInInspector] public Transform player;
    private bool flippedOnRight = true;
    private Pathfinding pathManager; 
    public TriggerCheker attackTrigger;
    private Animator anim;
    [SerializeField] float runSpeed = 1f; // Скорость при беге
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
        if(coll.tag == "WeaponDecoration")
            pathManager.SetTarget(coll.transform);
        if(coll.tag == "Player") 
        {
            pathManager.SetTarget(coll.transform);
            pathManager.speed = runSpeed; //Ставим скорость на сокрость при беге
            isPlayerTarget = true;
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
        
        if(decorationWeapons.Count != 0)
            Shot(decorationWeapons[0]);

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
    private void Shot(GameObject projectile)
    {
        if(projectile != null)
        {  
            projectile.transform.position = firePoint.position;
            pointRotation.rotation = CalculateAngle();
            projectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up, ForceMode2D.Impulse);
            decorationWeapons.Remove(projectile);
        }
    }
    private float CalculateAngle()
    {
        Vector2 direction = player.position - pointRotation.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        return angle;
    }
    private void Flip(){transform.Rotate(0f,180f,0f);}
}