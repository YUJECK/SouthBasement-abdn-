using System;

namespace SouthBasement.Characters.Components
{
    public abstract class CharacterDashable<TOwner> : CharacterComponent<TOwner>, IDashable
    {
        public event Action OnDashed;

        public bool Blocked { get; set; }

        public abstract void Dash();

        protected void InvokeOnDash() => OnDashed?.Invoke();
    }
}