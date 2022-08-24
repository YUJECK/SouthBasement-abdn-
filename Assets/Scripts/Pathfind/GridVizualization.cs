using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridVizualization : MonoBehaviour
{
    private List<GameObject> gridVizualization = new List<GameObject>();
    [SerializeField] private GameObject colliderPrefab;
    private Grid grid;

    public void ShowGrid()
    {
        if(grid.IsGridCreated)
        {
            for (int x = 0; x < grid.GridWidth; x++)
            {
                for (int y = 0; y < grid.GridHeight; y++)
                {
                    if (grid.GetGridPoint((int)x, (int)y) == 1)
                        gridVizualization.Add(Instantiate(colliderPrefab, new Vector3(x, y, 0), Quaternion.identity, transform));
                }
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
}
