using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private bool[,] visitedPoints; // Массив посещенных точек 
    private Grid grid;

    private Rigidbody2D rb; //Ссылка на Rigidbody2D объекта, нужно для обнулдения velocity из-за которого объект странно перемещается
    [HideInInspector]public bool isNowGoingToTarget; // Идет ли объект к цели
    private bool isPlayerTarget; // Идет ли объект к цели
    [SerializeField] private bool resetVelocity; //Будут ли обнулятся velocity у Rigidbody2D
    [SerializeField] private bool useTargetPoints; //Будет ли объект перемещатся к точкам
    [SerializeField] private Transform[] targetPoints; // Список точек для перемещения
    public TriggerCheker triggerCheker; //Триггер для остановки

    public Transform target; // Объект для которого будем искать путь
    [SerializeField] private List<Vector2> path; // Путь к таргету
    private float speed; //Скорость передвижения
    [SerializeField] float runSpeed = 1f; // Скорость при беге
    [SerializeField] float walkSpeed = 1f; // Скорость при ходьбе
    [SerializeField] float findRate = 10; // Частота поиска пути
    [SerializeField] float nextTime; // Время следующего поиска пути

    private List<GameObject> pathVisualization = new List<GameObject>();
    public bool isPathVisualization;

    public struct Point // Стуктура для точки
    {
        public int x;
        public int y;
        public List<Vector2> path;

        public Point(Vector2 start)
        {
            x = (int)start.x;
            y = (int)start.y;
            path = new List<Vector2>();
        }
    }


    private void Start()
    {
        grid = FindObjectOfType<Grid>(); // Ищем сетку
        
        if(resetVelocity)
            rb = GetComponent<Rigidbody2D>();
    }

    private List<Vector2> FindPath(Vector2 startPos, Vector2 endPos) // Поиск пути
    {
        if(grid.isGridCreated)
        {
            visitedPoints = new bool[grid.gridWidth,grid.gridHeight];
            List<Point> queue = new List<Point>();
            List<Point> nextQueue = new List<Point>();

            Point start = new Point();
            start.x = (int)startPos.x;
            start.y = (int)startPos.y;
            start.path = new List<Vector2>();
            start.path.Add(new Vector2(startPos.x, startPos.y));
            // Debug.Log(startPos);

            queue.Add(new Point(startPos));

            visitedPoints[start.x, start.y] = true;

            while (queue.Count > 0)
            {
                Point curr = queue[0];

                if (curr.x == (int)endPos.x && curr.y == (int)endPos.y)
                {
                    curr.path.Add(endPos);
                    
                    //Визуалиция{
                    if(pathVisualization.Count!=0) //Чистка
                        for(int i = 0; i < curr.path.Count;)
                        {
                            Destroy(pathVisualization[0]);
                            pathVisualization.RemoveAt(0);
                        }
                    if(isPathVisualization)
                    {
                        for(int i = 0; i < curr.path.Count; i++)
                            pathVisualization.Add(Instantiate(grid.enemyPath, curr.path[i], Quaternion.identity));
                    }
                    //}Визуализация


                    for(int i = 0; i < curr.path.Count-1; i++)//Чтобы враги не сталкивались
                    {
                        // grid.grid[(int)(curr.path[i].x / grid.nodeSize), (int)(curr.path[i].y / grid.nodeSize)] = 1;
                    }

                    return curr.path;
                }

                CheckPoint(1,0, curr, ref nextQueue, endPos);
                CheckPoint(-1,0, curr, ref nextQueue, endPos);
                CheckPoint(0,1, curr, ref nextQueue, endPos);
                CheckPoint(0,-1, curr, ref nextQueue, endPos);
                
                //Проверка угловых клеток
                CheckPoint(1,1, curr, ref nextQueue, endPos);
                CheckPoint(-1,1, curr, ref nextQueue, endPos);
                CheckPoint(1,-1, curr, ref nextQueue, endPos);
                CheckPoint(-1,-1, curr, ref nextQueue, endPos);
                
                queue.RemoveAt(0);
                
                if (queue.Count == 0)
                {
                    queue = new List<Point>(nextQueue);
                    nextQueue.Clear();
                }
            }
            Debug.LogError("Path wasnt found: " + startPos + " " + new Vector2((int)endPos.x, (int)endPos.y));
            return new List<Vector2>();
        }

        return new List<Vector2>();
    }
    private void CheckPoint(int dX, int dY, Point point, ref List<Point> listOfPoints, Vector2 end) // Проверка след,, точки
    {
        Point nextPoint = new Point();
        nextPoint.x = (int)point.x;
        nextPoint.y = (int)point.y;
        nextPoint.path = new List<Vector2>(point.path);
        
        //Нормальная проверка
        if (nextPoint.x + dX < grid.gridWidth && nextPoint.x + dX >= 0 && nextPoint.y + dY < grid.gridHeight && nextPoint.y + dY >= 0
        && visitedPoints[nextPoint.x + dX, nextPoint.y + dY] == false && grid.grid[nextPoint.x + dX, nextPoint.y + dY] == 0)
        {
            nextPoint.x += dX;
            nextPoint.y += dY;
            nextPoint.path.Add(new Vector2(nextPoint.x, nextPoint.y));
            listOfPoints.Add(nextPoint);

            visitedPoints[nextPoint.x, nextPoint.y] = true;
        }

        //Если конечная точка коллайдер
        else if(nextPoint.x + dX < grid.gridWidth && nextPoint.x + dX >= 0 && nextPoint.y + dY < grid.gridHeight && nextPoint.y + dY >= 0
        && visitedPoints[nextPoint.x + dX, nextPoint.y + dY] == false && nextPoint.x + dX == (int)end.x && nextPoint.y + dY == (int)end.y)
        {
            nextPoint.x += dX;
            nextPoint.y += dY;
            nextPoint.path.Add(new Vector2(nextPoint.x, nextPoint.y));
            listOfPoints.Add(nextPoint);

            visitedPoints[nextPoint.x, nextPoint.y] = true;
        }
    }
    public void SetTarget(Transform newTarget) // Поставить новый таргет
    {
        target = newTarget;

        if(path.Count == 0)
        {
            path = FindPath(
            new Vector2(transform.position.x / grid.nodeSize, transform.position.y / grid.nodeSize),
            new Vector2((int)(target.position.x / grid.nodeSize), (int)(target.position.y / grid.nodeSize)));
        }
        SetNextTime(); // Ставим сразу время, а то больше путь искаться в апдейте не будет
    }
    
    public void SetPointToTarget() // Поиск пути для точки
    {
        if(targetPoints.Length != 0)
        {
            int pointInd = Random.Range(0,targetPoints.Length); //Рандомим точку
            target = targetPoints[pointInd];
            speed = walkSpeed; // Ставим скорость на скорость при ходьбе
            SetNextTime();
        }
    }
    public void ResetTarget() { target = null; path.Clear(); isPlayerTarget = false;} // Срос таргета 
    private void SetNextTime() { nextTime = Time.time + findRate; } // Ставим следующее время
   
   
    private void FixedUpdate()
    {
        if(resetVelocity) // Обнуление velocity
            rb.velocity = new Vector2(0f, 0f);

        if(triggerCheker != null && !triggerCheker.isOnTrigger)
        {
            //Если есть таргет
            if(target != null)
            {
                if((Time.time >= nextTime & nextTime != 0) || path.Count == 0)
                {
                    //Убираем все единички что мы поставили и не успели убрать
                    ResetGridChanges();
                    
                    //Ищем новый путь
                    path = FindPath(
                    new Vector2(transform.position.x / grid.nodeSize, transform.position.y / grid.nodeSize),
                    new Vector2(target.position.x / grid.nodeSize, target.position.y / grid.nodeSize));

                    SetNextTime();
                }

                if(path.Count != 0) //Если путь не равен нулю, то мы идем по нему
                {
                    transform.position = Vector2.MoveTowards(transform.position, path[0], speed*Time.deltaTime);   // Перемещаем объект в след точку
                    isNowGoingToTarget = true;

                    if(transform.position == new Vector3(path[0].x, path[0].y, transform.position.z))
                    {
                        grid.grid[(int)(path[0].x / grid.nodeSize), (int)(path[0].y / grid.nodeSize)] = 0;
                        path.RemoveAt(0);        
                    }   
                }
                if(useTargetPoints && path.Count == 0 && !isPlayerTarget) ResetTarget();
            }            
            else if(useTargetPoints) SetPointToTarget();
            else isNowGoingToTarget = false;
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.tag == "Player" && !isPlayerTarget) 
        {
            SetTarget(coll.transform);
            if(useTargetPoints) speed = runSpeed; //Ставим скорость на сокрость при беге
            isPlayerTarget = true;
        }
    }

    public void ResetGridChanges()//Убирает все изменения в сетке
    {
        for(int i = 0; i < path.Count; i++)
        {
            grid.grid[(int)(path[i].x / grid.nodeSize), (int)(path[i].y / grid.nodeSize)] = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Player") 
        {
            ResetTarget(); //Убираем таргет
            
            if(useTargetPoints) //Идем к точке если таковые имеются 
                SetPointToTarget(); 
        }
    }
}