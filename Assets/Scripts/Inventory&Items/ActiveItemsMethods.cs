using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemsMethods : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private PointRotation pointRotation;

    [Header("Чилли перец")]
    [SerializeField] private GameObject fireballPrefab;
    public float FireballForce;

    [Header("Мышеловки")]
    [SerializeField] private GameObject mouseTrapPrefab;

    [Header("Хлопушка")]
    [SerializeField] private float crackerRange = 1f;
    [SerializeField] private GameObject crackerEffect;
    [SerializeField] private int damage = 40;

    public void SpawnFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * FireballForce, ForceMode2D.Impulse);
    }
    public void SpawnMousetrap() { GameObject fireball = Instantiate(mouseTrapPrefab, firePoint.position, Quaternion.identity);}
    public void Cracker()
    {
        pointRotation.StopRotating(true, 0.2f);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firePoint.position, crackerRange);
        GameObject effect =  Instantiate(crackerEffect, firePoint.position, firePoint.rotation, firePoint);
        Destroy(effect, 0.26f);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.tag == "Enemy" && !enemy.isTrigger)
                enemy.GetComponent<EnemyHealth>().TakeHit(damage, 3f);
        }
    }
}