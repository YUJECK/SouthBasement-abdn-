using System;

namespace TheRat
{
    public interface IAttackable
    {
        event Action<float> OnAttacked; 
        void Attack();
    }
}