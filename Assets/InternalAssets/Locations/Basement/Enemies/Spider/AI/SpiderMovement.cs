using SouthBasement.AI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace SouthBasement
{
    public sealed class SpiderMovement : MonoBehaviour, IMovable
    {
        [SerializeField] private Behaviour[] _navMeshcomponents;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        public void MoveUp(Vector2 to)
        {
            transform.position = to;
        }
        public void Walk(Vector2 to)
        {
            _navMeshAgent.SetDestination(to);
        }

        public void ActivateNavMesh()
        {
            foreach (var navMeshcomponent in _navMeshcomponents)
                navMeshcomponent.enabled = true;
        }
    }
}