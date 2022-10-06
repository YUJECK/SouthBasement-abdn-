using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TriggerChecker))]
public class Combat : MonoBehaviour
{
    //serialized variables
    [Header("Damage settings")]
    [SerializeField] private int minimumDamage = 10;
    [SerializeField] private int maximumDamage = 20;
    [Header("Timing settings")]
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private float attackRate = 1f;
    [Header("Attack area settings")]
    [SerializeField] private float attackRange = 0.3f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask damageLayer;

    //only private variables
    private TriggerChecker triggerChecker;

    //events
    [Header("Events")]
    public UnityEvent beforeHitting = new UnityEvent();
    public UnityEvent onHitting = new UnityEvent();

    //getters
    public int Damage => Random.Range(minimumDamage, maximumDamage);
    public TriggerChecker TriggerChecker => triggerChecker;

    //public methods
    public void StartAttackingConstantly() => StartCoroutine(ConstantlyAttackingCoroutine());
    public void StopAttackingConstantly() => StopCoroutine(ConstantlyAttackingCoroutine());

    //private methods
    private IEnumerator ConstantlyAttackingCoroutine()
    {
        while (true)
        {
            beforeHitting.Invoke();
            yield return new WaitForSeconds(attackDelay);
            Hit();
            yield return new WaitForSeconds(attackRate);
        }
    }
    private bool Hit()
    {
        //define hitted objects
        bool isHitSomeone = false;
        Collider2D[] hitObj = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damageLayer);
        onHitting.Invoke();

        //checking every hit objects for the presence of a Health component
        foreach (Collider2D obj in hitObj)
        {
            if (obj.TryGetComponent(out Health health))
            {
                health.TakeHit(Damage);
                isHitSomeone = true;
            }
        }
        return isHitSomeone;
    }

    //unity methods

    //temp
    private void Awake() => triggerChecker = GetComponent<TriggerChecker>();
}