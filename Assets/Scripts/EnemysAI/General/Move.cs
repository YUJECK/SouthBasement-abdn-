using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace EnemysAI
{
    [RequireComponent(typeof(Pathfinder))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
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
        [HideInInspector] public bool isNowWalk; //Идет ли сейчас 
        private bool blockStop = false;
        private bool isStopped = false; //Остановлен ли
        private List<Vector2> path = new List<Vector2>(); //Путь
        private EnemyTarget moveTarget; //Таргет

        [Header("События")]
        public UnityEvent onFlip = new UnityEvent();
        public UnityEvent onArrive = new UnityEvent();
        public UnityEvent onWakeUp = new UnityEvent();
        public UnityEvent onSleep = new UnityEvent();

        //Переменные для поворота
        private bool flippedOnRight = false;
        private Vector2 lastPos;

        //Ссылки на другие скрипты
        [Header("Другое")]
        [SerializeField] private TriggerChecker stopCheker;
        [SerializeField] private TargetSelection targetSelection;
        private Pathfinder pathfinder;
        private Grid grid;
        private Rigidbody2D rigidBody;
        private SpriteRenderer spriteRenderer;

        //Методы управления скриптом
        public void ResetVelocity()
        {
            //Сброс velocity
            if (rigidBody != null) rigidBody.velocity = Vector2.zero;
        }
        public void Moving()
        {
            if (!isStopped && pathfinder.grid.IsGridCreated)
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
                    SetNewTargetAndFindPath(moveTarget);
                    SetNextSearchTime();
                    if (moveTarget != null && moveTarget.targetMoveType == TargetType.Static)
                    {
                        ResetTarget();
                        onArrive.Invoke();
                    }
                }
            }
            else
            {
                isNowWalk = false;
            }
        }
        public void DynamicPathfind()
        {
            //Динамичный поиск пути
            if (!isStopped && moveTarget != null && moveTarget.targetMoveType == TargetType.Movable && (Time.time >= nextSearchTime))
            {
                ResetTarget();
                SetNewTargetAndFindPath(moveTarget);
                SetNextSearchTime();
            }
        }
        public void CheckRotationByMoving()
        {
            //Поворот
            if (new Vector3(lastPos.x, lastPos.y, transform.position.z) != transform.position && isNowWalk)
            {
                if (lastPos.x > transform.position.x && flippedOnRight)
                {
                    FlipThisObject();
                    flippedOnRight = false;
                }
                else if (lastPos.x < transform.position.x && !flippedOnRight)
                {
                    FlipThisObject();
                    flippedOnRight = true;
                }
            }
            lastPos = transform.position;
        }
        public void CheckRotationByTarget()
        {
            if(moveTarget != null)
            {
                if(transform.position.x != moveTarget.transform.position.x)
                {
                    Debug.Log("Cheching rotation");
                    if (transform.position.x > moveTarget.transform.position.x && flippedOnRight)
                    {
                        FlipThisObject();
                        flippedOnRight = false;
                    }
                    else if (transform.position.x < moveTarget.transform.position.x && !flippedOnRight)
                    {
                        FlipThisObject();
                        flippedOnRight = true;
                    }
                }
            }
        }

        //Методы поиска пути
        public void SetNewTargetAndFindPath(EnemyTarget target)
        {
            if(target != null)
            {
                if (path.Count != 0) ResetTarget();
                if(this.moveTarget != target) this.moveTarget = target;

                bool cashinPath; //Будет ли "кэшироваться" путь
                if (target.targetMoveType == TargetType.Static) cashinPath = true;
                else cashinPath = false;

                path = pathfinder.FindPath(
                    new Vector2(transform.position.x / grid.nodeSize, transform.position.y / grid.nodeSize),
                    new Vector2(target.transform.position.x / grid.nodeSize, target.transform.position.y / grid.nodeSize), cashinPath);
            }
        }
        public void ResetTarget(EnemyTarget target = null)
        {
            if (target != null && target == this.moveTarget) { this.moveTarget = null; }
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
        private void FlipThisObject() { transform.Rotate(0f, 180f, 0f); onFlip.Invoke();  }
        public void FlipOther(Transform _transform) { _transform.Rotate(180f, 0f, 0f); }

        //Юнитивские методы
        private void Awake()
        {
            pathfinder = GetComponent<Pathfinder>();
            rigidBody = GetComponent<Rigidbody2D>();
            grid = FindObjectOfType<Grid>();
            
            targetSelection.onTargetChange.AddListener(SetNewTargetAndFindPath);
            targetSelection.onResetTarget.AddListener(ResetTarget);
            if (movement == Movement.Wandering) onArrive.AddListener(targetSelection.SetNewTarget);
            isStopped = false;
        }
        private void FixedUpdate() //Физическая логика
        {
            if(controlMovementFromHere)
            {
                ResetVelocity();
                DynamicPathfind();
                Moving();
                CheckRotationByMoving();
            }
        }
    }
}
public enum Movement
{
    Wandering,
    StaticPursuit
}