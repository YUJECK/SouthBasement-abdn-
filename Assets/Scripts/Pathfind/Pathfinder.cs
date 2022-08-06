using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    //Класс
    public class Point // Класс для точки
    {
        public int x;
        public int y;
        public int cost;
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
    private bool[,] visitedPoints; // Массив посещенных точек 

    [SerializeField] private bool changeGrid = false; //Будет ли матрица изменяться в зависимости от пути
    public bool isPathVisualization;
    [HideInInspector] public List<Vector2Int> gridChanges = new List<Vector2Int>();
    private List<PathVisualization> pathVisualization = new List<PathVisualization>();
    private List<CachePath> cachePath = new List<CachePath>();

    private int failStrik = 0;
    private float failWaitTime = 1.5f;
    private float nextTime = 0;

    //Методы для поиска пути
    public List<Vector2> FindPath(Vector2 startPos, Vector2 endPos, bool cashing) // Поиск пути
    {
        if (grid != null && grid.isGridCreated && Time.time >= nextTime)
        {
            if (cashing)
            {
                Vector2Int startInt = new Vector2Int((int)startPos.x, (int)startPos.y);
                Vector2Int endInt = new Vector2Int((int)endPos.x, (int)endPos.y);

                foreach (CachePath cache in cachePath)
                {
                    if (startInt == cache.startPos && endInt == cache.endPos)
                        return cache.path;
                }
            }

            visitedPoints = new bool[grid.gridWidth, grid.gridHeight];
            List<Point> queue = new List<Point>();
            List<Point> nextQueue = new List<Point>();

            Point start = new Point(startPos);
            start.path = new List<Vector2>();
            start.path.Add(new Vector2(startPos.x, startPos.y));

            queue.Add(new Point(startPos));

            visitedPoints[start.x, start.y] = true;

            while (queue.Count > 0)
            {
                Point curr = queue[0];

                if (curr.x == (int)endPos.x && curr.y == (int)endPos.y)
                {
                    //{Визуалиция
                    if (pathVisualization.Count > 0) //Чистка
                        for (int i = 0; i < pathVisualization.Count; i++)
                        {
                            Destroy(pathVisualization[0].path);
                            Destroy(pathVisualization[0].blockedPath);
                            pathVisualization.RemoveAt(0);
                        }
                    if (isPathVisualization)
                    {
                        for (int i = 0; i < curr.path.Count; i++)
                            pathVisualization.Add(new PathVisualization(Instantiate(grid.enemyPath, curr.path[i], Quaternion.identity), Instantiate(grid._collider, curr.path[i], Quaternion.identity)));
                    }
                    //Визуализация}

                    if (changeGrid) BlockedPath(curr);
                    failStrik = 0;
                    if (cashing) cachePath.Add(new CachePath(new Vector2Int((int)startPos.x, (int)startPos.y), new Vector2Int((int)endPos.x, (int)endPos.y), curr.path));
                    return curr.path;
                }

                CheckPoint(1, 0, curr, ref nextQueue, endPos);
                CheckPoint(-1, 0, curr, ref nextQueue, endPos);
                CheckPoint(0, 1, curr, ref nextQueue, endPos);
                CheckPoint(0, -1, curr, ref nextQueue, endPos);
                CheckPoint(-1, -1, curr, ref nextQueue, endPos);
                CheckPoint(1, -1, curr, ref nextQueue, endPos);
                CheckPoint(-1, 1, curr, ref nextQueue, endPos);
                CheckPoint(1, 1, curr, ref nextQueue, endPos);

                queue.RemoveAt(0);

                if (queue.Count == 0)
                {
                    queue = new List<Point>(nextQueue);
                    nextQueue.Clear();
                }
            }

            //Если путь не был найден
            Debug.LogWarning("[ArtificialWarn]: Path wasn't found: " + startPos + " " + new Vector2((int)endPos.x, (int)endPos.y));
            failStrik++;
            if (failStrik >= 2) nextTime = Time.time + failWaitTime;

            return new List<Vector2>();
        }

        return new List<Vector2>();
    }
    private void CheckPoint(int dX, int dY, Point point, ref List<Point> listOfPoints, Vector2 end) // Проверка след,, точки
    {
        Point nextPoint = new Point(new Vector2(point.x, point.y));
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
        else if (nextPoint.x + dX < grid.gridWidth && nextPoint.x + dX >= 0 && nextPoint.y + dY < grid.gridHeight && nextPoint.y + dY >= 0
        && visitedPoints[nextPoint.x + dX, nextPoint.y + dY] == false && nextPoint.x + dX == (int)end.x && nextPoint.y + dY == (int)end.y)
        {
            nextPoint.x += dX;
            nextPoint.y += dY;
            nextPoint.path.Add(new Vector2(nextPoint.x, nextPoint.y));
            listOfPoints.Add(nextPoint);

            visitedPoints[nextPoint.x, nextPoint.y] = true;
        }
    }
    private void BlockedPath(Point point)
    {
        if (gridChanges.Count != 0) ResetGridChanges();

        for (int i = 0; i < point.path.Count / 2 - 3; i++)
        {
            point.path.RemoveAt(point.path.Count - 1);
        }

        for (int i = 0; i < point.path.Count - 1; i++) //Чтобы враги не сталкивались
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

    //Юнитивские методы
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