using System.Collections.Generic;
using UnityEngine;

namespace Creature.Pathfind
{
    
    public class Grid : MonoBehaviour
    {
        private bool isGridCreated = false;

        public int[,] grid;
        // 0 - пусто
        // 1 - не пусто :\

        // Выстоа и ширина сетки 
        [SerializeField] private List<string> blackTagList = new List<string>(); //Теги по которым не будет записываться 1
        [SerializeField] private int gridWidth = 300;
        [SerializeField] private int gridHeight = 300;

        [SerializeField] private float nodeSize = 1;

        //Геттеры
        public bool IsGridCreated => isGridCreated;
        public float NodeSize => nodeSize;
        public int GridWidth => gridWidth;
        public int GridHeight => gridHeight;

        public void StartGrid()
        {
            Camera camera = Camera.main;

            gridWidth = (int)Mathf.Ceil(gridWidth / nodeSize);
            gridHeight = (int)Mathf.Ceil(gridHeight / nodeSize);

            grid = new int[gridWidth, gridHeight];

            for (float x = 0; x < gridWidth; x += nodeSize)
            {
                for (float y = 0; y < gridHeight; y += nodeSize)
                {
                    float a = 0.2f;
                    List<Vector3> points = new List<Vector3>();
                    points.Add(new Vector3(0f, 0f, 0f));
                    points.Add(new Vector3(nodeSize * a, nodeSize * a, 0f));
                    points.Add(new Vector3(nodeSize * a, -nodeSize * a, 0f));
                    points.Add(new Vector3(-nodeSize * a, nodeSize * a, 0f));
                    points.Add(new Vector3(-nodeSize * a, -nodeSize * a, 0f));
                    points.Add(new Vector3(0, nodeSize * a, 0f));
                    points.Add(new Vector3(0, -nodeSize * a, 0f));
                    points.Add(new Vector3(nodeSize * a, 0, 0f));
                    points.Add(new Vector3(-nodeSize * a, 0, 0f));

                    bool isWall = false;

                    foreach (Vector3 point in points)
                    {
                        RaycastHit2D[] hitObjects = Physics2D.RaycastAll(new Vector3(x + point.x, y + point.y), Vector2.zero);
                        foreach(RaycastHit2D hit in hitObjects)
                        {
                            if (!blackTagList.Contains(hit.transform.tag) && hit.collider != null && !hit.collider.isTrigger)
                            {
                                isWall = true;
                                goto foreachExit;
                            }
                        }
                    }
                foreachExit: // Для выхода из двух циклов

                    if (isWall) grid[(int)x, (int)y] = 1;
                    else grid[(int)x, (int)y] = 0;
                }
            }

            isGridCreated = true;
            Debug.Log("[Info]: Grid created");
        }
        public void OverWriteGrid(Vector2 start, Vector2 end)
        {
            Camera camera = Camera.main;

            start.x = (int)Mathf.Ceil(start.x / nodeSize);
            start.y = (int)Mathf.Ceil(start.y / nodeSize);
            end.x = (int)Mathf.Ceil(end.x / nodeSize);
            end.y = (int)Mathf.Ceil(end.y / nodeSize);

            for (float x = start.x; x < end.x; x += nodeSize)
            {
                for (float y = start.y; y < end.y; y += nodeSize)
                {
                    float a = 0.7f;
                    List<Vector3> points = new List<Vector3>();
                    points.Add(new Vector3(0f, 0f, 0f));
                    points.Add(new Vector3(nodeSize * a, nodeSize * a, 0f));
                    points.Add(new Vector3(nodeSize * a, -nodeSize * a, 0f));
                    points.Add(new Vector3(-nodeSize * a, nodeSize * a, 0f));
                    points.Add(new Vector3(-nodeSize * a, -nodeSize * a, 0f));
                    points.Add(new Vector3(0, nodeSize * a, 0f));
                    points.Add(new Vector3(0, -nodeSize * a, 0f));
                    points.Add(new Vector3(nodeSize * a, 0, 0f));
                    points.Add(new Vector3(-nodeSize * a, 0, 0f));

                    bool isWall = false;

                    foreach (Vector3 point in points)
                    {
                        RaycastHit2D[] pointObjs = Physics2D.RaycastAll(new Vector3(x + point.x, y + point.y), Vector2.zero);

                        foreach (RaycastHit2D obj in pointObjs)
                        {
                            if (obj.collider != null)
                            {
                                if (!obj.collider.isTrigger && ((obj.collider.tag != "Enemy" && obj.collider.tag != "Player") || obj.transform.CompareTag("Decor")))
                                {
                                    isWall = true;
                                    goto foreachExit;
                                }
                            }
                        }
                    }
                foreachExit: // Для выхода из двух циклов

                    if (isWall)
                        grid[(int)x, (int)y] = 1;
                    else
                        grid[(int)x, (int)y] = 0;
                }
            }
        }
        public int GetGridPoint(int x, int y) 
        {
            if (IsGridCreated) return grid[x, y];
            else return 2;
        }
        public void EditGrid(int x, int y, int newPoint) { if (IsGridCreated) grid[x, y] = newPoint; }
    }
}