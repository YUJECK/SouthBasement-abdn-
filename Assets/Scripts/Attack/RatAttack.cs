using UnityEngine;
using System.Collections.Generic;
public class RatAttack : MonoBehaviour
{
    public SpriteRenderer Sp;
    public MelleRangeWeapon melleWeapon;
    public SpriteRenderer weaponSprite;
    public Animator anim;
    public Animator animRange;
    public Transform AttackPoint;
    public LayerMask EnemyLayers;
    public HealthEnemy enemyHealth;
    private CursorController cursor;
    public float sp_rotation;
    private float nextTime;
    public float AttackRange;
    public float attackRate = 2f;
    public int damage = 2;
    public bool is_Attack;
    private Vector3 posWhenAttack;

    private void Start()
    {
        cursor = FindObjectOfType<CursorController>();
    }
 
    void Update()
    {
        if(Time.time >= nextTime) // Атакак крысы
        {
            if (Input.GetMouseButtonDown(0) & !FindObjectOfType<Player>().isSprinting)
            {
                Attack();
                cursor.CursorClick();
                sp_rotation = GetComponent<Rigidbody2D>().rotation;
                nextTime = Time.time + 1f / attackRate;
            }
        }
        if(Sp.enabled)
        {
            if(Time.time >= nextTime)
            {
                Sp.enabled = false;
                is_Attack = false;
            }
        }
    }
    void Attack()
    {
        is_Attack = true;
        Sp.enabled = true;
        posWhenAttack = transform.position;
        anim.SetTrigger("IsAttack");
        animRange.SetTrigger("isShow");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position,AttackRange,EnemyLayers);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.tag == "Enemy")
            {
                if (!enemy.isTrigger)
                {
                    if(melleWeapon.effect == MelleRangeWeapon.Effect.Poisoned)
                        enemy.GetComponent<HealthEnemy>().GetPoisoned(melleWeapon.effectTime);
                    if(melleWeapon.effect == MelleRangeWeapon.Effect.Bleed)
                        enemy.GetComponent<HealthEnemy>().GetBleed(melleWeapon.effectTime);
                    if(melleWeapon.effect == MelleRangeWeapon.Effect.Burn)
                        enemy.GetComponent<HealthEnemy>().GetBurn(melleWeapon.effectTime);
                    
                    enemy.GetComponent<HealthEnemy>().TakeHit(damage);
                    Debug.Log("Enemy health: " + enemy.GetComponent<HealthEnemy>().health);
                }
            }
        }
    }
    public void SetMelleWeapon(MelleRangeWeapon weapon)
    {
        melleWeapon = weapon;
        AttackRange = weapon.attackRange;
        attackRate = weapon.attackRate;
        damage = weapon.damage;
        weaponSprite.sprite = weapon.sprite;
        AttackPoint.localScale = new Vector3(10*weapon.attackRange/2,10*weapon.attackRange/2,1);
    }

    public void HideMelleweaponIcon(bool hiding) // Включние, выключение спрайта оружия
    {   
        weaponSprite.gameObject.SetActive(!hiding);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
