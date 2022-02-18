using UnityEngine;

public class FireballSpawn : MonoBehaviour
{
    public Transform firePoint;
    public GameObject FireballPrefab;
    public float FireballForce;

    public void SpawnFireball()
    {
        GameObject fireball = Instantiate(FireballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * FireballForce, ForceMode2D.Impulse);
    }
}