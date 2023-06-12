using UnityEngine;

namespace TheRat.AI
{
    public abstract class Enemy : MonoBehaviour
    {
        public bool Enabled { get; private set; } = false;

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