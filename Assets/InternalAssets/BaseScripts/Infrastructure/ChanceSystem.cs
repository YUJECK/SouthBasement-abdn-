namespace SouthBasement.Helpers
{
    public static class ChanceSystem
    {
        public static int GetChance()
            => UnityEngine.Random.Range(0, 101);
    }
}