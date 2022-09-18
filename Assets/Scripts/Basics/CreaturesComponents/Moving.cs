using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreaturesAI.Pathfinding;

namespace CreaturesAI.Moving
{
    [RequireComponent(typeof(DynamicPathfinding))]
    public class Moving : MonoBehaviour
    {
        private List<Vector2> currentPath = new List<Vector2>();
        [SerializeField] private float moveSpeed = 0.05f;

        private DynamicPathfinding dynamicPathfinding;

        //methods
        public void SetPath(List<Vector2> newPath) => currentPath = newPath;
        public void Move()
        {
            if (currentPath.Count > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentPath[0], moveSpeed);

                if (new Vector2(transform.position.x, transform.position.y) == currentPath[0])
                    currentPath.RemoveAt(0);
            }
        }

        //unity methods
        private void Start()
        {
            dynamicPathfinding = GetComponent<DynamicPathfinding>();
            dynamicPathfinding.onPathWasFound.AddListener(SetPath);
        }
        private void FixedUpdate() => Move();
    }
}