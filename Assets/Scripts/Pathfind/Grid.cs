using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool _isGridCreated = false;
    public bool isGridCreated
    {
        get { return _isGridCreated; }
        private set { _isGridCreated = value; }
    }
    public GameObject _collider;
    public GameObject enemyPath;
    [SerializeField] private GameObject emptyArea;
    private List<GameObject> gridVizualization = new List<GameObject>();

    public int[,] grid;
    // 0 - нет коллайлера/это триггер
    // 1 - есть коллайлера

    // Выстоа и ширина сетки 
    public int gridWidth;
    public int gridHeight;

    public float nodeSize;

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
                float a = 0.4f;
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

        isGridCreated = true;
        Debug.Log("[Info]: Grid created");
    }
    public void OverwriteGrid(Vector2 start, Vector2 end)
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
        if (isGridCreated) return grid[x, y];
        else return 2;
    }
    public void EditGrid(int x, int y, int newPoint) { if (isGridCreated) grid[x, y] = newPoint; }
    
    public void ShowGrid()
    {
        for (float x = 0; x < gridWidth; x += nodeSize)
        {
            for (float y = 0; y < gridHeight; y += nodeSize)
            {
                if (grid[(int)x, (int)y] == 1)
                    gridVizualization.Add(Instantiate(_collider, new Vector3(x, y, 0), Quaternion.identity, transform));
            }
        }
    }
    public void DisableGrid()
    {
        for (int i = 0; i < gridVizualization.Count; i++)
        {
            Destroy(gridVizualization[0]);
            gridVizualization.RemoveAt(0);
        }
    }

    public void PathVisualization(bool active)
    {
        foreach (Pathfinder path in Resources.FindObjectsOfTypeAll(typeof(Pathfinder)) as Pathfinder[])
        { path.isPathVisualization = active; }
    }
}