using Zenject;

namespace SouthBasement.Characters.Components
{
    public sealed class ComponentFactory
    {
        private DiContainer _container;

        public ComponentFactory(DiContainer container)
            => _container = container;

        public void InitializeComponent(ICharacterComponent component)
            => _container.Inject(component);
    }
}