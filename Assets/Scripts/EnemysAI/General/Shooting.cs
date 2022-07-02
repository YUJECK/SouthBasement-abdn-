using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //�����
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

    //������
    [System.Serializable]
    public class Bullet
    {
        public BulletsType bulletsType = BulletsType.Limited; //������������ ���-�� ���� ��� ���
        public GameObject projectile; //���� ����
        public float bulletSpeed; //�������� ������ ����
        public float bulletChance = 100f; //���� ������ ����(��� ����� ���� � ����� �� �����������)
        public int bulletCount; //����� ����(��� ����������� ���-�� ���� �� �����������)
    }
    [System.Serializable]
    public class Pattern
    {
        public float patternChance = 100f;
        public ShootingPattern pattern;
    }

    [Header("���������")]
    private Transform firePoint;
    public UsageParameters shootingController = UsageParameters.Independently;
    public Patterns patternsUsage = Patterns.UsePatterns;

    [Header("")]
    //��� ���������
    private float fireRate = 1f;
    private List<Bullet> bullets = new List<Bullet>();
    //C ���������
    public List<Pattern> patternsList = new List<Pattern>();
    public Pattern currentPattern;
    
    private Pattern FindNewPattern()
    {
        float chance = Random.Range(0f, 100.1f);
        List<Pattern> patternsInChance = new List<Pattern>();
        
        foreach(Pattern pattern in patternsList)
        {
            if (pattern.patternChance <= chance)
                patternsInChance.Add(pattern);
        }

        return patternsInChance[Random.Range(0, patternsInChance.Count)];
    }
    
    private void Start()
    {
        currentPattern = FindNewPattern();
    }
    private void Update()
    {
        if (patternsUsage == Patterns.UsePatterns && patternsList.Count != 0)
        {
            currentPattern = FindNewPattern();
            currentPattern.pattern.StartPattern();
        }
    }
}