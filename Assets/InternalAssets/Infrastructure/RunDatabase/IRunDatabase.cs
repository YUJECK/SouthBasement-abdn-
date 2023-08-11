namespace SouthBasement
{
    public interface IRunDatabase
    {
        bool Created { get; }
        
        void Create();
        void Remove();
        void Reset();
        virtual void OnCharacterSpawned() { }
    }
}