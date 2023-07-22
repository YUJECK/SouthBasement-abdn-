namespace SouthBasement
{
    public interface IRunDatabase
    {
        bool Created { get; }
        
        void Create();
        void Reset();
        virtual void OnCharacterSpawned() { }
    }
}