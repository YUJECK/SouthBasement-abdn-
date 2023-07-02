using UnityEngine;
using UnityEngine.AI;

namespace SouthBasement.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class AngryRatMovement : MonoBehaviour, IEnemyMovable
    {
        [SerializeField] private NavMeshAgent agent;
        
        public bool Blocked { get; set; }
        public Vector2 CurrentMovement => agent.velocity;

        public void Move(Vector2 to)
        {
            if (!Blocked)
                agent.SetDestination(to);
        }
    }
}