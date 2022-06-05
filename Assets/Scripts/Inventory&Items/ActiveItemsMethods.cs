using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemsMethods : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    [Header("Чилли перец")]
    [SerializeField] private GameObject fireballPrefab;
    public float FireballForce;

    [Header("Мышеловки")]
    [SerializeField] private GameObject mouseTrapPrefab;

    public void SpawnFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * FireballForce, ForceMode2D.Impulse);
    }

    public void SpawnMousetrap() { GameObject fireball = Instantiate(mouseTrapPrefab, firePoint.position, Quaternion.identity);}
}
