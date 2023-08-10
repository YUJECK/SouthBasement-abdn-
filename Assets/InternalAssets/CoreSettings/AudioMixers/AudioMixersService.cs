using UnityEngine.Audio;

namespace SouthBasement
{
    public sealed class AudioMixersService
    {
        public AudioMixer SoundsMixer { get; private set; }
        public AudioMixer MusicMixer { get; private set; }

        private readonly AudioServiceConfig _audioServiceConfig;
        
        public AudioMixersService(AudioServiceConfig audioServiceConfig)
        {
            SoundsMixer = audioServiceConfig.SoundsMixer;
            MusicMixer = audioServiceConfig.MusicMixer;

            _audioServiceConfig = audioServiceConfig;
        }

        public void PauseAllSounds()
        {
            _audioServiceConfig.PauseSoundsSnapshot    
        }
    }
}
