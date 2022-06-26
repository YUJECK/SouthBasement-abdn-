using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour
{
    //Показатели здоровья
    [Header("Здоровье")]
    public int health;
    public int maxHealth;

    public SpriteRenderer effectIndicator;
    [SerializeField] private Color damageColor;

    [Header("Звуки")]
    [SerializeField] private string destroySound; // Звук смерти
    [SerializeField] private string hitSound; // Звук получения урона

    [Header("Еффекты")]
    public List<EffectsList> effectsCanUse;

    //События
    [Header("События")]
    public UnityEvent onDie = new UnityEvent();  //Методы которые вызовуться при уничтожении объекта
    public UnityEvent<int, int> onHealthChange = new UnityEvent<int, int>();
    public UnityEvent<float> stun = new UnityEvent<float>();
    private UnityEvent effects = new UnityEvent();

    //Другое
    private Coroutine damageInd = null;

    //Ссылки на другие классы
    public RoomCloser roomCloser;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public AudioManager audioManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        onDie.AddListener(DefaultOnDie);

        if (roomCloser != null)
        {
            roomCloser.EnemyCounterTunUp();
            onDie.AddListener(roomCloser.EnemyCounterTunDown);
        }
    }
    public void DefaultOnDie() => Destroy(gameObject);

    //Всякие манипуляции со здоровьем
    public abstract void TakeHit(int damage);
    private IEnumerator TakeHitVizualization()
    {
        gameObject.GetComponent<SpriteRenderer>().color = damageColor;
        yield return new WaitForSeconds(0.6f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(100, 100, 100, 100);
    }
    public abstract void Heal(int heal);
    public abstract void SetHealth(int NewMaxHealth, int NewHealth);
    public abstract void TakeAwayHealth(int TakeAwayMaxHealth, int TakeAwayHealth);

    //Еффекты которые могут наложиться на врага    
    private IEnumerator EffectActive(float duration, EffectsList effect)
    {
        if(effectsCanUse.Contains(effect))
        {
            UnityAction effectMethod = null;
            switch (effect)
            {
                case EffectsList.Burn:
                    effects.AddListener(Burn);
                    effectMethod = Burn;
                    break;
                case EffectsList.Bleed:
                    effects.AddListener(Bleed);
                    effectMethod = Bleed;
                    break;
                case EffectsList.Poisoned:
                    effects.AddListener(Poisoned);
                    effectMethod = Poisoned;
                    break;
                case EffectsList.Regeneration:
                    effects.AddListener(Regeneration);
                    effectMethod = Regeneration;
                    break;
            }
            yield return new WaitForSeconds(duration);
            effects.RemoveListener(effectMethod);
        }
    }
    public void GetEffect(float duration, EffectsList effect) => StartCoroutine(EffectActive(duration, effect));

    //Еффекты
    public void Burn() { TakeHit(9); }
    public void Poisoned() { TakeHit(5); }
    public void Bleed() { TakeHit(14); }
    public void Regeneration() { Heal(10); }
}
