using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Movement")]

    //Мувмент крысы
    private bool isStopped = false;
    public Vector2 movement;
    public float speed = 5f; // Скорость игрока
    private float dashTime = 0f; // Длина рывка
    [SerializeField] private Vector2 movementOnDash; // Напрвление рывка
    public float dashDuration = 5f; // Скорость рывка
    [SerializeField] private float dashRate = 3f; //Скорость перезарядки рывка
    private float dashNextTime = 0f; //То когда сможет игрок сделать рывок
    [SerializeField] private float dashSpeed = 5f; // Скорость рывка
    [SerializeField] private float sprint = 0.35f; //Процент увеличения скорости при спринте

    [HideInInspector]   
    public bool isSprinting; // Спринтит ли игрок
    private float boostSpeed; // Новая скорость при спринте
    private float normalSpeed = 5f; // Обычная скорость
    
    //Графика и компонеты
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer playerSpiteRend; // Спрайт игрока
    private bool flippedOnRight = false; // Повернут ли игрок направо
    private Rotation rotation;

    //Ссылки на другие скрипты
    private RatAttack ratAttack;
    private AudioManager audioManager;
    public static Player instance; // Синглтон

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerSpiteRend = GetComponent<SpriteRenderer>();
        GameManager.isActiveAnyPanel = false;
        normalSpeed = speed;
        ratAttack = FindObjectOfType<RatAttack>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnLevelWasLoaded(int level)
    {
        //Перемещение игрока на стартовую позицию
        transform.position = GameObject.FindWithTag("StartPoint").GetComponent<Transform>().position;
    }
    
    private void Update()
    {   
        if (!GameManager.isActiveAnyPanel)
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");

            //Анимация игрока
            if(movement.x != 0 || movement.y != 0)
                anim.SetBool("Is_Run", true);
                
            else if(movement.x == 0 && movement.y == 0)
                anim.SetBool("Is_Run", false);
            
            //Написать рывок
            
            //Спринт
            if((!isSprinting & Input.GetKeyDown(KeyCode.LeftControl))&(movement.x != 0 || movement.y != 0))
                Sprint();
            if(isSprinting & Input.GetKeyUp(KeyCode.LeftControl))
                Sprint();

                
            //Рывок
            if(Input.GetMouseButtonDown(1) && !isSprinting && dashTime == 0f && dashNextTime <= Time.time)
            {
                dashTime = dashDuration;
                
                movementOnDash = Vector2.zero;

                if(movement.x > 0) movementOnDash.x = 1f;
                if(movement.x < 0) movementOnDash.x = -1f;
                
                if(movement.y > 0) movementOnDash.y = 1f;
                if(movement.y < 0) movementOnDash.y = -1f;
                
                dashNextTime = Time.time + dashRate;
            }   
        }
        else
            anim.SetBool("Is_Run", false);

    }
    private void FixedUpdate()
    {
        if (!GameManager.isActiveAnyPanel)
        {
            if(!isStopped)
            {
                //Движение игрока
                rb.velocity = new Vector2(movement.x, movement.y) * speed;
                // audioManager.PlayClip("RatWalk");
                
                //Рывок
                if(dashTime > 0f) Dashing();
                else dashTime = 0f;
            }
            else
                rb.velocity = Vector2.zero;


            //Поворот спрайта игрока
            if (movement.x > 0) 
                rotation = Rotation.Right;
            if (movement.x < 0)
                rotation = Rotation.Left;

            if (rotation == Rotation.Right & !flippedOnRight)
                Flip();
            if (rotation == Rotation.Left & flippedOnRight)
                Flip();  
        }   
    }
    
    public void PlayAttackAnimation(TypeOfAttack type)
    {
        if (type == TypeOfAttack.Pinpoint) anim.SetTrigger("AttackPinpoint");
        if (type == TypeOfAttack.Wide) anim.SetTrigger("AttackWide");
        if (type == TypeOfAttack.Above) anim.SetTrigger("AttackAbove");
    }
    private void Sprint()
    {
        if(!isSprinting)
        {
            boostSpeed = speed + speed*sprint;
            normalSpeed = speed;
            speed = boostSpeed;
            anim.SetBool("Is_Sprint", true);
            ratAttack.HideMelleweaponIcon(true);
            isSprinting = true;
        }
        else
        {
            speed = normalSpeed;
            anim.SetBool("Is_Sprint", false);
            ratAttack.HideMelleweaponIcon(false); 
            isSprinting = false;
        }
    }
    private void Dashing() 
    { 
        rb.velocity = new Vector2(movement.x, movement.y) * (speed + dashSpeed); 
        dashTime -= 0.1f;
    }
    public void BoostSpeed(float speedBoost) {speed = speed + speed * speedBoost;} // Ускорение игрока
    public void SetStop(bool active) { isStopped = active; }
    void Flip()
    {
        flippedOnRight = !flippedOnRight;  
        transform.Rotate(0f,180f,0f);
        ratAttack.pointRotation.coefficient *= -1f;
        ratAttack.AttackPoint.GetComponent<SpriteRenderer>().flipY = !ratAttack.AttackPoint.GetComponent<SpriteRenderer>().flipY;
    }
    
    enum Rotation { Left, Right }
}   