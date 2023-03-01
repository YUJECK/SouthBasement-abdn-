using UnityEngine;

namespace TheRat.Player
{
    public abstract class Character : MonoBehaviour
    {
        public IMovable Movable { get; protected set; }
        public IAttackable Attackable { get; protected set; }
        public IDashable Dashable { get; protected set; }
    
        public CharacterStats Stats { get; protected set; }
    }
}