using System;

namespace SouthBasement.Characters.Components
{
    public interface ICharacterComponent : IDisposable
    {
        void OnStart();
        void OnUpdate();
        void Dispose();
    }
}