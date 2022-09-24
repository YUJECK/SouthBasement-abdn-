using CreaturesAI.Pathfinding;
using UnityEngine;

public sealed class Managers : MonoBehaviour
{
    static private GridManager grid;
    static public GridManager Grid => grid;

    private void Awake()
    {
        grid = FindObjectOfType<GridManager>();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (grid == null)
            grid = FindObjectOfType<GridManager>();
    }
}
