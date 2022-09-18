using System.Collections.Generic;
using UnityEngine;

namespace CreaturesAI.Pathfinding
{
    public sealed class Pathfinder : MonoBehaviour
    {
        private enum GCostDefining
        {
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

            private Point previosPoint;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            
            public Point PreviosPoint => previosPoint;
            public float G => g;
            public float H => h;
            public float F => g + h;
            public int X => x;
            public int Y => y;
            public void SetCosts(float g, float h) { this.g = g; this.h = h; }
            public void SetPreviosPoint(Point startingPoint) => previosPoint = startingPoint;
            public void SetXY(Vector2 point)
            {
                x = (int)point.x;
                y = (int)point.y;
            }
        }
        private class PointComparer : IComparer<Point>
        {
            public int Compare(Point x, Point y)
            {
                if (x.F > y.F) return 1;
                else if (x.F < y.F) return -1;
                return 0;
            }
        }

        [SerializeField] private GCostDefining gCostDefining;
        GridManager grid;

        private float DefineGCost(Point startPoint, Point endPoint)
        {
            switch (gCostDefining)
            {
                case GCostDefining.Distance:
                    return Vector2.Distance(new Vector2(startPoint.X, startPoint.Y), new Vector2(endPoint.X, endPoint.Y));
                case GCostDefining.Random:
                    return Random.Range(0, 5);
            }
            return 1;
        }
        private int Heuristic(Point first, Point second) => Mathf.Abs(first.X - second.X) + Mathf.Abs(first.Y - second.Y);
        private bool CheckPointCollider(Point point)
        {
            if (grid.GetPoint(new Vector2Int(point.X, point.Y)) == 0)
                return true;
            else return false;
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
                if (CheckPointCollider(nextPoint) && !ignoredPoints[nextPoint.X, nextPoint.Y])
                    neibhourPoints.Add(nextPoint);
            }
            return neibhourPoints;
        }
        public List<Vector2> FindPath(Vector2 start, Vector2 end)
        {
            List<Point> nextPoints = new List<Point>();
            bool[,] visitedPoints = new bool[grid.GridWidth, grid.GridHeight];
            Point startPoint = new Point((int)start.x, (int)start.y);
            Point endPoint = new Point((int)end.x, (int)end.y);

            Point currentPoint = startPoint;

            while (true)
            {
                //if we have reached to the end
                if (currentPoint.X == endPoint.X && currentPoint.Y == endPoint.Y)
                    return RestorePath(currentPoint);

                //getting neibhours
                List<Point> neibhourPoints = GetNeibhourPoints(currentPoint, visitedPoints);

                //defining costs, previos point
                foreach (Point point in neibhourPoints)
                {
                    point.SetCosts(currentPoint.G + DefineGCost(currentPoint, point), Heuristic(point, endPoint));
                    point.SetPreviosPoint(currentPoint);
                    nextPoints.Add(point);

                    visitedPoints[point.X, point.Y] = true;
                }
                //sorting points
                nextPoints.Sort(new PointComparer());

                //choosing the best point
                currentPoint = nextPoints[0];
                nextPoints.Remove(currentPoint);
            }
        }
        private List<Vector2> RestorePath(Point endPoint)
        {
            Point current = endPoint;
            List<Vector2> path = new List<Vector2>();

            do
            {
                path.Add(new Vector2(current.X, current.Y));
                current = current.PreviosPoint;
            } while ((current.PreviosPoint != null));

            path.Reverse();

            return path;
        }

        void Awake() => grid = FindObjectOfType<GridManager>();
    }
}