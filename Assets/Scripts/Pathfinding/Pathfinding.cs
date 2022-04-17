using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private bool[,] visitedPoints; // Массив посещенных точек 
    private Grid grid;

    private Rigidbody2D rb; //Ссылка на Rigidbody2D объекта, нужно для обнулдения velocity из-за которого объект странно перемещается
    public bool isRun; // Идет ли объект к цели
    public bool resetVelocity; //Будут ли обнулятся velocity у Rigidbody2D
    public TriggerCheker triggerCheker; //Триггер для остановки

    [SerializeField] private Transform target; // Объект для которого будем искать путь
    [SerializeField] private List<Vector2> path; // Путь к таргету
    [SerializeField] float speed = 1f; // Скорость передвижения
    [SerializeField] float findRate = 10; // Частота поиска пути
    [SerializeField] float nextTime; // Время следующего поиска пути

    public struct Point // Стуктура для точки
    {
        public int x;
        public int y;
        public List<Vector2> path;
    }


    private void Start()
    {
        grid = FindObjectOfType<Grid>(); // Ищем сетку
        
        if(resetVelocity)
            rb = GetComponent<Rigidbody2D>();
    }

    private List<Vector2> FindPath(Vector2 startPos, Vector2 endPos) // Поиск пути
    {
        visitedPoints = new bool[grid.gridWidth,grid.gridHeight];
        List<Point> queue = new List<Point>();
        List<Point> nextQueue = new List<Point>();

        Point start = new Point();
        start.x = (int)startPos.x;
        start.y = (int)startPos.y;
        start.path = new List<Vector2>();
        start.path.Add(new Vector2(startPos.x, startPos.y));

        Debug.Log("Start pos - " + start.x + " " + start.y);
        Debug.Log("End pos - " + endPos);

        queue.Add(start);

        visitedPoints[start.x, start.y] = true;

        while (queue.Count > 0)
        {
            Point curr = queue[0];

            if (curr.x == (int)endPos.x && curr.y == (int)endPos.y)
            {
                Debug.Log("Path was found");
                return curr.path;
            }

            //   н
            // н т н
            //   н
            CheckPoint(1,0, curr, ref nextQueue, endPos);
            CheckPoint(-1,0, curr, ref nextQueue, endPos);
            CheckPoint(0,1, curr, ref nextQueue, endPos);
            CheckPoint(0,-1, curr, ref nextQueue, endPos);
            
            //Проверка угловых клеток
            // н н
            //  т 
            // н н
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
    private void CheckPoint(int dX, int dY, Point point, ref List<Point> listOfPoints, Vector2 end) // Проверка след,, точки
    {
        Point nextPoint = new Point();
        nextPoint.x = point.x;
        nextPoint.y = point.y;
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
    public void ResetTarget() { target = null; path.Clear();} // Срос таргета

    private void SetNextTime() { nextTime = Time.time + findRate; } // Ставим следующее время
   
   
    private void FixedUpdate()
    {
        if(resetVelocity) // Обнуление velocity
            rb.velocity = new Vector2(0f, 0f);

        if(triggerCheker != null && !triggerCheker.isOnTrigger)
        {
            if(target != null)
            {
                if((Time.time >= nextTime & nextTime != 0) || path.Count == 0)
                {
                    //Ищем новый путь
                    path = FindPath(
                    new Vector2(transform.position.x / grid.nodeSize, transform.position.y / grid.nodeSize),
                    new Vector2(target.position.x / grid.nodeSize, target.position.y / grid.nodeSize));
                    
                    // //Перероверяем сетку, чтобы определялись враги
                    // grid.ResetGrid(
                    // new Vector2(transform.position.x-10, transform.position.y-10),
                    // new Vector2(transform.position.x+10, transform.position.y) 
                    // );

                    SetNextTime();
                }
                    
                if(path.Count != 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, path[0], speed*Time.deltaTime);   // Перемещаем объект в след точку
                    isRun = true;

                    if(transform.position == new Vector3(path[0].x,path[0].y, transform.position.z))
                        path.RemoveAt(0);        
                }
            }        
            else isRun = false;
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.tag == "Player" && target == null)
            SetTarget(coll.transform);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            ResetTarget();
    }
}