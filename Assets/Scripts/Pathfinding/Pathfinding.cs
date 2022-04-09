using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private bool[,] visitedPoints; // Массив посещенных точек 
    [SerializeField]private Grid grid;
    private Rigidbody2D rb;

    [SerializeField] private Transform target; // Объект для которого будем искать путь
    [SerializeField] private List<Vector2> path; // Путь к таргету
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
        rb = GetComponent<Rigidbody2D>(); // Берем RigidBody2D от объекта
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
            var curr = queue[0];

            if (curr.x == (int)endPos.x && curr.y == (int)endPos.y)
            {
                Debug.Log("Path was found");
                return curr.path;
            }

            CheckNext(1,0, curr, ref nextQueue);
            CheckNext(-1,0, curr, ref nextQueue);
            CheckNext(0,1, curr, ref nextQueue);
            CheckNext(0,-1, curr, ref nextQueue);
            
            CheckNext(1,1, curr, ref nextQueue);
            CheckNext(-1,1, curr, ref nextQueue);
            CheckNext(1,-1, curr, ref nextQueue);
            CheckNext(-1,-1, curr, ref nextQueue);
            
            queue.RemoveAt(0);
            
            if (queue.Count == 0)
            {
                queue = new List<Point>(nextQueue);
                nextQueue.Clear();
            }
        }

        Debug.LogError("Path wasnt found");
        return new List<Vector2>();
    }
    private void CheckNext(int dX, int dY, Point point, ref List<Point> q) // Проверка след,, точки
    {
        var p = new Point();
        p.x = point.x;
        p.y = point.y;
        p.path = new List<Vector2>(point.path);
        
        if (p.x + dX < grid.gridWidth && p.x + dX >= 0 && p.y + dY < grid.gridHeight && p.y + dY >= 0
        && visitedPoints[p.x + dX, p.y + dY] == false && grid.grid[p.x + dX, p.y + dY] == 1)
        {
            p.x += dX;
            p.y += dY;
            p.path.Add(new Vector2(p.x, p.y));
            q.Add(p);

            visitedPoints[p.x, p.y] = true;
        }
    }

    public void SetTarget(Transform newTarget) // Поставить новый таргет
    {
        target = newTarget;
        path = FindPath(
        new Vector2(transform.position.x / grid.nodeSize, transform.position.y / grid.nodeSize),
        new Vector2((int)(target.position.x / grid.nodeSize), (int)(target.position.y / grid.nodeSize)));
    }
    public void ResetTarget() { target = null; } // Срос таргета

    private void Update()
    {
        if(target != null)
        {
            if(Time.time >= nextTime & nextTime != 0)
            {
                path = FindPath(
                new Vector2(transform.position.x / grid.nodeSize, transform.position.y / grid.nodeSize),
                new Vector2(target.position.x / grid.nodeSize, target.position.y / grid.nodeSize));
            }
                

            if(path.Count != 0)
            {
                rb.MovePosition(path[0]);   
                if(transform.position == new Vector3(path[0].x,path[0].y, transform.position.z))
                    path.RemoveAt(0);        
            }
        }        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            SetTarget(coll.transform);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            ResetTarget();
    }
}
