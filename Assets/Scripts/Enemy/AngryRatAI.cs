using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryRatAI : MonoBehaviour
{
    [Header("AttackSettings")]
    private float nextTime;
    public float attackRate = 4f;
    public int damage = 1;
    public List<GameObject> decorationWeapons;
    [SerializeField] private Transform firePoint; 
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
    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.tag == "WeaponDecoration")
            pathManager.SetTarget(coll.transform);
    }

    void FixedUpdate()
    {   
        if(attackTrigger.isOnTrigger)
        {
            if (Time.time >= nextTime)
                anim.SetTrigger("isAttack");
        }

        if(pathManager.target!=null)//Поворот врага к игроку
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
            nextTime = Time.time + 1f / attackRate;
        }
        anim.ResetTrigger("isAttack");
    }
    private void Shot(GameObject projectile)
    {
        if(projectile!=null)
        {
            Vector3 direction = player.position - transform.position ;
            float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg - 90f;
            
            GameObject Projectile = Instantiate(projectile, firePoint.position, Quaternion.identity);
            Projectile.GetComponent<Rigidbody2D>().rotation = angle;
            Projectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100f, ForceMode2D.Force);
            decorationWeapons.Remove(projectile);
        }
    }
    private void Flip(){transform.Rotate(0f,180f,0f);}
}