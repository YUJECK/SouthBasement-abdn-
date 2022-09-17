using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreaturesAI.Pathfinding
{
    [RequireComponent(typeof(Pathfinder))]
    public class DynamicPathfinding : MonoBehaviour
    {
        [SerializeField] private float pathfindingRate = 0.5f;
        private List<Vector2> path;

        [SerializeField] private Transform test;
        private Pathfinder pathfinder;
        [SerializeField] private Utility.ComponentWorkState workState;

        public void StartPathfinding(Transform target) { StartCoroutine(Pathfinding(target)); workState = Utility.ComponentWorkState.Working; }
        public void StopPathfinding() { StopCoroutine(Pathfinding(null)); workState = Utility.ComponentWorkState.Stopped; }

        private IEnumerator Pathfinding(Transform target)
        {
            Vector3 previosTargetPosition = target.position;
            while (true)
            {
                if(previosTargetPosition != target.position)
                    path = pathfinder.FindPath(transform.position, target.position);

                previosTargetPosition = target.position;
                yield return new WaitForSeconds(pathfindingRate);
            }
        }

        private void Start()
        {
            pathfinder = GetComponent<Pathfinder>();
            StartPathfinding(test);
        }
    }
}
