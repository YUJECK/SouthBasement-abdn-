using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    //Энамы
    public enum ProjectileType //Как будет работать снаряд
    {
        Bullet, //Как пуля
        Bomb //Как бомба
    }
    public enum ProjectileTarget //Кого будет атаковать
    {
        HitEnemy, //Только врага
        HitPlayer, //Только игрока
        HitBoth //Обоих
    }

    //Переменные
    [Header("Настройки снаряда")]
    public ProjectileType projectileType;
    [SerializeField] private ProjectileTarget projectileTarget;
    [SerializeField] private bool stopOnCollision;
    public EffectsList projectileEffect;
    [SerializeField] private int damage = 20;
    //Для бомбы
    [Header("Настройка бомбы")]
    [SerializeField] private float timeToExplosion = 1f;
    [SerializeField] private float explosionRadius = 0.5f;
    [SerializeField] private bool activeBombFromOtherScript = false;
    [SerializeField] private UnityEvent onStartExplosion = new UnityEvent();
    [SerializeField] private UnityEvent onExplosion = new UnityEvent();
    //Для пули

    [Header("Настройки еффекта")]
    [SerializeField] private GameObject projectileExplosionObject;
    [SerializeField] private float projectileExplosionDuration = 0.5f;
    [SerializeField] private float effectDuration = 0f;
    [SerializeField] private EffectStats effectStats;

    //Ссылки на другие скрипты
    private EffectsInfo effectsManager;
    private void Start()
    {
        effectsManager = FindObjectOfType<EffectsInfo>();
        if (projectileType == ProjectileType.Bomb)
        {
            if (!activeBombFromOtherScript) StartCoroutine(Bomb(timeToExplosion));
            onExplosion.AddListener(Explosion);
        }
    }

    //Методы бомбы
    private IEnumerator Bomb(float explTime)
    {
        onStartExplosion.Invoke();
        yield return new WaitForSeconds(explTime);
        onExplosion.Invoke();
    }
    private void Explosion()
    {
        //Определяем все объекты попавшие в радиус атаки
        Collider2D[] hitObj = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        //Проверяем каждый их них на наличие комнонента Health
        foreach (Collider2D obj in hitObj)
        {
            if (obj.TryGetComponent(typeof(Health), out Component comp))
            {
                if (((obj.CompareTag("Enemy") && projectileTarget == ProjectileTarget.HitEnemy || projectileTarget == ProjectileTarget.HitBoth) ||
                (obj.CompareTag("Player") && projectileTarget == ProjectileTarget.HitPlayer || projectileTarget == ProjectileTarget.HitBoth)))
                {
                    Health hittedHealth = obj.gameObject.GetComponent<Health>();
                    hittedHealth.TakeHit(damage);

                    if (projectileEffect != EffectsList.None && hittedHealth.EffectHandler != null) //Если есть еффект
                        hittedHealth.EffectHandler.AddEffect(effectDuration, effectStats, projectileEffect);
                }
            }
        }
        GameObject effect = Instantiate(projectileExplosionObject, gameObject.transform.position, Quaternion.identity);
        Destroy(effect, projectileExplosionDuration);
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        if (projectileType == ProjectileType.Bomb)
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    //Методы пули
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (projectileType == ProjectileType.Bullet)
        {
            if (((collision.gameObject.CompareTag("Enemy") && projectileTarget == ProjectileTarget.HitEnemy || collision.gameObject.CompareTag("Enemy") && projectileTarget == ProjectileTarget.HitBoth) ||
                (collision.gameObject.CompareTag("Player") && projectileTarget == ProjectileTarget.HitPlayer || collision.gameObject.CompareTag("Player") && projectileTarget == ProjectileTarget.HitBoth)))
            {
                Health hittedHealth = collision.gameObject.GetComponent<Health>();
                hittedHealth.TakeHit(damage);

                if (projectileEffect != EffectsList.None && hittedHealth.EffectHandler != null) //Если есть еффект
                    hittedHealth.EffectHandler.AddEffect(effectDuration, effectStats, projectileEffect);
            }

            GameObject effect = Instantiate(projectileExplosionObject, gameObject.transform.position, Quaternion.identity);
            Destroy(effect, projectileExplosionDuration);
            Destroy(gameObject);
        }
        if (stopOnCollision) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}