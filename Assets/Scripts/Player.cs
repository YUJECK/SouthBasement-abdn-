using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Movement")]

    //Мувмент крысы
    public bool isStopped;
    private Vector2 movement;
    public float speed = 5; // Скорость игрока
    public float sprint = 0.35f; //Процент увеличения скорости при спринте

    [HideInInspector]   
    public bool isSprinting; // Спринтит ли игрок
    private float boostSpeed; // Новая скорость при спринте
    private float normalSpeed; // Обычная скорость
    
    //Графика и компонеты
    public Collider2D sprintColl; //Коллайдер при спринте
    public Collider2D normalColl; // кеоллайдер при ходьбе
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer playerSpiteRend; // Спрайт игрока
    private bool flippedOnRight = false; // Повернут ли игрок направо
    private Rotation rotation;


    //Ссылки на другие скрипты
    private RatAttack ratAttack;
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
            if((Input.GetKeyDown(KeyCode.LeftControl))&(movement.x != 0 || movement.y != 0))
                Sprint(true);
            if(Input.GetKeyUp(KeyCode.LeftControl))
                Sprint(false);
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
                rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
            }

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

    private void Sprint(bool _isSprinting)
    {
        if(_isSprinting)
        {
            boostSpeed = speed + speed*sprint;
            normalSpeed = speed;
            speed = boostSpeed;
            anim.SetBool("Is_Sprint", true);
            sprintColl.enabled = true;
            normalColl.enabled = false;
            ratAttack.HideMelleweaponIcon(true);
            isSprinting = true;
        }
        else
        {
            speed = normalSpeed;
            sprintColl.enabled = false;
            normalColl.enabled = true;
            anim.SetBool("Is_Sprint", false);
            ratAttack.HideMelleweaponIcon(false); 
            isSprinting = false;
        }
    }
    void Flip()
    {
        flippedOnRight = !flippedOnRight;  
        transform.Rotate(0f,180f,0f);
    }
    
    enum Rotation
    {
        Left,
        Right
    }
}   