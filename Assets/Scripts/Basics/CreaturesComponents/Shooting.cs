using UnityEngine;

public sealed class Shooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    public void Shoot(GameObject projectile, float speed, ForceMode2D forceMode2D)
    {
        if (firePoint != null)
        {
            GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
            Rigidbody2D projectileRigidbody = newProjectile.GetComponent<Rigidbody2D>();
            projectileRigidbody.AddForce(Vector2.up * speed, forceMode2D);
        }
        else Debug.LogWarning("Fire point is null");
    }
}