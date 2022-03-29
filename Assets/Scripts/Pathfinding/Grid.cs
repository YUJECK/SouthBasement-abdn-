using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GameObject _collider;
    [SerializeField] private GameObject emptyArea;
    [SerializeField] private int[,] grid;
    // 0 - нет коллайлера/это триггер
    // 1 - есть коллайлера
    
    // Выстоа и ширина сетки 
    [SerializeField] private int gridWidth;  
    [SerializeField] private int gridHeight;

    [SerializeField] private int nodeSize;

    private void Awake()
    {
        Camera camera = Camera.main;
        
        gridWidth = (int)Mathf.Ceil(gridWidth/nodeSize);
        gridHeight = (int)Mathf.Ceil(gridHeight/nodeSize);

        grid = new int[gridWidth, gridHeight];

    
        for(int x = 0; x < gridWidth*nodeSize; x+=nodeSize)
        {
            for(int y = 0; y < gridHeight*nodeSize; y+=nodeSize)
            {
                Vector2 pointPos = camera.ScreenToWorldPoint(new Vector3(x,y,0));
                Debug.Log(pointPos);
                RaycastHit2D pointObj = Physics2D.Raycast(pointPos, Vector2.zero);
            
                if(pointObj.collider == null)
                    grid[x/nodeSize,y/nodeSize] = 0;
                else
                {
                    if(pointObj.collider.isTrigger)
                        grid[x/nodeSize,y/nodeSize] = 0;
                    else
                        grid[x/nodeSize,y/nodeSize] = 1;
                }
                
                if(grid[x,y] == 0)
                    Instantiate(emptyArea,new Vector3(pointPos.x,pointPos.y,0),Quaternion.identity,transform);
                else
                    Instantiate(_collider,new Vector3(pointPos.x,pointPos.y,0),Quaternion.identity,transform);
            }
        }
    }
}

