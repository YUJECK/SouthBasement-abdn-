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
    public UsageParameters shootingController = UsageParameters.Independently; //Откуда будет осущетвляться стрельба(тут/в другом скрипте)
    public ForceMode2D forceMode = ForceMode2D.Impulse; //Форс мод при стельбе
    public Patterns patternsUsage = Patterns.UsePatterns; //Просто стрелять или использовать паттерны атак

    //Без паттернов
    [Header("Настройка обычной стрельбы")]
    [SerializeField] private List<Bullet> bulletsList = new List<Bullet>();
    [SerializeField] private float fireRate = 1f; //Скорость стрельбы
    private float nextTime = 0f; //След время для стерльбы

    //C паттерами
    [Header("Настройка паттернов")]
    public List<Pattern> patternsList = new List<Pattern>(); //Лист паттернов
    public float patternUseRate = 0.5f; //Частота использования паттернов
    [HideInInspector] public ShootingPattern currentPattern; //Паттерн который сейчас активен

    [Header("Другое")]
    [SerializeField] private Transform firePoint; //Точка спавна пуль
    //Ссылки на другие скрипты
    private PointRotation pointRotation;

    //Паттерны
    private Pattern FindNewPattern()
    {
        float chance = Random.Range(0f, 100f);
        List<Pattern> patternsInChance = new List<Pattern>();

        foreach (Pattern pattern in patternsList)
        {
            if (pattern.patternChance >= chance)
                patternsInChance.Add(pattern);
        }

        if (patternsInChance.Count == 0) Debug.Log(chance);
        return patternsInChance[Random.Range(0, patternsInChance.Count)];
    }
    private void SetNewPattern()
    {
        if (currentPattern != null && currentPattern.isWork) currentPattern.StopPattern(this);
        currentPattern = FindNewPattern().pattern;
        currentPattern = Instantiate(currentPattern);
        currentPattern.onExit.AddListener(SetNewPattern);
        UnityAction<Shooting> startMethod = currentPattern.StartPattern;
        Utility.InvokeMethod<Shooting>(startMethod, this, patternUseRate);
    }

    //Пули
    private int FindBullet()
    {
        float chance = Random.Range(0f, 100f);
        List<Bullet> bulletsInChance = new List<Bullet>();

        foreach (Bullet bullet in bulletsList)
        {
            if (bullet.bulletChance >= chance)
                bulletsInChance.Add(bullet);
        }

        if (bulletsInChance.Count == 0) Debug.Log(chance);
        return Random.Range(0, bulletsList.Count);
    }

    //Основные методы
    public void Shoot(GameObject projectile, float offset, float speed)
    {
        pointRotation.offset = offset;
        if (forceMode == ForceMode2D.Force) speed *= 30;
        GameObject _projectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = _projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * speed, forceMode);
    }
    public void StopCurrentPattern() { if (currentPattern != null) currentPattern.StopPattern(this); }
    
    //Юнитивские методы
    private void Start()
    {
        if(patternsUsage == Patterns.UsePatterns && shootingController == UsageParameters.Independently) SetNewPattern();
        pointRotation = GetComponent<PointRotation>();
    }
    private void Update()
    {
        if(shootingController == UsageParameters.Independently)
        {
            if(patternsUsage == Patterns.DontUsePatterns && Time.time >= nextTime)
        {
            int bulletInd = FindBullet();
            Shoot(bulletsList[bulletInd].projectile, 0f, bulletsList[bulletInd].bulletSpeed);
            if (bulletsList[bulletInd].bulletsType == BulletsType.Limited)
            {
                bulletsList[bulletInd].bulletCount--;
                if (bulletsList[bulletInd].bulletCount <= 0) bulletsList.RemoveAt(bulletInd);
            }
            nextTime = Time.time + fireRate;
        }
        }
    }
}