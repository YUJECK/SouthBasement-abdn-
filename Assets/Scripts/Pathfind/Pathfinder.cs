using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    //Классы
    public class Point // Стуктура для точки
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
    private class PathVisualization //Стуркура хранящая визуализацию путя, и визуализацию блока,, пути
    {
        public GameObject path;
        public GameObject blockedPath;

        public PathVisualization(GameObject _path, GameObject _blockedPath)
        {
            path = _path;
            blockedPath = _blockedPath;
        }
    };
    private class CachePath
    {
        public Vector2Int startPos;
        public Vector2Int endPos;
        public List<Vector2> path;

        public CachePath(Vector2Int start, Vector2Int end, List<Vector2> path)
        {
            startPos = start;
            startPos = end;
            this.path = path;
        }
    }

    [HideInInspector] public Grid grid;

    [SerializeField] private bool changeGrid = false; //Будет ли матрица изменяться в зависимости от пути
    public bool isPathVisualization;
    [HideInInspector] public List<Vector2Int> gridChanges = new List<Vector2Int>();
    private List<PathVisualization> pathVisualization = new List<PathVisualization>();
    private List<CachePath> cachePath = new List<CachePath>();

    private bool[,] visitedPoints;
    private int failStrik = 0;
    private float failWaitTime = 1.5f;
    private float nextTime = 0;

    //Методы для поиска пути
    public List<Vector2> FindPath(Vector2 startPos, Vector2 endPos, bool cashing) // Поиск пути
    {
        visitedPoints = new bool[grid.gridWidth, grid.gridHeight]; // Массив посещенных точек 
        List<Point> reachable = new List<Point>(); //Прилягающие точки

        endPos = new Vector2((int)endPos.x, (int)endPos.y);
        reachable.Add(new Point(startPos));

        while (reachable.Count > 0)
        {
            //Выбираем след., точку
            Point current = reachable[Random.Range(0, reachable.Count)];

            if (new Vector2(current.x, current.y) == endPos)
                return current.path;

            reachable.Remove(current);
            visitedPoints[current.x, current.y] = true;

            if (current == null) Debug.LogError("Current point == null");
            List<Point> newReachable = GetReachablePoints(current, endPos);

            foreach (Point point in newReachable)
            {
                if (!reachable.Contains(point))
                {
                    point.path.Add(new Vector2(current.x, current.y));
                    reachable.Add(point);
                }
            }
        }

        return new List<Vector2>();
    }
    private void ChoosePoint(List<Point> points) { } // 
    private List<Point> GetReachablePoints(Point point, Vector2 endPoint) // 
    {
        List<Point> reachablePoints = new List<Point>();

        if (CheckPoint(1, 0, point, endPoint)) reachablePoints.Add(new Point(new Vector2(point.x + 1, point.y + 0)));
        if (CheckPoint(-1, 0, point, endPoint)) reachablePoints.Add(new Point(new Vector2(point.x + -1, point.y + 0)));
        if (CheckPoint(0, 1, point, endPoint)) reachablePoints.Add(new Point(new Vector2(point.x + 0, point.y + 1)));
        if (CheckPoint(0, -1, point, endPoint)) reachablePoints.Add(new Point(new Vector2(point.x + 0, point.y + -1)));
        //if (CheckPoint(1, 1, point)) reachablePoints.Add(new Point(new Vector2(point.x + 1, point.y + 1)));
        //if (CheckPoint(-1, 1, point)) reachablePoints.Add(new Point(new Vector2(point.x + -1, point.y + 1)));
        //if (CheckPoint(1, -1, point)) reachablePoints.Add(new Point(new Vector2(point.x + 1, point.y + -1)));
        //if (CheckPoint(-1, -1, point)) reachablePoints.Add(new Point(new Vector2(point.x + -1, point.y + -1)));

        return reachablePoints;
    }
    private bool CheckPoint(int dX, int dY, Point point, Vector2 endPoint) // 
    {
        if (point.x + dX < grid.gridWidth && point.x + dX > 0
        && point.y + dY < grid.gridHeight && point.y + dY > 0
        && grid.grid[point.x + dX, point.y + dY] == 0 && !visitedPoints[point.x + dX, point.y + dY])
        {
            return true;
        }
        //Если конечная точка коллайдер
        else if (point.x + dX < grid.gridWidth && point.x + dX >= 0
        && point.y + dY < grid.gridHeight && point.y + dY >= 0
        && !visitedPoints[point.x + dX, point.y + dY] && point.x + dX == (int)endPoint.x && point.y + dY == (int)endPoint.y)
        {
            return true;
        }
        else return false;
    }

    //На потом
    private void BlockedPath(Point point)
    {
        if (gridChanges.Count != 0) ResetGridChanges();

        for (int i = 0; i < point.path.Count - 1; i++)//Чтобы враги не сталкивались
        {
            grid.grid[(int)(point.path[i].x / grid.nodeSize), (int)(point.path[i].y / grid.nodeSize)] = 1;
            gridChanges.Add(new Vector2Int((int)(point.path[i].x / grid.nodeSize), (int)(point.path[i].y / grid.nodeSize)));
        }
    }
    public void ResetGridChanges()//Убирает все изменения в сетке
    {
        for (int i = 0; i < gridChanges.Count; i++)
            grid.grid[gridChanges[i].x, gridChanges[i].y] = 0;
        gridChanges.Clear();
    }

    //Юнитивсие методы
    private void Awake() { grid = FindObjectOfType<Grid>(); }
    private void OnDestroy()
    {
        ResetGridChanges();
        if (pathVisualization.Count != 0) //Чистка
            for (int i = 0; i < pathVisualization.Count;)
            {
                Destroy(pathVisualization[0].path);
                Destroy(pathVisualization[0].blockedPath);
                pathVisualization.RemoveAt(0);
            }
    }
}