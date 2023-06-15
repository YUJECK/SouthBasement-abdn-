namespace TheRat.Interfaces
{
    public interface ISpawnable
    {
        int SpawnChance { get; }
        
        void OnSpawned();
    }
}