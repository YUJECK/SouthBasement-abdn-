using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public GameObject ProjectileEffect;
    public int damage;
    [SerializeField] private string effectName = "None";
    [SerializeField] private float effectTime = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            return;

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<HealthEnemy>().TakeHit(damage);
            
            if(effectName != "None")
            {
                //Если есть еффект
                if(effectName == "Burn")
                    collision.gameObject.GetComponent<HealthEnemy>().GetBurn(effectTime);
                if(effectName == "Poisoned")
                    collision.gameObject.GetComponent<HealthEnemy>().GetPoisoned(effectTime);
                if(effectName == "Bleed")
                    collision.gameObject.GetComponent<HealthEnemy>().GetBleed(effectTime);
            }
        }
            
        GameObject effect = Instantiate(ProjectileEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }
}
