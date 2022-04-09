using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GameObject _collider;
    [SerializeField] private GameObject emptyArea;
    public int[,] grid;
    // 0 - нет коллайлера/это триггер
    // 1 - есть коллайлера
    
    // Выстоа и ширина сетки 
    public int gridWidth;  
    public int gridHeight;

    public int nodeSize;

    private void Awake()
    {
        Invoke("StartGrid", 3f);
    }

    private void StartGrid()
    {
        Camera camera = Camera.main;
        
        gridWidth = (int)Mathf.Ceil(gridWidth/nodeSize);
        gridHeight = (int)Mathf.Ceil(gridHeight/nodeSize);

        grid = new int[gridWidth, gridHeight];

        for(int x = 0; x < gridWidth; x+=nodeSize)
        {
            for(int y = 0; y < gridHeight; y+=nodeSize)
            {
                List<Vector3> points = new List<Vector3>();
                points.Add(new Vector3(nodeSize*0.2f, nodeSize*0.2f, 0f));
                points.Add(new Vector3(nodeSize*0.2f, -nodeSize*0.2f, 0f));
                points.Add(new Vector3(-nodeSize*0.2f, nodeSize*0.2f, 0f));
                points.Add(new Vector3(-nodeSize*0.2f, -nodeSize*0.2f, 0f));

                bool isWall = false;    

                foreach(Vector3 point in points)
                {
                    RaycastHit2D[] pointObjs = Physics2D.RaycastAll(new Vector3(x + point.x, y + point.y),Vector2.zero);

                    foreach(RaycastHit2D obj in pointObjs)
                    {
                        if(obj.collider != null)
                        {   
                            if(!obj.collider.isTrigger && (obj.collider.tag != "Enemy" || obj.collider.tag != "Player"))
                            {
                                isWall = true;
                                goto foreachExit;
                            }   
                        }
                    }
                }
                foreachExit: // Для выхода из двух циклов

                if(isWall)
                {
                    grid[x,y] = 1;
                    // Instantiate(_collider,new Vector3(x,y,0), Quaternion.identity,transform);
                }
                else 
                {
                    // Instantiate(emptyArea,new Vector3(x, y, 0),Quaternion.identity,transform);
                    grid[x,y] = 0;
                }
            }
        }
        Debug.Log("GridWasCreated");
    }
}

