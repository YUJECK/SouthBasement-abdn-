using SouthBasement.Economy;
using Zenject;

namespace SouthBasement
{
    public sealed class CheeseRunDatabase : IRunDatabase
    {
        public CheeseService CheeseService { get; private set; }
        
        private DiContainer _diContainer;

        public bool Created { get; private set; }

        public void Create()
        {
            if (Created) return;

            CheeseService = new CheeseService(_diContainer);
            Created = true;
        }

        public void Reset() => CheeseService = new(_diContainer);
    }
}