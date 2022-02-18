using UnityEngine;

public class RatAttack : MonoBehaviour
{
    public SpriteRenderer Sp;
    public Animator anim;
    public Animator animRange;
    public Transform AttackPoint;
    public LayerMask EnemyLayers;
    public HealthEnemy enemyHealth;
    
    public float sp_rotation;
    private float nextTime;
    public float AttackRange;
    public float attackRate = 2f;
    public int damage = 2;
    public bool is_Attack;
    private Vector3 posWhenAttack;
 
    void Update()
    {
        if(Time.time >= nextTime)
        {
            if (Input.GetMouseButtonDown(0) & !FindObjectOfType<Player>().isSprinting)
            {
                Attack();
                sp_rotation = GetComponent<Rigidbody2D>().rotation;
                nextTime = Time.time + 1f / attackRate;
            }
        }
        if(Sp.enabled)
        {
            if(Time.time >= nextTime)
            {Sp.enabled = false;
            is_Attack = false;}
        }
    }
    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position,AttackRange,EnemyLayers);
        anim.SetTrigger("IsAttack");
        animRange.SetTrigger("isShow");
        Sp.enabled = true;
        is_Attack = true;
        posWhenAttack = transform.position;
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.tag == "Enemy")
            {
                if (!enemy.isTrigger)
                {
                    enemy.GetComponent<HealthEnemy>().TakeHit(damage);
                    Debug.Log("Health" + enemy.GetComponent<HealthEnemy>().health);
                }
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
