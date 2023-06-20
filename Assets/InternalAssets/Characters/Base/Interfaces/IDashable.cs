using System;

namespace SouthBasement
{
    public interface IDashable : IDisposable
    {
        event Action OnDashed;

        bool Blocked { get; set; }

        void Dash();
    }
}