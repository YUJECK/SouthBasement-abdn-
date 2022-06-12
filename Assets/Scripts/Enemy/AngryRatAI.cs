using System.Collections.Generic;
using UnityEngine;

public class AngryRatAI : MonoBehaviour
{
    [Header("Параметры атаки")]
    public int minDamage = 1; //Дамаг 
    public int maxDamage = 1; //Дамаг 
    [SerializeField] private float attackRate = 1f; //Частота атаки
    [SerializeField] private TriggerCheker attackCheker;
    private float nextAttackTime = 0f; //След время атаки


    [Header("Параметры поведения")]
    [SerializeField] private Transform target;
    public Effect stun;
    private bool isNowGoing; //Идет ли враг 
    private bool freezeFlip;
    private bool isFlippedOnRight; //Повернут ли враг напрво
    private TargetType targetMoveType = TargetType.Static; //Подвижный ли таргет
    public TriggerCheker stopCheker;
    public TriggerCheker triggerCheker; //Область в которой враг будет идти за игроком
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
    [SerializeField] private List<EnemyTarget> targets; //Точки для передвижения
    private List<Vector2> path = new List<Vector2>(); //Путь
    private Vector2 lastPos = new Vector2();

    //Ссылки на другие классы
    private Health player; //Ссылка на ХП игрока
    private Animator anim; //Ссылка на аниматор объекта
    private Rigidbody2D rb; //Ссылка на Rigidbody2D объекта
    private Grid grid; //Ссылка на матрицу
    private RoomCloser roomCloser;
    private Pathfinding pathManager; //Ссылка на скрипт отвечающий за поиск пути

    //Методы атаки
    public void Attack()
    {
        if (attackCheker.trigger) //Если игрок находится в радиусе атаки
        {
            //Бьём врага
            player.TakeHit(Random.Range(minDamage, minDamage + 1));
            SetNextAttackTime();
            Debug.Log(gameObject.name + " attack");
        }
        anim.ResetTrigger("IsAttack");
    }
    private void SetNextAttackTime() { nextAttackTime = Time.time + attackRate; }
    public void Stun(float stunTime)
    { stun.durationTime = stunTime; stun.startTime = Time.time; anim.SetBool("isStunned", true); }

    //Методы поведения
    public void SetTarget(Transform target)
    {
        if (grid.isGridCreated && this.target != target)
        {
            this.target = target;
            FindPath(target);
            SetNextSearchTime();
            //Debug.Log("Target: " + target.name + " setted");
        }
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
        if (path.Count != 0) path.Clear();

        path = pathManager.FindPath(
           new Vector2(transform.position.x / grid.nodeSize, transform.position.y / grid.nodeSize),
           new Vector2(target.position.x / grid.nodeSize, target.position.y / grid.nodeSize));
    }
    private Transform FindNewTarget()
    {
        bool isSamePriority = true;
        EnemyTarget target = null;
        int priority = targets[0].priority;

        //Проверяем все таргеты по приоритету
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].priority == priority) continue;
            else //Если приоритет не одинаковый
            {
                isSamePriority = false;
                target = targets[targets.Count - 1];
                break;
            }
        }
        //Если у всех таргетов одинаковый приоритет
        if (isSamePriority)
        {
            int rand = Random.Range(0, targets.Count);
            target = targets[rand];
        }

        if (target != null)
        {
            if (target.targetType == TargetType.Movable)
                speed = runSpeed;
            else speed = walkSpeed;
        }
        else FindObjectOfType<RatConsole>().DisplayText("Таргет не был найден", Color.red,
            RatConsole.Mode.ConsoleMessege, "<AngryRatAI.cs, line 132>");
      
        //Не думаю что сюда код вообще попадет
        return null;
    }
    public void OnAreaExit(GameObject obj) //Метод который будет вызываться при выходе за границу поля зрения врага
    {
        if(obj.TryGetComponent(typeof(EnemyTarget), out Component comp))
        {
            if(targets.Contains(obj.GetComponent<EnemyTarget>()))
            {
                if (target == obj.GetComponent<EnemyTarget>()) ResetTarget();
                targets.Remove(obj.GetComponent<EnemyTarget>());
            }
        }
    }
    public void OnAreaEnter(GameObject obj) //Метод который будет вызываться при входе в поле зрения
    {
        if (obj.TryGetComponent(typeof(EnemyTarget), out Component comp))
        {
            EnemyTarget newTarget = obj.GetComponent<EnemyTarget>();
            if (!targets.Contains(newTarget))
            {
                targets.Add(newTarget);
                QuickSort(targets, 0, targets.Count-1);
                SetTarget(FindNewTarget());
            }
        }
    }

    //Другие методы
    private void Flip() { transform.Rotate(0f, 180f, 0f); }

        //Методы QuickSort-а
        private List<EnemyTarget> QuickSort(List<EnemyTarget> targets, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex) return targets;

            int pivot = GetPivotInd(targets, minIndex, maxIndex);

            QuickSort(targets, minIndex, pivot - 1);
            QuickSort(targets, pivot + 1, maxIndex);

            return new List<EnemyTarget>();
        }
        private int GetPivotInd(List<EnemyTarget> targets, int minIndex, int maxIndex)
        {
            int pivot = minIndex - 1;

            for (int i = minIndex; i <= maxIndex; i++)
            {
                if (targets[i].priority < targets[maxIndex].priority)
                {
                    pivot++;
                    Swap(ref targets, pivot, i);
                }
            }

            pivot++;
            Swap(ref targets, pivot, maxIndex);

            return pivot;
        }
        private void Swap(ref List<EnemyTarget> targets, int firstInd, int secondInd)
        {
            EnemyTarget tmp = targets[firstInd];
            targets[firstInd] = targets[secondInd];
            targets[secondInd] = tmp;
        }

    //Юнитивские методы
    private void Start()
    {
        player = FindObjectOfType<Health>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        grid = FindObjectOfType<Grid>();
        pathManager = GetComponent<Pathfinding>();
        speed = walkSpeed;
        roomCloser = GetComponent<HealthEnemy>().roomCloser;
        GetComponent<HealthEnemy>().stun.AddListener(Stun);
        if(triggerCheker != null)
        {
            triggerCheker.onEnter.AddListener(OnAreaEnter);
            triggerCheker.onExit.AddListener(OnAreaExit);
        }
        SetNextAttackTime();
        if (targets.Count != 0) QuickSort(targets, 0, targets.Count - 1);
    }
    private void Update() //Основная логика
    {
        if (stun.durationTime == 0f)
        {
            //Выбор таргета
            if (target == null && targets.Count != 0) SetTarget(FindNewTarget());

            if (pathManager != null) //Поиск пути
            {
                if (targetMoveType == TargetType.Movable && target != null && (Time.time >= nextSearchTime || path.Count == 0))
                {
                    pathManager.ResetGridChanges();
                    FindPath(target);
                    SetNextSearchTime();
                }

                if (target != null) //Поврот 
                {
                    if (new Vector3(lastPos.x, lastPos.y, transform.position.z) != transform.position && !freezeFlip)
                    {
                        if (lastPos.x < transform.position.x && isFlippedOnRight)
                        {
                            Flip();
                            isFlippedOnRight = false;
                        }
                        else if (lastPos.x > transform.position.x && !isFlippedOnRight)
                        {
                            Flip();
                            isFlippedOnRight = true;
                        }
                    }

                    lastPos = transform.position;
                }
            }
            if (anim != null)//Анимация и атака
            {
                if (attackCheker.trigger && Time.time >= nextAttackTime) anim.SetTrigger("isAttack"); //Атака

                if (isNowGoing) anim.SetBool("isRun", true); //Ходьба
                else anim.SetBool("isRun", false);
            }
            if (triggerCheker.trigger && triggerCheker.obj.TryGetComponent(typeof(EnemyTarget), out Component newTarget))
            {
                if (!targets.Contains(triggerCheker.obj.GetComponent<EnemyTarget>()))
                {
                    targets.Add(triggerCheker.obj.GetComponent<EnemyTarget>());
                    QuickSort(targets, 0, targets.Count - 1);
                    FindNewTarget();
                }
            }
        }
        else if (Time.time - stun.startTime >= stun.durationTime) { stun.durationTime = 0f; anim.SetBool("isStunned", false); }
    }
    private void FixedUpdate() //Физическая логика
    {
        //Сброс velocity
        if (rb != null) rb.velocity = Vector2.zero;

        if (stun.durationTime == 0f)
        {
            //Движение 
            if (path.Count != 0 && stopCheker != null && !stopCheker.trigger)
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
            else if (path.Count == 0)
            {
                if (targetMoveType != TargetType.Movable) ResetTarget();
                isNowGoing = false;
            }
        }
    }

    //Проверка триггеров/колизий
    private void OnCollisionStay2D(Collision2D collision) { freezeFlip = true; }
    private void OnCollisionExit2D(Collision2D collision) { freezeFlip = false; }
}