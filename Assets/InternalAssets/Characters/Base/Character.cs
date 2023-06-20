using UnityEngine;

namespace SouthBasement.Characters
{
    public abstract class Character : MonoBehaviour
    {
        [field: SerializeField] public CharacterConfig CharacterConfig { get; private set; }
        
        public IMovable Movable { get; protected set; }
        public IAttackable Attackable { get; protected set; }
        public IDashable Dashable { get; protected set; }
        public PlayerAnimator PlayerAnimator { get; protected set; }
    
        public CharacterStats Stats { get; protected set; }
    }
}