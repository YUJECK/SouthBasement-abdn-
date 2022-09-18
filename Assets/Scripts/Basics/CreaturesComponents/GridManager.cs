using System.Collections.Generic;
using UnityEngine;

namespace CreaturesAI.Pathfinding
{
    public sealed class GridManager : MonoBehaviour
    {
        enum ObstacleDefining
        {
            AllColliders,
            CustomTags
        }

        [SerializeField] private int gridWidth = 300;
        [SerializeField] private int gridHeight = 300;

        [SerializeField] private ObstacleDefining obstacleDefining;
        [SerializeField] private List<string> tagBlackList = new List<string>();
        [SerializeField] private List<string> obstacleTags = new List<string>();

        public GameObject test;
        private int[,] grid = { };
        private Dictionary<Vector2Int, int> editedPoints = new Dictionary<Vector2Int, int>();

        //getters
        public int GetPoint(Vector2Int point)
        {
            if (CheckRange(point))
                return grid[(int)point.x, (int)point.y];
            else return grid[0, 0];
        }
        public int GetEditedPoint(Vector2Int point)
        {
            if (editedPoints.ContainsKey(point))
                return editedPoints[point];
            else
            {
                Debug.LogWarning("Point " + point + " hasn't been edited");
                return 0;
            }
        }
        public int GridWidth => gridWidth;
        public int GridHeight => gridHeight;

        //setters
        public void EditPoint(Vector2Int point, int newValue)
        {
            if (CheckRange(point))
            {
                editedPoints.Add(point, grid[point.x, point.y]);
                grid[point.x, point.y] = newValue;
            }
        }
        public void DiscardPointEditing(Vector2Int point)
        {
            if (editedPoints.ContainsKey(point))
            {
                grid[point.x, point.y] = editedPoints[point];
                editedPoints.Remove(point);
            }
            else Debug.LogWarning("Point " + point + " hasn't been edited");
        }

        //other
        public bool CheckRange(Vector2Int point)
        {
            if ((point.x > 0 && point.x < GridWidth) && (point.y > 0 && point.y < GridHeight))
                return true;
            else
            {
                Debug.LogWarning("Point" + point + " is out of range");
                return false;
            }
        }
        private void GenerateGrid()
        {
            grid = new int[gridWidth, gridHeight];

            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(x, y), new Vector2(0, 0));

                    foreach (RaycastHit2D nextHit in hits)
                    {
                        if (nextHit.collider != null && !nextHit.collider.isTrigger && !tagBlackList.Contains(nextHit.transform.tag))
                        {
                            if (obstacleDefining == ObstacleDefining.AllColliders) grid[x, y] = 1;
                            else if (obstacleTags.Contains(nextHit.transform.tag)) grid[x, y] = 1;
                        }
                        else grid[x, y] = 0;
                    }
                }
            }
        }

        //unity methods
        private void Awake() => GenerateGrid();
    }
}