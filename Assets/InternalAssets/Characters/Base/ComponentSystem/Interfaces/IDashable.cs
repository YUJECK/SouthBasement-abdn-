using System;

namespace SouthBasement.Characters.Components
{
    public interface IDashable
    {
        event Action OnDashed;

        bool Blocked { get; set; }

        void Dash();
    }
}