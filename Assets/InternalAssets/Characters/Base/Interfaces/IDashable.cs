using System;

namespace TheRat
{
    public interface IDashable : IDisposable
    {
        event Action OnDashed;
        void Dash();
    }
}