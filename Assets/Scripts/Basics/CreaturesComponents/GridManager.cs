using System.Collections.Generic;
using UnityEngine;

namespace CreaturesAI.Pathfinding
{
    public enum ObstacleDefining { AllColliders, CustomTags }
    public sealed class GridManager : MonoBehaviour
    {
        //variables
        [SerializeField] private int gridWidth = 300;
        [SerializeField] private int gridHeight = 300;
        [Space(10)]
        [SerializeField] private ObstacleDefining obstacleDefining;
        [SerializeField] private List<string> obstacleTagsBlacklist = new List<string>();
        [SerializeField] private List<string> obstacleTags = new List<string>();

        private int[,] grid = { };
        private Dictionary<Vector2, int> editedPoints = new Dictionary<Vector2, int>();

        //getters
        public int GetPoint(Vector2 point)
        {
            if (CheckRange(point))
                return grid[(int)point.x, (int)point.y];
            else return grid[0, 0];
        }
        public int GetEditedPoint(Vector2 point)
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
        public ObstacleDefining ObstacleDefining => obstacleDefining;

        //setters
        public void EditPoint(Vector2 point, int newValue)
        {
            if (CheckRange(point))
            {
                editedPoints.Add(point, grid[(int)point.x, (int)point.y]);
                grid[(int)point.x, (int)point.y] = newValue;
            }
        }
        public void DiscardPointEditing(Vector2 point)
        {
            if (editedPoints.ContainsKey(point))
            {
                grid[(int)point.x, (int)point.y] = editedPoints[point];
                editedPoints.Remove(point);
            }
            else Debug.LogWarning("Point " + point + " hasn't been edited");
        }

        //other
        public bool CheckRange(Vector2 point)
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
                        if (nextHit.collider != null && !nextHit.collider.isTrigger && !obstacleTagsBlacklist.Contains(nextHit.transform.tag))
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