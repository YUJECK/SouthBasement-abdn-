namespace SouthBasement.Characters.Components
{
    public abstract class CharacterComponent<TOwner> : ICharacterComponent
    {
        public TOwner Owner { get; protected set; }
        
        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void Dispose() { }
    }
}