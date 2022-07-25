using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EnemysAI
{
    [RequireComponent(typeof(PointRotation))]
    public class Shooting : MonoBehaviour
    {
        //Энамы для переключений у кастомного инспектора
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
            [SerializeField] private ShootingPattern shootingPattern; //Сам паттерн
            
            [Header("Настройки использования паттерна")]
            [SerializeField] private int chance; //Шанс использования этого паттерна
            [SerializeField] private float useRate; //Время ожидания после использования этого паттерна
            
            [Header("События")]
            public UnityEvent onEnter; //При активации этого паттерна
            public UnityEvent onExit; //Во время завершения этого паттерна

            //Геттеры и методы взаимодействия с паттерном
            public int GetChance() { return chance; }
            public float GetUseRate() { return useRate; }
            public bool GetIsWork() { if (shootingPattern != null) return shootingPattern.isWork; else return false; }

            public void StartPattern(Shooting shooting) { if (shootingPattern) shootingPattern.StartPattern(shooting); }
            public void StopPattern(Shooting shooting) { if (shootingPattern) shootingPattern.StartPattern(shooting); }
            public void ActivePattern()
            {
                shootingPattern = Instantiate(shootingPattern);

                shootingPattern.onEnter = onEnter;
                shootingPattern.onExit = onExit;
            }
        
        }

        //Переменные
        [Header("Настройки")]
        public UsageParameters shootingController = UsageParameters.Independently; //Откуда будет осущетвляться стрельба(тут/в другом скрипте)
        public ForceMode2D forceMode = ForceMode2D.Impulse; //Форс мод при стельбе
        public Patterns patternsUsage = Patterns.UsePatterns; //Просто стрелять или использовать паттерны атак

        //Без паттернов
        [Header("Настройка обычной стрельбы")]
        [SerializeField] private List<Bullet> bulletsList = new List<Bullet>();
        public UnityEvent onFire = new UnityEvent();
        [SerializeField] private float fireRate = 1f; //Скорость стрельбы
        private float nextTime = 0f; //След время для стерльбы

        //C паттерами
        [Header("Настройка паттернов")]
        public List<Pattern> patternsList = new List<Pattern>(); //Лист паттернов
        public float patternUseRate = 0.5f; //Частота использования паттернов
        [HideInInspector] public Pattern currentPattern; //Паттерн который сейчас активен

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
                if (pattern.GetChance() >= chance)
                    patternsInChance.Add(pattern);
            }

            if (patternsInChance.Count == 0) Debug.Log(chance);
            return patternsInChance[Random.Range(0, patternsInChance.Count)];
        }
        private void SetNewPattern()
        {
            if (currentPattern != null && currentPattern.GetIsWork()) currentPattern.StopPattern(this);
            
            currentPattern = FindNewPattern();
            currentPattern.ActivePattern();
        }
        private IEnumerator ActivatePatterns()
        {
            float patternUseRate = 2f;
            
            while(true)
            {
                SetNewPattern();
                patternUseRate = currentPattern.GetUseRate();
                currentPattern.StartPattern(this);
                yield return new WaitForSeconds(patternUseRate);
            }
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

        //Методы взаимодействия со скриптом
        public void Shoot(GameObject projectile, float offset, float speed)
        {
            if (projectile != null && firePoint != null)
            {
                pointRotation.offset = offset;
                if (forceMode == ForceMode2D.Force) speed *= 30;
                GameObject _projectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = _projectile.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.up * speed, forceMode);
                onFire.Invoke();
            }
        }
        public void StopCurrentPattern() { if (currentPattern != null) currentPattern.StopPattern(this); }

        //Юнитивские методы
        private void Start()
        {
            pointRotation = GetComponent<PointRotation>();
            if (patternsUsage == Patterns.UsePatterns && shootingController == UsageParameters.Independently)
                StartCoroutine(ActivatePatterns());
        }
        private void Update()
        {
            if (shootingController == UsageParameters.Independently)
            {
                if (patternsUsage == Patterns.DontUsePatterns && Time.time >= nextTime)
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
}