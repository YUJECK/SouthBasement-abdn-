using SouthBasement.AI;
using UnityEngine;
using UnityEngine.AI;

namespace SouthBasement
{
    public sealed class SpiderMovement : MonoBehaviour, IEnemyMovable
    {
        [SerializeField] private Behaviour[] _navMeshcomponents;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public bool Blocked { get; set; }
        public Vector2 CurrentMovement { get; }
        
        public void Move(Vector2 to) => transform.position = to;

        public void Walk(Vector2 to) => _navMeshAgent.SetDestination(to);

        public void ActivateNavMesh()
        {
            foreach (var navMeshcomponent in _navMeshcomponents)
                navMeshcomponent.enabled = true;
        }
    }
}