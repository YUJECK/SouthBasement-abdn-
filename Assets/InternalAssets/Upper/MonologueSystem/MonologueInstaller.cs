using Zenject;

namespace SouthBasement.MonologueSystem
{
    public class MonologueInstaller : MonoInstaller
    {
        public MonologuePanelConfig monologuePanelConfig;
        
        public override void InstallBindings()
        {
            Container
                .Bind<MonologuePanelConfig>()
                .FromInstance(monologuePanelConfig)
                .AsSingle();

            Container
                .Bind<MonologueManager>()
                .FromInstance(new MonologueManager())
                .AsSingle();
        }
    }
}