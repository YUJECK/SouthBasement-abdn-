using UnityEngine;

[RequireComponent(typeof(PointRotation))]
public class PlayerAttack : MonoBehaviour
{
    public MelleRangeWeapon melleWeapon;
    public MelleRangeWeapon defaultWeapon;
    [HideInInspector] public PointRotation pointRotation;
    public SpriteRenderer weaponSprite;
    private Animator animRange;
    public Transform attackPoint;
    private float nextTime;

    [Header("Attack settings")]
    public LayerMask enemyLayers;
    public float attackRange;
    private float attackRate = 2f;
    public int damage = 2;
    public int damageBoost = 2;
    private bool is_Attack;
    private Vector3 posWhenAttack;

    //Сслыки на другие скрипты
    private PlayerController player;
    private EffectsManager effectsManager;

    private void Start()
    {
        pointRotation = GetComponent<PointRotation>();
        animRange = transform.GetChild(0).GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        effectsManager = FindObjectOfType<EffectsManager>();
        SetToDefault();
    }
    void Update()
    {
        if(Time.time >= nextTime) // Атака крысы
        {
            if (!GameManager.isPlayerStopped & Input.GetMouseButtonDown(0) & !FindObjectOfType<PlayerController>().isSprinting)
            {
                SetNextTime();
                Attack();
            }
            if (Time.time >= nextTime)
                is_Attack = false;
        }
    }
    
    private void Attack()
    {
        is_Attack = true;
        pointRotation.StopRotating(true, 0.8f);
        //Проигрываем анимации
        player.PlayAttackAnimation(melleWeapon.typeOfAttack);
        PlayAttackRangeAnimation(melleWeapon.typeOfAttack);
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.tag == "Enemy" && !enemy.isTrigger)
            {
                HealthEnemy enemyHealth = enemy.GetComponent<HealthEnemy>();
                    
                //Накладываем еффект если есть
                if(melleWeapon != null && melleWeapon.effect != EffectsList.None)
                {
                    //if(melleWeapon.effect == EffectsList.Poisoned)
                    //    effectsManager.GetPoisoned(melleWeapon.effectTime, null, enemyHealth);
                    //if(melleWeapon.effect == EffectsList.Bleed)
                    //    effectsManager.GetBleed(melleWeapon.effectTime, null, enemyHealth);
                    //if(melleWeapon.effect == EffectsList.Burn)
                    //    effectsManager.GetBurn(melleWeapon.effectTime, null, enemyHealth);
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
        attackRange = weapon.attackRange;
        attackRate = weapon.attackRate;
        damage = weapon.damage;
        weaponSprite.sprite = weapon.sprite;
        attackPoint.localScale = new Vector3(10*weapon.attackRange/2, 10*weapon.attackRange/2, 1);
        attackPoint.localPosition = new Vector3(attackPoint.localPosition.x, melleWeapon.lenght, 0f);
    }
    public void SetToDefault(){ SetMelleWeapon(defaultWeapon); }
    public void HideMelleweaponIcon(bool hiding) // Включние, выключение спрайта оружия
    {   
        weaponSprite.gameObject.SetActive(!hiding);
    }
    public void SetNextTime() { nextTime = Time.time + attackRate; }
    
    void OnDrawGizmosSelected()//Отрисовка радиуса атаки
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
