using SouthBasement.Characters;
using SouthBasement.Characters.Rat;
using Zenject;

namespace SouthBasement
{
    public sealed class CharacterRunDatabase : IRunDatabase
    {
        public bool Created { get; private set; }
        public Character Character { get; private set; }

        private readonly DiContainer _diContainer;

        public CharacterRunDatabase(DiContainer diContainer) 
            => _diContainer = diContainer;

        public void Create()
        {
            if(Created) return;
            
            CreateNewCharacterInstance();

            _diContainer
                .Bind<Character>()
                .FromInstance(Character)
                .AsCached();

            Created = true;
        }

        public void Remove()
        {
            Created = false;
            Character.Components.DisposeAll();
            _diContainer.Unbind<Character>();
        }

        private void CreateNewCharacterInstance()
        {
            Character?.Components.DisposeAll();

            Character = new RatCharacter();
            _diContainer.Inject(Character);
        }

        public void Reset()
        {
            if (!Created)
            {
                Create();
                return;
            }

            CreateNewCharacterInstance();

            _diContainer
                .Rebind<Character>()
                .FromInstance(Character)
                .AsCached();
        }
    }
}