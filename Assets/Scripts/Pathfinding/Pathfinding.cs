using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private bool[,] visitedPoints;
    private Grid grid;

    
    private Rigidbody2D rb;

    [SerializeField] private Transform target;
    [SerializeField] private List<Vector2> path;
    [SerializeField] float findRate;
    [SerializeField] float nextTime;

    public struct Point
    {
        public int x;
        public int y;
        public List<Vector2> path;
    }


    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        rb = GetComponent<Rigidbody2D>();
    }

    private List<Vector2> FindPath(Vector2 startPos, Vector2 endPos)
    {
        visitedPoints = new bool[grid.gridWidth,grid.gridHeight];
        List<Point> queue = new List<Point>();
        List<Point> nextQueue = new List<Point>();

        Point start = new Point();
        start.x = (int)startPos.x;
        start.y = (int)startPos.y;
        start.path = new List<Vector2>();
        start.path.Add(new Vector2(startPos.x, startPos.y));

        queue.Add(start);

        visitedPoints[start.x, start.y] = true;

        while (queue.Count > 0)
        {
            var curr = queue[0];
            if (curr.x == endPos.x && curr.y == endPos.y)
                return curr.path;
            
            
            CheckNext(1,0, curr, ref nextQueue);
            CheckNext(-1,0, curr, ref nextQueue);
            CheckNext(0,1, curr, ref nextQueue);
            CheckNext(0,-1, curr, ref nextQueue);
            
            CheckNext(1,1, curr, ref nextQueue);
            CheckNext(-1,1, curr, ref nextQueue);
            CheckNext(-1,1, curr, ref nextQueue);
            CheckNext(1,-1, curr, ref nextQueue);
            
            queue.RemoveAt(0);
            
            // Next Level
            if (queue.Count == 0)
            {
                queue = new List<Point>(nextQueue);
                nextQueue.Clear();
            }
        }

        return new List<Vector2>();
    }
    private void CheckNext(int dX, int dY, Point point, ref List<Point> q)
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

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        path = FindPath(new Vector2(0,0), new Vector2(10,10));
    }

    private void Update()
    {
        if(target != null)
        {
            if(Time.time >= nextTime)
                path = FindPath(new Vector2(0,0), new Vector2(10,10));

            if(path.Count != 0)
            {
                rb.MovePosition(path[0]);   
                if(transform.position == new Vector3(path[0].x,path[0].y, 0f))
                    path.RemoveAt(0);        
            }
        }        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
            SetTarget(coll.transform);
    }
}
