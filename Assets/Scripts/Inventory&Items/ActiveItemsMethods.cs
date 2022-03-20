using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemsMethods : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    [Header("Чилли перец")]
    [SerializeField] private GameObject fireballPrefab;
    public float FireballForce;

    public void SpawnFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * FireballForce, ForceMode2D.Impulse);
    }
}
