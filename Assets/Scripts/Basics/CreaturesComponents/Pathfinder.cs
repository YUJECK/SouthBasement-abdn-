using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreaturesAI.Pathfinding
{
    public class Pathfinder : MonoBehaviour
    {
        private enum GCostDefining
        {
            AlwaysOne,
            Distance,
            Random
        }
        private class Point
        {
            //position
            private int x;
            private int y;
            //costs
            private float g = 0; 
            private float h = 0;

            private Point cameFrom;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public Point CameFrom => cameFrom;
            public float G => g;
            public float H => h;
            public float F => g + h;
            public int X => x;
            public int Y => y;
            public void SetCosts(float g, float h) { this.g = g; this.h = h; }
            public void SetCameFrom(Point startingPoint) => cameFrom = startingPoint;
            public void SetXY(Vector2 point)
            {
                x = (int)point.x;
                y = (int)point.y;
            }
        }
        
        GridManager grid;
        [SerializeField] private GCostDefining gCostDefining;
        [SerializeField] GameObject obj;
        [SerializeField] GameObject test2;

        public List<Vector2> FindPath(Vector2 start, Vector2 end)
        {
            List<Point> nextPoints = new List<Point>();
            bool[,] visitedPoints = new bool[grid.GridWidth, grid.GridHeight];
            Point startPoint = new Point((int)start.x, (int)start.y);
            Point endPoint = new Point((int)end.x, (int)end.y);

            Instantiate(test2, start, Quaternion.identity);
            Instantiate(test2, end, Quaternion.identity);


            Point currentPoint = startPoint;

            while (true)
            {
                if (currentPoint.X == endPoint.X && currentPoint.Y == endPoint.Y)
                    return RestorePath(currentPoint);

                List<Point> neibhourPoints = GetNeibhourPoints(currentPoint, visitedPoints);
                foreach (Point point in neibhourPoints)
                {
                    point.SetCosts(currentPoint.G + DefineGCost(currentPoint, point), Heuristic(point, endPoint));
                    point.SetCameFrom(currentPoint);
                    nextPoints.Add(point);

                    visitedPoints[point.X, point.Y] = true;

                    Instantiate(obj, new Vector2(point.X, point.Y), Quaternion.identity);
                }

                currentPoint = GetBestPoint(nextPoints);
                nextPoints.Remove(currentPoint);
            }
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
                if (CheckPoint(nextPoint) && !ignoredPoints[nextPoint.X, nextPoint.Y])
                    neibhourPoints.Add(nextPoint);
            }
            return neibhourPoints;
        }
        private Point GetBestPoint(List<Point> points)
        {
            Point bestPoint = points[0];
            
            foreach (Point point in points)
            {
                if (point.F < bestPoint.F)
                    bestPoint = point;
            }

            return bestPoint;
        }
        private int Heuristic(Point first, Point second) => Mathf.Abs(first.X - second.X) + Mathf.Abs(first.Y - second.Y);
        private bool CheckPoint(Point point)
        {
            if (grid.GetPoint(new Vector2Int(point.X, point.Y)) == 0)
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
                current = current.CameFrom;
            } while ((current.CameFrom != null));

            return path;
        }
        private float DefineGCost(Point startPoint, Point endPoint)
        {
            if (gCostDefining == GCostDefining.AlwaysOne) return 1;
            else if (gCostDefining == GCostDefining.Distance)
                return Vector2.Distance(new Vector2(startPoint.X, startPoint.Y), new Vector2(endPoint.X, endPoint.Y));
            else return Random.Range(0, 5);
        }

        void Start()
        {
            grid = FindObjectOfType<GridManager>();
            FindPath(new Vector2Int(32, 49), new Vector2Int(57, 32));
        }
    }
}