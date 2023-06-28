using SouthBasement.AI;
using UnityEngine;

namespace TheRat
{
    public sealed class SpiderMovement : MonoBehaviour, IMovable
    {
        public void Move(Vector2 to)
        {
            transform.position = to;
        }
    }
}