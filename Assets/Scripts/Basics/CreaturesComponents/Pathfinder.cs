using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreaturesAI.Pathfinding
{
    public class Pathfinder : MonoBehaviour
    {
        [SerializeField] List<Vector2> test;
        
        GridManager grid;
        [SerializeField] GameObject obj;
        public List<Vector2Int> FindPath(Vector2Int startPoint, Vector2Int endPoint)
        {
            Queue<Vector2Int> nextPoints = new Queue<Vector2Int>();
            bool[,] visitedPoints = new bool[grid.GridWidth, grid.GridHeight];

            nextPoints.Enqueue(startPoint);

            while (nextPoints.Count > 0)
            {
                if (nextPoints.Peek().x == endPoint.x && nextPoints.Peek().y == endPoint.y)
                    return new List<Vector2Int>();
                visitedPoints[(int)nextPoints.Peek().x, (int)nextPoints.Peek().y] = true;

                List<Vector2Int> neibhourPoints = GetNeibhourPoints(nextPoints.Peek(), visitedPoints);

                foreach (Vector2Int point in neibhourPoints)
                {
                    nextPoints.Enqueue(point);
                    visitedPoints[point.x, point.y] = true;
                    Instantiate(obj, (Vector2)point, Quaternion.identity);
                }
                nextPoints.Dequeue();
            }

            return new List<Vector2Int>();
        }
        private List<Vector2Int> GetNeibhourPoints(Vector2Int point, bool[,] ignoredPoints)
        {
            List<Vector2Int> neibhourPoints = new List<Vector2Int>();
            List<Vector2Int> pointsToCheck = new List<Vector2Int>();
            //adding points to check
            pointsToCheck.Add(new Vector2Int(point.x + 1, point.y + 1));
            pointsToCheck.Add(new Vector2Int(point.x + 1, point.y - 1));
            pointsToCheck.Add(new Vector2Int(point.x - 1, point.y + 1));
            pointsToCheck.Add(new Vector2Int(point.x, point.y + 1));
            pointsToCheck.Add(new Vector2Int(point.x + 1, point.y));
            //cheking points
            foreach (Vector2Int nextPoint in pointsToCheck)
            {
                if (CheckPoint(nextPoint, ignoredPoints))
                    neibhourPoints.Add(nextPoint);
            }
            return neibhourPoints;
        }
        private bool CheckPoint(Vector2Int point, bool[,] ignoredPoints)
        {
            if (grid.GetPoint(point) == 0 && !ignoredPoints[point.x, point.y])
                return true;
            else return false;
        }
        void Start()
        {
            grid = FindObjectOfType<GridManager>();
            //test = GetNeibhourPoints(new Vector2(10, 10), new List<Vector2>());
            FindPath(new Vector2Int(40, 40), new Vector2Int(60, 60));
        }
    }
}