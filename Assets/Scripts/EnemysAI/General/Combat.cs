using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PointRotation))]
public class Combat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint; // Точка атаки
    private PointRotation pointRotation;

    [Header("Параметры атаки")]
    [SerializeField] private int minDamage = 10;
    [SerializeField] private int maxDamage = 10;
    [SerializeField] private float attackRange = 0.5f; // Радиус атаки
    [SerializeField] private float attackRate = 3f; // Периодичность атаки
    [SerializeField] private float attackTimeOffset = 0.6f; // Время когда сработает корутина

    [Header("События")]
    [SerializeField] private UnityEvent onAttack = new UnityEvent(); // При атаке
    [SerializeField] private UnityEvent onBeforeAttack = new UnityEvent(); // За attackTimeOffset до атаки
    [SerializeField] private UnityEvent onEnterArea = new UnityEvent(); // Когда зашел в радиус активации атаки

    [Header("Определение цели")]
    [SerializeField] private LayerMask damageLayer; // Дамажный слой
    [SerializeField] private string enterTag = "Player"; // Тег на проверку у тригера

    private float nextTime = 0f;
    private bool onTrigger = false;
    private bool isStopped = false;

    //Методы атаки
    private IEnumerator StartAttack(float waitTime)
    {
        onBeforeAttack.Invoke();
        yield return new WaitForSeconds(waitTime);
        Attack();
    }
    private void Attack()
    {
        //Определяем все объекты попавшие в радиус атаки
        Collider2D[] hitObj = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damageLayer);
        pointRotation.StopRotating(true, 0.8f);
        onAttack.Invoke();

        //Проверяем каждый их них на наличие комнонента Health
        foreach (Collider2D obj in hitObj)
        {
            if (obj.TryGetComponent(typeof(PlayerHealth), out Component comp))
                obj.GetComponent<PlayerHealth>().TakeHit(Random.Range(minDamage, maxDamage + 1));
        }
    }
    private void SetNextAttackTime(float value) { nextTime = Time.time + value; }

    //Типо сеттеры и геттеры
    public void SetStop(bool active) { isStopped = active; }
    public bool GetStop() { return isStopped; }

    //Юнитивские методы
    private void Start()
    {
        pointRotation = GetComponent<PointRotation>();
    }
    private void Update()
    {
        //Проверяем можем ли мы атаковать
        if (!isStopped && onTrigger && Time.time >= nextTime - attackTimeOffset)
        {
            StartCoroutine(StartAttack(attackTimeOffset));
            SetNextAttackTime(attackRate + attackTimeOffset);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == enterTag)
        {
            onTrigger = true;
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
