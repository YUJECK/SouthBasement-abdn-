using UnityEngine.Audio;
using Zenject;

namespace SouthBasement
{
    public sealed class AudioMixersServiceInstaller : MonoInstaller
    {
        public AudioMixersConfig _audioMixersServiceConfig;
        private AudioMixerSnapshot 

        public override void InstallBindings()
        {
            Container.Bind<AudioMixersService>().FromInstance(new(_audioMixersServiceConfig));
        }
    }
}
