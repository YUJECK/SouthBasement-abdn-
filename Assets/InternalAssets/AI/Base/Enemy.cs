using System;
using UnityEngine;

namespace SouthBasement.AI
{
    public abstract class Enemy : MonoBehaviour
    {
        public bool Enabled { get; private set; } = false;

        public event Action OnDied;

        public virtual void Die()
        {
            OnDied?.Invoke();
        }
        
        public virtual void Enable()
        {
            Enabled = true;
        }

        public virtual void Disable()
        {
            Enabled = false;
        }
    }
}