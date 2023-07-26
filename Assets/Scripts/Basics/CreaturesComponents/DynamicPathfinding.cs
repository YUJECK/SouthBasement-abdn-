using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CreaturesAI.Pathfinding
{
    [RequireComponent(typeof(Pathfinder))]
    public sealed class DynamicPathfinding : MonoBehaviour
    {
        //variables
        [SerializeField] private float pathfindingRate = 0.5f;
        public UnityEvent<List<Vector2>> onPathWasFound;
        private List<Vector2> path = new List<Vector2>();

        private Pathfinder pathfinder;
        private Coroutine pathFindingCoroutine;

        //getters
        public bool IsNowWorking 
        {
            ///<summary>
            ///simple check pathFindingCoroutine
            ///if it's null - return false
            ///if it isn't return true

            get
            {
                if (pathFindingCoroutine == null) return false;
                else return true;
            }
        }

        //methods
        public void StartPathfinding(Transform target)
        {
            if (!IsNowWorking)
                pathFindingCoroutine = StartCoroutine(Pathfinding(target));
        }
        public void StopPathfinding()
        {
            if (IsNowWorking)
            {
                StopCoroutine(pathFindingCoroutine);
                pathFindingCoroutine = null;
                path.Clear();
            }
        }

        private IEnumerator Pathfinding(Transform target)
        {
            Vector3 previosTargetPosition = Vector3.zero;

            while (true)
            {
                if (previosTargetPosition != target.position)
                {
                    path = pathfinder.FindPath(transform.position, target.position);
                    onPathWasFound.Invoke(path);
                }

                previosTargetPosition = target.position;
                yield return new WaitForSeconds(pathfindingRate);
            }
        }

        //unity methods
        private void Start() => pathfinder = GetComponent<Pathfinder>();
        private void OnDrawGizmosSelected()
        {
            if (path.Count > 0)
            {
                Vector2 previos = transform.position;

                foreach (Vector2 nextPoint in path)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(previos, nextPoint);
                    previos = nextPoint;
                }
            }
        }
    }
}