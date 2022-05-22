using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool isGridCreated = false;
    public GameObject _collider;
    public GameObject enemyPath;
    [SerializeField] private GameObject emptyArea;
    public int[,] grid;
    // 0 - нет коллайлера/это триггер
    // 1 - есть коллайлера
    
    // Выстоа и ширина сетки 
    public int gridWidth;  
    public int gridHeight;

    public float nodeSize;

    private void Awake()
    {
        // Invoke("StartGrid", 5f);
    }

    private void StartGrid()
    {
        Camera camera = Camera.main;
        
        gridWidth = (int)Mathf.Ceil(gridWidth/nodeSize);
        gridHeight = (int)Mathf.Ceil(gridHeight/nodeSize);

        grid = new int[gridWidth, gridHeight];

        for(float x = 0; x < gridWidth; x+=nodeSize)
        {
            for(float y = 0; y < gridHeight; y+=nodeSize)
            {
                float a = 0.7f;
                List<Vector3> points = new List<Vector3>();
                points.Add(new Vector3(0f, 0f, 0f));
                points.Add(new Vector3(nodeSize*a, nodeSize*a, 0f));
                points.Add(new Vector3(nodeSize*a, -nodeSize*a, 0f));
                points.Add(new Vector3(-nodeSize*a, nodeSize*a, 0f));
                points.Add(new Vector3(-nodeSize*a, -nodeSize*a, 0f));

                bool isWall = false;    

                foreach(Vector3 point in points)
                {
                    RaycastHit2D[] pointObjs = Physics2D.RaycastAll(new Vector3(x + point.x, y + point.y), Vector2.zero);

                    foreach(RaycastHit2D obj in pointObjs)
                    {
                        if(obj.collider != null)
                        {   
                            if(!obj.collider.isTrigger && ((obj.collider.tag != "Enemy" && obj.collider.tag != "Player") || obj.transform.CompareTag("Decor")))
                            {
                                isWall = true;
                                goto foreachExit;
                            }   
                        }
                    }
                }
                foreachExit: // Для выхода из двух циклов

                if(isWall)
                    grid[(int)x, (int)y] = 1;
                else 
                    grid[(int)x, (int)y] = 0;
            }
        }

        isGridCreated = true;
        Debug.Log("GridWasCreated");
    }
    public void ResetGrid(){StartGrid();}
    public void ShowGrid()
    {        
        for(float x = 0; x < gridWidth; x+=nodeSize)
        {
            for(float y = 0; y < gridHeight; y+=nodeSize)
            {
                if(grid[(int)x, (int)y] == 1)
                    Instantiate(_collider,new Vector3(x,y,0), Quaternion.identity,transform);
                else 
                    Instantiate(emptyArea,new Vector3(x, y, 0),Quaternion.identity,transform);
            }
        }
    }

    public void PathVisualization(bool active)
    {
        foreach(Pathfinding path in Resources.FindObjectsOfTypeAll(typeof(Pathfinding)) as Pathfinding[])
        {path.isPathVisualization = active;}
    }
}