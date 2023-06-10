using NavMeshPlus.Components;
using UnityEngine;

namespace TheRat.AI.Grid
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