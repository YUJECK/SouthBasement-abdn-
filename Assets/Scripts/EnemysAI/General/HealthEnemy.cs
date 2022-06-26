using System.Collections;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public struct Effect
{
    public float startTime;
    public float durationTime;
};
public class HealthEnemy : MonoBehaviour
{
    //Показатели здоровья
    public int health;
    public int maxHealth;

    public UnityEvent onDestroy = new UnityEvent();  //Методы которые вызовуться при уничтожении объекта
    public SpriteRenderer effectIndicator;
    [SerializeField] private Color damageColor;

    [SerializeField] int minCheese = 0;
    [SerializeField] int maxCheese = 4;
    [SerializeField] private string destroySound; // Звук смерти
    [SerializeField] private string hitSound; // Звук получения урона

    //Время действия еффектов
    public UnityEvent<float> stun = new UnityEvent<float>();
    public Coroutine damageInd = null;

    //Ссылки на другие скрипты
    public RoomCloser roomCloser;
    private GameManager gameManager;
    [SerializeField] private AngryRatAI angryRatAi;
    private EffectsManager effectsManager;
    private AudioManager audioManager;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        effectsManager = FindObjectOfType<EffectsManager>();
        audioManager = FindObjectOfType<AudioManager>();

        if (roomCloser != null)
        {
            roomCloser.EnemyCounterTunUp();
            onDestroy.AddListener(roomCloser.EnemyCounterTunDown);
        }
    }
    private void OnDestroy() { onDestroy.Invoke(); }

    //Всякие манипуляции со здоровьем
    public void TakeHit(int damage, float stunTime = 0f)
    {
        health -= damage;
        if (hitSound != "") audioManager.PlayClip(hitSound);
        if (health <= 0)
        {
            int cheeseCount = Random.Range(minCheese, maxCheese);
            if (maxCheese != 0) gameManager.SpawnCheese(transform.position, cheeseCount);
            if (destroySound != "") audioManager.PlayClip(destroySound);
            Destroy(gameObject);
        }

        if (stunTime != 0f)
            stun.Invoke(stunTime);
        if (damageInd != null) StopCoroutine(damageInd);

        damageInd = StartCoroutine(TakeHitVizualization());
    }
    private IEnumerator TakeHitVizualization()
    {
        gameObject.GetComponent<SpriteRenderer>().color = damageColor;
        yield return new WaitForSeconds(0.6f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(100, 100, 100, 100);
    }
    public void Heal(int bonusHealth)
    {
        health += bonusHealth;

        if (health > maxHealth)
            health = maxHealth;
    }
    public void SetHealth(int NewMaxHealth, int NewHealth)
    {
        maxHealth = NewMaxHealth;
        health = NewHealth;

        if (health > maxHealth)
            health = maxHealth;
    }
    public void TakeAwayHealth(int TakeAwayMaxHealth, int TakeAwayHealth)
    {
        maxHealth -= TakeAwayMaxHealth;
        health -= TakeAwayHealth;

        if (health > maxHealth)
            health = maxHealth;
    }
    public void SetBonusHealth(int NewMaxHealth, int NewHealth)
    {
        Debug.Log("New Health");
        maxHealth += NewMaxHealth;
        health += NewHealth;

        if (health > maxHealth)
            health = maxHealth;
    }

    //Еффекты которые могут наложиться на врага   
    public void Burn() { TakeHit(9); }
    public void Poisoned() { TakeHit(5); }
    public void Bleed() { TakeHit(14); }
    public void Regeneration() { Heal(10); }
}