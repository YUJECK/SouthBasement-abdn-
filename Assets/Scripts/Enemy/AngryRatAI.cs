using System.Collections.Generic;
using UnityEngine;

public class AngryRatAI : MonoBehaviour
{
    [Header("Параметры атаки")]
    public int damage = 1; //Дамаг 
    [SerializeField] private float attackRate = 1f; //Частота атаки
    [SerializeField] private TriggerCheker attackCheker; 
    private float nextAttackTime = 0f; //След время атаки


    [Header("Параметры поведения")]
    [SerializeField] private Transform target;
    private bool isNowGoing; //Идет ли враг 
    private bool isFlippedOnRight; //Повернут ли враг напрво
    private bool isTargetCanWalk; //Подвижный ли таргет
    public TriggerCheker stopCheker;
    private float _speed; //Скорость передвижения вр крысы
    public float speed
    {
        get { return _speed; }
        set 
        {
            if (value <= 0f)
            {
                _speed = 1;
                return;
            }
            if (value > 8f)
            {
                _speed = 8;
                return;
            }
            _speed = value;
        }
    
    }
    [SerializeField] private float walkSpeed = 2f; //Скорость при ходьбе
    [SerializeField] private float runSpeed = 3.3f; //Скорость при беге
    [SerializeField] private float searchRate = 3f; //Частота поиска
    private float nextSearchTime = 0f; //След время поиска
    [SerializeField] private Transform[] targetPoints; //Точки для передвижения
    private List<Vector2> path = new List<Vector2>(); //Путь

    //Ссылки на другие классы
    private Health player; //Ссылка на ХП игрока
    [SerializeField] private Animator anim; //Ссылка на аниматор объекта
    private Rigidbody2D rb; //Ссылка на Rigidbody2D объекта
    private Grid grid; //Ссылка на матрицу
    private Pathfinding pathManager; //Ссылка на скрипт отвечающий за поиск пути

    //Методы атаки
    public void Attack()
    {
        if (attackCheker.isOnTrigger) //Если игрок находится в радиусе атаки
        {
            //Бьём врага
            player.TakeHit(damage);
            SetNextAttackTime();
        }
        anim.ResetTrigger("IsAttack");
    }
    private void SetNextAttackTime() { nextAttackTime = Time.time + attackRate; Debug.Log(nextAttackTime); }
    
   
    //Методы поведения
    public void SetTarget(Transform target)
    {
        this.target = target;
        FindPath(target);
        SetNextSearchTime();
    }
    public void ResetTarget()
    {
        target = null;
        path.Clear();
        pathManager.ResetGridChanges();
    }
    private void SetNextSearchTime() { nextSearchTime = Time.time + searchRate; }
    private void FindPath(Transform target)
    {
        path = pathManager.FindPath(
           new Vector2(transform.position.x / grid.nodeSize, transform.position.y / grid.nodeSize),
           new Vector2(target.position.x / grid.nodeSize, target.position.y / grid.nodeSize));
    }

    //Другие методы
    private void Flip() { transform.Rotate(0f, 180f, 0f); }

    
    //Юнитивские методы
    private void Start()
    {
        player = FindObjectOfType<Health>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        grid = FindObjectOfType<Grid>();
        pathManager = GetComponent<Pathfinding>();
        speed = walkSpeed;
        SetNextAttackTime();
    }
    private void Update() //Основная логика
    {
        if (anim != null)//Анимация
        {
            if (attackCheker.isOnTrigger && Time.time >= nextAttackTime) anim.SetTrigger("IsAttack"); //Атака
            
            if (isNowGoing) anim.SetBool("IsRun", true); //Ходьба
            else anim.SetBool("IsRun", false);
        }
        if(pathManager != null) //Поиск пути
        {
            if (isTargetCanWalk && target != null && (Time.time >= nextSearchTime || path.Count == 0))
            {
                pathManager.ResetGridChanges();
                FindPath(target);
                SetNextSearchTime();
            }
            //Если нет таргета, то мы ставим таргетом одну из точек
            else if (target == null && targetPoints.Length != 0)
            {
                SetTarget(targetPoints[Random.Range(0, targetPoints.Length)]);
                isTargetCanWalk = false;
            }
            
            if(target != null) //Поврот 
            {
                if (target.position.x > transform.position.x && isFlippedOnRight)
                {
                    Flip();
                    isFlippedOnRight = false;
                }
                if (target.position.x < transform.position.x && !isFlippedOnRight)
                {
                    Flip();
                    isFlippedOnRight = true;
                }
            }
        }
    }
    private void FixedUpdate() //Физическая логика
    {
        //Сброс velocity
        if (rb != null) rb.velocity = Vector2.zero;

        //Движение 
        if (path.Count != 0 && stopCheker != null && !stopCheker.isOnTrigger)
        {
            transform.position = Vector2.MoveTowards(transform.position, path[0], speed * Time.deltaTime);
            isNowGoing = true;

            if (transform.position == new Vector3(path[0].x, path[0].y, transform.position.z))
            {
                path.RemoveAt(0);
                //Убираем коллайдер пути
                if (pathManager.gridChanges.Count != 0)
                {
                    grid.grid[pathManager.gridChanges[0].x, pathManager.gridChanges[0].y] = 0;
                    pathManager.gridChanges.RemoveAt(0);
                }
            }
        }
        else
        {
            if(!isTargetCanWalk) ResetTarget();
            isNowGoing = false;
        }
    }

    //Проверка триггеров
    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            if(target != coll.transform)
            {
                SetTarget(coll.transform);
                isTargetCanWalk = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
            ResetTarget();
    }
}