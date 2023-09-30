using System;
using UnityEngine;

namespace SouthBasement.AI
{
    public abstract class Enemy : MonoBehaviour, IWithChance
    {
        [field: SerializeField] public int SpawnChance { get; private set; } = 100;
        public bool Enabled { get; private set; } = false;

        public event Action<Enemy> OnDied;
        public event Action<Enemy> OnEnable;
        public event Action<Enemy> OnDisable;

        public virtual void Die()
        {
            OnDied?.Invoke(this);
        }
        
        public virtual void Enable()
        {
            Enabled = true;
            OnEnable?.Invoke(this);
        }

        public virtual void Disable()
        {
            Enabled = false;
            OnDisable?.Invoke(this);
        }
    }
}