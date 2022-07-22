using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EffectStats
{
    public EffectStats(float rate, int strength)
    {
        effectRate = rate;
        effectStrength = strength;
    }
    public float effectRate;
    public int effectStrength;
    private float nextTime = 0f;
    public void SetNextTime(float time) => nextTime += time;
    public void ResetToZeroNextTime() => nextTime = 0f;
    public float GetNextTime() { return nextTime; }
};
public abstract class Health : MonoBehaviour
{
    //Показатели здоровья
    [Header("Здоровье")]
    public int health;
    public int maxHealth;

    [Header("Визуальные еффекты")]
    [SerializeField] private Color damageColor; //Цвет при получении урона
    [SerializeField] protected string destroySound; // Звук смерти
    [SerializeField] protected string hitSound; // Звук получения урона
    
    [Header("События")]
    //Настраевамые поля 
    [SerializeField] protected bool destroyOnDie = true;
    [SerializeField] protected float destroyOffset = 0f;
    //События
    public UnityEvent<float> stun = new UnityEvent<float>();
    public UnityEvent onDie = new UnityEvent();  //Методы которые вызовуться при уничтожении объекта
    public UnityEvent<int, int> onHealthChange = new UnityEvent<int, int>();

    //Другое
    private Coroutine damageInd = null;
    protected EffectsInfo effectManager;
    protected GameManager gameManager;

    //Всякие манипуляции со здоровьем
    public abstract void TakeHit(int damage, float stunDuration = 0f);
    public abstract void Heal(int heal);
    public abstract void SetHealth(int newMaxHealth, int newHealth);

    public IEnumerator TakeHitVizualization()
    {
        gameObject.GetComponent<SpriteRenderer>().color = damageColor;
        yield return new WaitForSeconds(0.6f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(100, 100, 100, 100);
    }
}