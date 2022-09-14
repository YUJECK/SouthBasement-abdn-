using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreaturesAI.Pathfinding
{
    public class GridManager : MonoBehaviour
    {
        enum ObstacleDefining
        {
            AllColliders,
            CustomTags
        }

        [SerializeField] private int gridWidth = 300;
        [SerializeField] private int gridHeight = 300;

        private int[,] grid = { };
        [SerializeField] private ObstacleDefining obstacleDefining;
        [SerializeField] private List<string> tagBlackList = new List<string>();
        [SerializeField] private List<string> obstacleTags = new List<string>();

        //getters
        public int GetPoint(Vector2 point) => grid[(int)point.x, (int)point.y];
        public int GridWidth => gridWidth;
        public int GridHeight => gridHeight;

        private void GenerateGrid()
        {
            grid = new int[gridWidth, gridHeight];

            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(x,y), new Vector2(0, 0));

                    foreach (RaycastHit2D nextHit in hits)
                    {
                        if (nextHit.collider != null && !nextHit.collider.isTrigger)
                        {
                            if(obstacleDefining == ObstacleDefining.AllColliders) grid[x, y] = 1;
                            else if(obstacleTags.Contains(nextHit.transform.tag)) grid[x, y] = 1;
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