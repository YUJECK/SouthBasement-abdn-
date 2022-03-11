using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject ProjectileEffect;
    public int damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            return;
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<HealthEnemy>().TakeHit(damage);
            
        GameObject effect = Instantiate(ProjectileEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }
}
