using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PointRotation))]
public class Shooting : MonoBehaviour
{
    //Энамы
    public enum UsageParameters
    {
        FromOtherScript,
        Independently
    }
    public enum BulletsType
    {
        Limited,
        Unlimited
    }
    public enum Patterns
    {
        UsePatterns,
        DontUsePatterns
    }

    //Классы
    [System.Serializable] public class Bullet
    {
        public BulletsType bulletsType = BulletsType.Limited; //Ограниченное кол-во пуль или нет
        public GameObject projectile; //Сама пуля
        public float bulletSpeed; //Скорость полета пули
        public float bulletChance = 100f; //Шанс выбора пули(при одной пули в листе не учитывается)
        public int bulletCount; //Кол-во пуль(При бесконечном кол-ве пуль не учитывается)
    }
    [System.Serializable] public class Pattern
    {
        public float patternChance = 100f;
        [SerializeField] public ShootingPattern pattern;
    }

    [Header("Настройки")]
    [SerializeField] private Transform firePoint; //Точка спавна пуль
    public UsageParameters shootingController = UsageParameters.Independently; //Откуда будет осущетвляться стрельба(тут/в другом скрипте)
    public ForceMode2D forceMode = ForceMode2D.Impulse; //Форс мод при стельбе
    public Patterns patternsUsage = Patterns.UsePatterns; //Просто стрелять или использовать паттерны атак

    //Без паттернов
    [Header("")]
    [SerializeField] private float fireRate = 1f; //Скорость стрельбы
    [SerializeField] private List<Bullet> bullets = new List<Bullet>();
    
    //C паттерами
    [Header("")]
    public List<Pattern> patternsList = new List<Pattern>(); //Лист паттернов
    public float patternUseRate = 0.5f; //Частота использования паттернов
    public Pattern currentPattern; //Паттерн который сейчас активен

    //Ссылки на другие скрипты
    private PointRotation pointRotation;

    //Паттерны
    private Pattern FindNewPattern()
    {
        float chance = Random.Range(0f, 100f);
        List<Pattern> patternsInChance = new List<Pattern>();
        
        foreach(Pattern pattern in patternsList)
        {
            if (pattern.patternChance >= chance)
                patternsInChance.Add(pattern);
        }

        if(patternsInChance.Count == 0) Debug.Log(chance);
        return patternsInChance[Random.Range(0, patternsInChance.Count)];
    }
    private void SetNewPattern()
    {
        if (currentPattern != null && currentPattern.pattern.isWork) currentPattern.pattern.StopPattern(this); 
        currentPattern = FindNewPattern();
        currentPattern.pattern = Instantiate(currentPattern.pattern);
        currentPattern.pattern.onExit.AddListener(SetNewPattern);
        UnityAction<Shooting> startMethod = currentPattern.pattern.StartPattern;
        Utility.InvokeMethod<Shooting>(startMethod, this, patternUseRate);
    }

    //Основные методы
    public void Shoot(GameObject projectile, float offset, float speed)
    {
        pointRotation.offset = offset;
        GameObject _projectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = _projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * speed, forceMode);
    }
    public void StopCurrentPattern() => currentPattern.pattern.StopPattern(this);
    
    //Юнитивские методы
    private void Start()
    {
        SetNewPattern();
        pointRotation = GetComponent<PointRotation>();
    }
    private void Update()
    {
        if(patternsUsage == Patterns.DontUsePatterns)
        {

        }
    }
}