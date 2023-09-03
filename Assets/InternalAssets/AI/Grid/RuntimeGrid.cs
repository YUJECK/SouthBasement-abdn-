using NavMeshPlus.Components;
using UnityEngine;

namespace SouthBasement.AI.Grid
{
    public sealed class RuntimeGrid : MonoBehaviour
    {
        private NavMeshSurface _surface2D;

        private void Start()
        {
            GetComponent<NavMeshSurface>().BuildNavMeshAsync();
        }
    }
}