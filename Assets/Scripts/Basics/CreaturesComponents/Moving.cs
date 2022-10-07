using CreaturesAI.Pathfinding;
using System.Collections.Generic;
using UnityEngine;

namespace CreaturesAI
{
    [RequireComponent(typeof(DynamicPathfinding))]
    public class Moving : MonoBehaviour
    {
        private List<Vector2> currentPath = new List<Vector2>();
        [SerializeField] private float defaultMoveSpeed = 0.05f;

        private DynamicPathfinding dynamicPathfinding;

        //methods
        public void SetPath(List<Vector2> newPath) => currentPath = newPath;
        public void Move(float moveSpeed = 0f)
        {
            if (moveSpeed == 0f) moveSpeed = defaultMoveSpeed;

            if (currentPath.Count > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentPath[0], moveSpeed);

                if (new Vector2(transform.position.x, transform.position.y) == currentPath[0])
                    currentPath.RemoveAt(0);
            }
        }

        //unity methods
        private void Awake()
        {
            dynamicPathfinding = GetComponent<DynamicPathfinding>();
            dynamicPathfinding.onPathWasFound.AddListener(SetPath);
        }
    }
}