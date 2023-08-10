using UnityEngine;
using UnityEngine.Audio;

namespace TheRat
{
    public sealed class AudioMixersService
    {
        public AudioMixer SoundsMixer { get; private set; }
        public AudioMixer MusicMixer { get; private set; }


        public AudioMixersService(AudioMixer soundsMixer, AudioMixer musicMixer)
        {
            SoundsMixer = soundsMixer;
            MusicMixer = musicMixer;
            
            SoundsMixer.updateMode = AudioMixerUpdateMode.Normal;
            MusicMixer.updateMode = AudioMixerUpdateMode.Normal;
        }
    }

    [CreateAssetMenu]
    public sealed class AudioMixersServiceConfig : ScriptableObject
    {
        public AudioMixer SoundsMixer;
        public AudioMixer MusicMixer;
    }
}
