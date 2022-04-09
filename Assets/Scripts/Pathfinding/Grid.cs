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
                Vector2 pointPos = new Vector3(x,y,0);
                RaycastHit2D pointObj = Physics2D.Raycast(pointPos, Vector2.zero);
            
                if(pointObj.collider != null)
                {
                    if(pointObj.collider.isTrigger)
                        grid[x,y] = 0;
                    else
                        grid[x,y] = 1;
                }
                else
                {
                    grid[x,y] = 0;
                }
                
                if(grid[x,y] == 0)
                    Instantiate(emptyArea,new Vector3(pointPos.x,pointPos.y,0),Quaternion.identity,transform);
                else if(grid[x,y] == 1)
                    Instantiate(_collider,new Vector3(pointPos.x,pointPos.y,0),Quaternion.identity,transform);
            }
        }
        Debug.Log("GridWasCreated");
    }
}

