using NavMeshPlus.Components;
using UnityEngine;

namespace AutumnForest.AI.Grid
{
    public sealed class RuntimeGrid : MonoBehaviour
    {
        public NavMeshSurface Surface2D;

        void Start()
        {
            Surface2D.BuildNavMeshAsync();
        }
    }
}