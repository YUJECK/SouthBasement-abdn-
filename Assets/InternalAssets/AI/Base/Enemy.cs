using System;
using SouthBasement.InternalAssets.Infrastructure;
using UnityEngine;

namespace SouthBasement.AI
{
    public abstract class Enemy : MonoBehaviour, IWithChance
    {
        [field: SerializeField] public int SpawnChance { get; private set; } = 100;
        public bool Enabled { get; private set; } = false;

        public event Action<Enemy> OnDied;

        public virtual void Die()
        {
            OnDied?.Invoke(this);
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