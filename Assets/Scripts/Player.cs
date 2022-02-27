using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    private Vector2 movement;
    public float speed = 5; // Скорость игрока
    public float sprint = 0.35f; //Процент увеличения скорости при спринте

    [HideInInspector]   
    public bool isSprinting; // Спринтит ли игрок
    private float boostSpeed; // Новая скорость при спринте
    private float normalSpeed; // Обычная скорость
    
    //Items
    [Header("")]
    public Item[] weapons; // Список озужия у игрока
    private int activeWeapon = 0; // Какой предмет сейчас используется
    
    //Графика и компонеты
    public Collider2D sprintColl; //Коллайдер при спринте
    public Collider2D normalColl; // кеоллайдер при ходьбе
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer Sp;
    private bool flippedOnRight = false; // Повернут ли игрок направо
    private Rotation rotation;

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
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Sp = GetComponent<SpriteRenderer>();
        GameManager.isActiveAnyPanel = false;
        normalSpeed = speed;
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

            Animation();

            //Использование активного предмета
            if (Input.GetMouseButtonDown(1) & !isSprinting)
            {
                if(weapons[activeWeapon] != null)
                {
                    weapons[activeWeapon].PassiveEffect();
                    Debug.Log("Fire");
                }
            }
            
            //Написать рывок
            
            //Спринт
            if((Input.GetKeyDown(KeyCode.LeftControl))&(movement.x != 0 || movement.y != 0))
            {
                boostSpeed = speed + speed*sprint;
                normalSpeed = speed;
                speed = boostSpeed;
                anim.SetBool("Is_Sprint", true);
                sprintColl.enabled = true;
                normalColl.enabled = false;
                isSprinting = true;
            }
            if(Input.GetKeyUp(KeyCode.LeftControl))
            {
                speed = normalSpeed;
                sprintColl.enabled = false;
                normalColl.enabled = true;
                anim.SetBool("Is_Sprint", false);
                isSprinting = false;
            }
        }
        else
            anim.SetBool("Is_Run", false);

    }
    private void FixedUpdate()
    {
        if (!GameManager.isActiveAnyPanel)
        {
            //Движение игрока
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

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
    private void Animation()
    {
        //Анимация игрока
        if(movement.x != 0 || movement.y != 0)
            anim.SetBool("Is_Run", true);
            
        else if(movement.x == 0 && movement.y == 0)
            anim.SetBool("Is_Run", false);
    }
    void Flip()
    {
        flippedOnRight = !flippedOnRight;  
        transform.Rotate(0f,180f,0f);
        FindObjectOfType<PointRotation>().Flip();
    }

    enum Rotation
    {
        Left,
        Right
    }
}   