using System.Collections.Generic;
using UnityEngine;

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
        public int bulletCount; //Колво пуль(При бесконечном кол-ве пуль не учитывается)
    }
    [System.Serializable] public class Pattern
    {
        public float patternChance = 100f;
        public ShootingPattern pattern;
    }

    [Header("Настройки")]
    public UsageParameters shootingController = UsageParameters.Independently;
    public Patterns patternsUsage = Patterns.UsePatterns;

    [Header("")]
    //Без паттернов
    private float fireRate = 1f;
    private List<Bullet> bullets = new List<Bullet>();
    //C паттерами
    public List<Pattern> patternsList = new List<Pattern>();

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
}