using Zenject;

namespace SouthBasement
{
    public sealed class AudioMixersServiceInstaller : MonoInstaller
    {
        public AudioMixersServiceConfig _audioMixersServiceConfig;

        public override void InstallBindings()
        {
            _audioMixersServiceConfig = Instantiate(_audioMixersServiceConfig);
            
            Container.Bind<AudioMixersService>().FromInstance(new AudioMixersService(_audioMixersServiceConfig));
        }
    }
}
