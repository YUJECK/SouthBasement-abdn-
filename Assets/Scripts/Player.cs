using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 movement;
    public float speed = 5;
    public float sprint = 0.35f;

    [HideInInspector]   
    public bool isSprinting;
    private float boostSpeed;
    private float normalSpeed;
    
    //Items
    [Header("")]
    public Item[] weapons;
    private int activeWeapon = 0;
    
    //Графика и компонеты
    public Collider2D sprintColl;
    public Collider2D normalColl;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer Sp;
    private bool flippedOnRight = false;
    private Rotation rotation;

    public static Player instance;

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