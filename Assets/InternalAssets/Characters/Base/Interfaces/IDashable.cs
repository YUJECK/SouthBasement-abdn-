using System;

namespace SouthBasement
{
    public interface IDashable : IDisposable
    {
        event Action OnDashed;
        void Dash();
    }
}