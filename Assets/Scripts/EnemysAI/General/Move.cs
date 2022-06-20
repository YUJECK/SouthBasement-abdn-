using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Pathfinding))]
[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    [Header("Параметры передвижения")]
    [SerializeField] private float _speed = 3; // Скорость передвижения 
    [SerializeField] private float searchRate = 1f; // Частота репоиска 
    private float nextSearchTime = 0f; 
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
    [HideInInspector] public bool isNowWalk; //Идет ли сейчас 
    public bool isStopped = false; //Остановлен ли
    private List<Vector2> path = new List<Vector2>(); //Путь
    private EnemyTarget target; //Таргет

    [Header("События")]
    public UnityEvent onFlip = new UnityEvent();

    //Переменные для поворота
    private bool flippedOnRight;
    private Vector2 lastPos;

    //Ссылки на другие скрипты
    [Header("Другое")]
    [SerializeField] private TriggerCheker stopCheker;
    [SerializeField] private TargetSelection targetSelection;
    private Pathfinding pathfinding;
    private Grid grid;
    private Rigidbody2D rb;

    //Методы поиска пути
    public void FindNewPath(EnemyTarget target)
    {
        if (path.Count != 0) ResetTarget();
        this.target = target;   
        path = pathfinding.FindPath(
           new Vector2(transform.position.x / grid.nodeSize, transform.position.y / grid.nodeSize),
           new Vector2(target.transform.position.x / grid.nodeSize, target.transform.position.y / grid.nodeSize));
    }
    public void ResetTarget(EnemyTarget target = null)
    {
        if (target != null && target == this.target) { this.target = null; } 
        path.Clear();
        pathfinding.ResetGridChanges();
    }
    private void SetNextSearchTime() { nextSearchTime = Time.time + searchRate; }

    //Метод поворота    
    private void FlipObject() { if (!isStopped) { transform.Rotate(0f, 180f, 0f); onFlip.Invoke(); } }
    public void FlipOther(Transform _transform) { _transform.Rotate(180f, 0f, 0f); } 
    //Юнитивские методы
    private void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
        rb = GetComponent<Rigidbody2D>();
        grid = FindObjectOfType<Grid>();
        targetSelection.onTargetChange.AddListener(FindNewPath);
        targetSelection.onResetTarget.AddListener(ResetTarget);

        isStopped = false;
    }
    private void FixedUpdate() //Физическая логика
    {
        //Сброс velocity
        if (rb != null) rb.velocity = Vector2.zero;
        if(!isStopped)
        {
            //Динамичный поиск пути
            if (target != null && target.targetMoveType == TargetType.Movable && (Time.time >= nextSearchTime || path.Count == 0))
            {
                ResetTarget();
                FindNewPath(target);
                SetNextSearchTime();
            }

            //Поворот
            if (new Vector3(lastPos.x, lastPos.y, transform.position.z) != transform.position && isNowWalk)
            {
                if (lastPos.x < transform.position.x && flippedOnRight)
                {
                    FlipObject();
                    flippedOnRight = false;
                }
                else if (lastPos.x > transform.position.x && !flippedOnRight)
                {
                    FlipObject();
                    flippedOnRight = true;
                }
            }
            lastPos = transform.position;
            //Движение 
            if (path.Count != 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, path[0], speed * Time.deltaTime);
                isNowWalk = true;

                if (transform.position == new Vector3(path[0].x, path[0].y, transform.position.z))
                {
                    path.RemoveAt(0);
                    //Убираем коллайдер пути
                    if (pathfinding.gridChanges.Count != 0)
                    {
                        grid.grid[pathfinding.gridChanges[0].x, pathfinding.gridChanges[0].y] = 0;
                        pathfinding.gridChanges.RemoveAt(0);
                    }
                }
            }
            else
            {
                if (target!=null && target.targetMoveType == TargetType.Static)
                    ResetTarget();
  
                isNowWalk = false;
            }
        }
    }
    public void SetStop(bool active) { isStopped = active; } 
}