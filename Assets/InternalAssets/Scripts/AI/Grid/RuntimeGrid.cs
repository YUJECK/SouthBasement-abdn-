using NavMeshPlus.Components;
using UnityEngine;

namespace AutumnForest.AI.Grid
{
    public sealed class RuntimeGrid : MonoBehaviour
    {
        public NavMeshSurface Surface2D;

        private void Start()
        {
            GetComponent<NavMeshSurface>().BuildNavMeshAsync();
        }
    }
}