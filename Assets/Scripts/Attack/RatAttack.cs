using UnityEngine;
using System.Collections.Generic;

public class RatAttack : MonoBehaviour
{
    public MelleRangeWeapon melleWeapon;
    public MelleRangeWeapon defaultWeapon;
    private PointRotation pointRotation;
    public SpriteRenderer weaponSprite;
    private Animator animRange;
    public Transform AttackPoint;
    private float nextTime;

    [Header("Attack settings")]
    public LayerMask EnemyLayers;
    public float AttackRange;
    private float attackRate = 2f;
    public int damage = 2;
    public int damageBoost = 2;
    private bool is_Attack;
    private Vector3 posWhenAttack;

    //Сслыки на другие скрипты
    private Player player;
    private EffectsManager effectsManager;

    private void Start()
    {
        pointRotation = FindObjectOfType<PointRotation>();
        animRange = transform.GetChild(0).GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        effectsManager = FindObjectOfType<EffectsManager>();
        SetToDefault();
    }
    void Update()
    {
        if(Time.time >= nextTime) // Атакак крысы
        {
            if (Input.GetMouseButtonDown(0) & !FindObjectOfType<Player>().isSprinting)
            {
                Attack();
                // cursor.CursorClick();
                SetNextTime();
            }
            
            is_Attack = false;
            pointRotation.StopRotating(false);
        }
    }
    
    private void Attack()
    {
        is_Attack = true;
        pointRotation.StopRotating(true);
        //Проигрываем анимации
        player.PlayAttackAnimation(melleWeapon.typeOfAttack);
        PlayAttackRangeAnimation(melleWeapon.typeOfAttack);
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.tag == "Enemy" && !enemy.isTrigger)
            {
                HealthEnemy enemyHealth = enemy.GetComponent<HealthEnemy>();
                    
                //Накладываем еффект если есть
                if(melleWeapon != null && melleWeapon.effect != EffectsList.None)
                {
                    if(melleWeapon.effect == EffectsList.Poisoned)
                        effectsManager.GetPoisoned(melleWeapon.effectTime, null, enemyHealth);
                    if(melleWeapon.effect == EffectsList.Bleed)
                        effectsManager.GetBleed(melleWeapon.effectTime, null, enemyHealth);
                    if(melleWeapon.effect == EffectsList.Burn)
                        effectsManager.GetBurn(melleWeapon.effectTime, null, enemyHealth);
                }
                //Наносим урон
                enemy.GetComponent<HealthEnemy>().TakeHit(damage+damageBoost, melleWeapon.stunTime);
            }
        }
    }
    private void PlayAttackRangeAnimation(TypeOfAttack type)
    {
        if (type == TypeOfAttack.Pinpoint) animRange.SetTrigger("AttackPinpoint");
        if (type == TypeOfAttack.Wide) animRange.SetTrigger("AttackWide");
        if (type == TypeOfAttack.Above) animRange.SetTrigger("AttackAbove");
    }
    public void SetMelleWeapon(MelleRangeWeapon weapon)
    {
        melleWeapon = weapon;
        AttackRange = weapon.attackRange;
        attackRate = weapon.attackRate;
        damage = weapon.damage;
        weaponSprite.sprite = weapon.sprite;
        AttackPoint.localScale = new Vector3(10*weapon.attackRange/2, 10*weapon.attackRange/2, 1);
        AttackPoint.localPosition = new Vector3(AttackPoint.localPosition.x, melleWeapon.lenght, 0f);
    }
    public void SetToDefault(){ SetMelleWeapon(defaultWeapon); }
    public void HideMelleweaponIcon(bool hiding) // Включние, выключение спрайта оружия
    {   
        weaponSprite.gameObject.SetActive(!hiding);
    }
    public void SetNextTime() { nextTime = Time.time + attackRate; }
    
    void OnDrawGizmosSelected()//Отрисовка радиуса атаки
    {
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
