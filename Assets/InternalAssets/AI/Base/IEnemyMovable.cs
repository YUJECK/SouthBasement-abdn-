using System;
using UnityEngine;

namespace SouthBasement.AI
{
    public interface IEnemyMovable
    {
        bool Blocked { get; set; }
        Vector2 CurrentMovement { get; }

        public void Move(Vector2 to, Action onCompleted = null);
    }
}