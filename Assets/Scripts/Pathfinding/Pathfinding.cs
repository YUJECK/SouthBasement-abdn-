using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    private List<bool> visitedPoints;

    public struct Point
    {
        public int x;
        public int y;
        public List<Vector2> path;
    }
}
