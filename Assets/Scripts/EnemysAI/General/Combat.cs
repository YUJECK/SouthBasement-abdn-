using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PointRotation))]
public class Combat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [Header("Параметры атаки")]
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackRate = 3f;
    [SerializeField] private float attackTimeOffset = 0.6f;
    [SerializeField] private UnityEvent onAttack = new UnityEvent();
    [SerializeField] private UnityEvent onBeforeAttack = new UnityEvent();
    [SerializeField] private UnityEvent onEnterArea = new UnityEvent();
    [Header("Определение цели")]
    [SerializeField] private LayerMask damageLayer;
    [SerializeField] private string enterTag = "Player";

    private float nextTime = 0f;
    private bool onTrigger = false;
   
    //Методы атаки
    private void Attack()
    {
        Collider2D[] hitObj = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damageLayer);
        onAttack.Invoke();

        foreach (Collider2D obj in hitObj)
        {
            if(obj.TryGetComponent(typeof(Health), out Component comp))
            {
                obj.GetComponent<Health>().TakeHit(damage);
                SetNextAttackTime(attackRate);
            }
        }
    }
    private void SetNextAttackTime(float value) { nextTime = Time.time + value; }

    //Юнитивские методы
    private void Update() 
    {
        if (onTrigger && Time.time + attackTimeOffset == nextTime) { onBeforeAttack.Invoke(); } 
        if (onTrigger && Time.time >= nextTime) Attack(); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == enterTag)
        {
            onTrigger = true;
            SetNextAttackTime(attackTimeOffset);
            onEnterArea.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            onTrigger = false;
    }
    void OnDrawGizmosSelected()//Отрисовка радиуса атаки
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
