using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PointRotation))]
public class Combat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackRate = 3f;
    private float nextTime = 0f;
    [SerializeField] private LayerMask damageLayer;
    [SerializeField] private UnityEvent onAttack = new UnityEvent();

    private void Attack()
    {
        if(Time.time >= nextTime)
        {
            Debug.Log("attack");
            Collider2D[] hitObj = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damageLayer);
            onAttack.Invoke();

            foreach (Collider2D obj in hitObj)
            {
                if(obj.TryGetComponent(typeof(Health), out Component comp))
                {
                    obj.GetComponent<Health>().TakeHit(damage);
                    SetNextAttackTime();
                }
            }
        }
    }
    private void SetNextAttackTime() { nextTime = Time.time + attackRate; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("sdkljhf");
        if (collision.gameObject.layer == damageLayer)
            Attack();
    }

    void OnDrawGizmosSelected()//Отрисовка радиуса атаки
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
