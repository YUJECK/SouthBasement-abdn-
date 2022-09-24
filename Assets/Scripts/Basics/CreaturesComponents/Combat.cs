using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
    //serialized variables
    [SerializeField] private int minimumDamage = 10;
    [SerializeField] private int maximumDamage = 20;
    [SerializeField] private float attackRate = 1f;

    [SerializeField] private float attackRange = 0.3f;
    [SerializeField] private Transform attackPoint;

    [SerializeField] private LayerMask damageLayer;
    //private variables

    //getters
    public int Damage => Random.Range(minimumDamage, maximumDamage);
    
    //public methods
    public void StartAttacking() => StartCoroutine(StartAttackingCoroutine());
    //private methods
    private IEnumerator StartAttackingCoroutine()
    {
        while (true)
        {
            Hit();
            yield return new WaitForSeconds(attackRate);
        }
    }
    private bool Hit()
    {
        //define hitted objects
        bool isHitSomeone = false;
        Collider2D[] hitObj = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damageLayer);
        //onAttack.Invoke();

        //checking every hit objects for the presence of a Health component
        foreach (Collider2D obj in hitObj)
        {
            isHitSomeone = true;
            //if (obj.TryGetComponent(out Health health))
            //    health.TakeHit(Damage);
            Debug.Log("Hitted the " + obj.name);
        }

        return isHitSomeone;
    }

    //unity methods
    private void Start()
    {
        StartCoroutine(StartAttackingCoroutine());
    }
}