using UnityEngine.Audio;

namespace SouthBasement
{
    public sealed class AudioMixersService
    {
        public AudioMixer MasterMixer { get; private set; }

        private readonly AudioMixersServiceConfig _audioServiceConfig;
        
        public AudioMixersService(AudioMixersServiceConfig audioServiceConfig)
        {
            MasterMixer = audioServiceConfig.MasterMixer;

            _audioServiceConfig = audioServiceConfig;
        }

        public void PauseAllAudio()
        {
            //_audioServiceConfig.PauseSnapshot.TransitionTo(0f);
        }

        public void UppauseAllAudio()
        {
            //_audioServiceConfig.CurrentSnapshot.TransitionTo(0f);
        }
    }
}
