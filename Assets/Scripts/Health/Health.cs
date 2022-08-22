using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour
{
    //Показатели здоровья
    [Header("Показатели здоровья")]
    [SerializeField] protected int currentHealth = 60;
    [SerializeField] protected int maxHealth = 60;

    [Header("События")]
    //Настраевамые поля 
    [SerializeField] protected float destroyOffset = 0f;
    //События
    public UnityEvent<int, int> onHealthChange = new UnityEvent<int, int>();
    public UnityEvent<int, int> onTakeHit = new UnityEvent<int, int>();
    public UnityEvent<int, int> onHeal = new UnityEvent<int, int>();
    public UnityEvent onDie = new UnityEvent();  //Методы которые вызовуться при уничтожении объекта

    //Другое
    protected EffectHandler effectHandler;
    private Coroutine damageInd = null;

    //Геттеры
    public EffectHandler EffectHandler => effectHandler;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    //Всякие манипуляции со здоровьем
    public abstract void TakeHit(int damage, float stunDuration = 0f);
    public abstract void Heal(int heal);
    public abstract void SetHealth(int newMaxHealth, int newHealth);
}