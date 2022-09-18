using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CreaturesAI.Pathfinding
{
    [RequireComponent(typeof(Pathfinder))]
    public sealed class DynamicPathfinding : MonoBehaviour
    {
        [SerializeField] private float pathfindingRate = 0.5f;
        public UnityEvent<List<Vector2>> onPathWasFound;
        private Utility.ComponentWorkState workState;
        private List<Vector2> path = new List<Vector2>();


        private Pathfinder pathfinder;

        //methods
        public void StartPathfinding(Transform target)
        {
            if (workState != Utility.ComponentWorkState.Working)
            {
                StartCoroutine(Pathfinding(target));
                workState = Utility.ComponentWorkState.Working;
            }
        }
        public void StopPathfinding()
        {
            if (workState == Utility.ComponentWorkState.Working)
            {
                StopCoroutine(Pathfinding(null));
                workState = Utility.ComponentWorkState.Stopped;
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
                Vector2 previos = path[0];
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
