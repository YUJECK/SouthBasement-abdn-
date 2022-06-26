using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject ProjectileEffect;
    [SerializeField] private float projectileEffectDuration;
    public int damage;
    [SerializeField] private EffectsList projectileEffect;
    [SerializeField] private float effectTime = 0f;

    //Ссылки на другие скрипты
    private EffectsManager effectsManager;
    private void Start()
    {
        effectsManager = FindObjectOfType<EffectsManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            return;

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<HealthEnemy>().TakeHit(damage);
            
            if(projectileEffect != EffectsList.None)
            {
                ////Если есть еффект
                //if(projectileEffect == EffectsList.Poisoned)
                //    effectsManager.GetBurn(effectTime, null, collision.gameObject.GetComponent<HealthEnemy>());
                //if(projectileEffect== EffectsList.Burn)
                //    effectsManager.GetBurn(effectTime, null, collision.gameObject.GetComponent<HealthEnemy>());
                //if(projectileEffect == EffectsList.Bleed)
                //    effectsManager.GetBurn(effectTime, null, collision.gameObject.GetComponent<HealthEnemy>());
            }
        }
            
        GameObject effect = Instantiate(ProjectileEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(effect, projectileEffectDuration);
        Destroy(gameObject);
    }
}
