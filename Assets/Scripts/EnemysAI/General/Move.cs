using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace EnemysAI
{
    [RequireComponent(typeof(Pathfinder))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Move : MonoBehaviour
    {
        [Header("Параметры передвижения")]
        [SerializeField] private Movement movement = Movement.Wandering;
        [SerializeField] private float _speed = 3; // Скорость передвижения 
        [SerializeField] private float searchRate = 1f; // Частота репоиска 
        [SerializeField] private bool controlMovementFromHere = true;
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
        /*[HideInInspector] */public bool isNowWalk; //Идет ли сейчас 
        private bool blockStop = false;
        private bool isStopped = false; //Остановлен ли
        private List<Vector2> path = new List<Vector2>(); //Путь
        private EnemyTarget target; //Таргет

        [Header("События")]
        public UnityEvent onFlip = new UnityEvent();
        public UnityEvent onArrive = new UnityEvent();
        public UnityEvent onWakeUp = new UnityEvent();
        public UnityEvent onSleep = new UnityEvent();

        //Переменные для поворота
        private bool flippedOnRight;
        private Vector2 lastPos;

        //Ссылки на другие скрипты
        [Header("Другое")]
        [SerializeField] private TriggerChecker stopCheker;
        [SerializeField] private TargetSelection targetSelection;
        private Pathfinder pathfinder;
        private Grid grid;
        private Rigidbody2D rb;

        //Методы управления скриптом
        public void ResetVelocity()
        {
            //Сброс velocity
            if (rb != null) rb.velocity = Vector2.zero;
        }
        public void Moving()
        {
            if (!isStopped && pathfinder.grid.isGridCreated)
            {
                //Движение 
                if (path.Count != 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, path[0], speed * Time.deltaTime);
                    isNowWalk = true;

                    if (transform.position == new Vector3(path[0].x, path[0].y, transform.position.z))
                    {
                        path.RemoveAt(0);
                        //Убираем коллайдер пути
                        if (pathfinder.gridChanges.Count != 0)
                        {
                            grid.grid[pathfinder.gridChanges[0].x, pathfinder.gridChanges[0].y] = 0;
                            pathfinder.gridChanges.RemoveAt(0);
                        }
                    }
                }
                else if (path.Count == 0)
                {
                    isNowWalk = false;
                    if (target != null && target.targetMoveType == TargetType.Static)
                    {
                        ResetTarget();
                        onArrive.Invoke();
                    }
                }
            }

            else isNowWalk = false;
        }
        public void DynamicPathfind()
        {
            //Динамичный поиск пути
            if (!isStopped && target != null && target.targetMoveType == TargetType.Movable && (Time.time >= nextSearchTime || path.Count == 0))
            {
                ResetTarget();
                FindNewPath(target);
                SetNextSearchTime();
            }
        }
        public void CheckRotation()
        {
            //Поворот
            if (new Vector3(lastPos.x, lastPos.y, transform.position.z) != transform.position && isNowWalk)
            {
                if (lastPos.x < transform.position.x && flippedOnRight)
                {
                    FlipThisObject();
                    flippedOnRight = false;
                }
                else if (lastPos.x > transform.position.x && !flippedOnRight)
                {
                    FlipThisObject();
                    flippedOnRight = true;
                }
            }
            lastPos = transform.position;
        }

        //Методы поиска пути
        public void FindNewPath(EnemyTarget target)
        {
            if (path.Count != 0) ResetTarget();
            this.target = target;

            bool cashinPath; //Будет ли "кэшироваться" путь
            if (target.targetMoveType == TargetType.Static) cashinPath = true;
            else cashinPath = false;

            path = pathfinder.FindPath(
                new Vector2(transform.position.x / grid.nodeSize, transform.position.y / grid.nodeSize),
                new Vector2(target.transform.position.x / grid.nodeSize, target.transform.position.y / grid.nodeSize), cashinPath);
        }
        public void ResetTarget(EnemyTarget target = null)
        {
            if (target != null && target == this.target) { this.target = null; }
            path.Clear();
            pathfinder.ResetGridChanges();
        }
        private void SetNextSearchTime() { nextSearchTime = Time.time + searchRate; }

        //Типо сеттеры и геттеры
        public void SetStop(bool stopActive) { if (!blockStop) isStopped = stopActive; }
        public void SetBlocking(bool blockActive) { blockStop = blockActive; }
        public bool GetStop() { return isStopped; }
        public bool GetBlocking() { return blockStop; }

        //Метод поворота    
        private void FlipThisObject() { if (!isStopped) { transform.Rotate(0f, 180f, 0f); onFlip.Invoke(); } }
        public void FlipOther(Transform _transform) { _transform.Rotate(180f, 0f, 0f); }

        //Юнитивские методы
        private void Start()
        {
            pathfinder = GetComponent<Pathfinder>();
            rb = GetComponent<Rigidbody2D>();
            grid = FindObjectOfType<Grid>();
            targetSelection.onTargetChange.AddListener(FindNewPath);
            targetSelection.onResetTarget.AddListener(ResetTarget);
            if (movement == Movement.Wandering) onArrive.AddListener(targetSelection.SetNewTarget);
            isStopped = false;
        }
        private void FixedUpdate() //Физическая логика
        {
            if(controlMovementFromHere)
            {
                DynamicPathfind();
                Moving();
                CheckRotation();
                ResetVelocity();
            }
        }
    }
}
public enum Movement
{
    Wandering,
    StaticPursuit
}