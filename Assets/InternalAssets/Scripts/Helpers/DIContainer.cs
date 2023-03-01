using Zenject;

namespace Assets.InternalAssets.Scripts
{
    public static class DIContainer
    {
        private static DiContainer diContainer;

        public static DiContainer DiContainer => diContainer;
    }
}