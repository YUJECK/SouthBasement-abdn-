namespace SouthBasement.Interfaces
{
    public interface ISpawnable
    {
        int SpawnChance { get; }
        
        void OnSpawned();
    }
}