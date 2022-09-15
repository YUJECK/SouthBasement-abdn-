using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreaturesAI.Pathfinding
{
    public class Pathfinder : MonoBehaviour
    {
        private class Point
        {
            private int x;
            private int y;

            private Point cameFrom;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public void SetCameFrom(Point startingPoint) => cameFrom = startingPoint;
            public Point GetCameFrom() => cameFrom;
            public int X => x;
            public int Y => y;
            public void SetXY(Vector2 point)
            {
                x = (int)point.x;
                y = (int)point.y;
            }
        }

        [SerializeField] List<Vector2> test;
        
        GridManager grid;
        [SerializeField] GameObject obj;
        public List<Vector2> FindPath(Vector2Int startPoint, Vector2Int endPoint)
        {
            Queue<Point> nextPoints = new Queue<Point>();
            bool[,] visitedPoints = new bool[grid.GridWidth, grid.GridHeight];
            Point start = new Point(startPoint.x, startPoint.y);

            nextPoints.Enqueue(start);

            while (nextPoints.Count > 0)
            {
                if (nextPoints.Peek().X == endPoint.x && nextPoints.Peek().Y == endPoint.y)
                    return RestorePath(nextPoints.Peek());
                visitedPoints[(int)nextPoints.Peek().X, (int)nextPoints.Peek().Y] = true;

                List<Point> neibhourPoints = GetNeibhourPoints(nextPoints.Peek(), visitedPoints);

                foreach (Point point in neibhourPoints)
                {
                    point.SetCameFrom(nextPoints.Peek());
                    nextPoints.Enqueue(point);
                    visitedPoints[point.X, point.Y] = true;
                }
                nextPoints.Dequeue();
            }

            return new List<Vector2>();
        }
        private List<Point> GetNeibhourPoints(Point point, bool[,] ignoredPoints)
        {
            List<Point> neibhourPoints = new List<Point>();
            List<Point> pointsToCheck = new List<Point>();
            //adding points to check
            pointsToCheck.Add(new Point(point.X, point.Y + 1));
            pointsToCheck.Add(new Point(point.X, point.Y - 1));
            pointsToCheck.Add(new Point(point.X + 1, point.Y));
            pointsToCheck.Add(new Point(point.X - 1, point.Y));
            pointsToCheck.Add(new Point(point.X + 1, point.Y + 1));
            pointsToCheck.Add(new Point(point.X - 1, point.Y - 1));
            pointsToCheck.Add(new Point(point.X + 1, point.Y - 1));
            pointsToCheck.Add(new Point(point.X - 1, point.Y + 1));
            //cheking points
            foreach (Point nextPoint in pointsToCheck)
            {
                if (CheckPoint(nextPoint, ignoredPoints))
                    neibhourPoints.Add(nextPoint);
            }
            return neibhourPoints;
        }
        private bool CheckPoint(Point point, bool[,] ignoredPoints)
        {
            if (grid.GetPoint(new Vector2Int(point.X, point.Y)) == 0 && !ignoredPoints[point.X, point.Y])
                return true;
            else return false;
        }
        private List<Vector2> RestorePath(Point endPoint)
        {
            Point current = endPoint;
            List<Vector2> path = new List<Vector2>();

            do
            {
                Instantiate(obj, new Vector2(current.X, current.Y), Quaternion.identity);
                path.Add(new Vector2(current.X, current.Y));
                current = current.GetCameFrom();
            } while ((current.GetCameFrom() != null));

            return path;
        }
        
        void Start()
        {
            grid = FindObjectOfType<GridManager>();
            //test = GetNeibhourPoints(new Vector2(10, 10), new List<Vector2>());
            test = FindPath(new Vector2Int(40, 40), new Vector2Int(60, 60));
        }
    }
}