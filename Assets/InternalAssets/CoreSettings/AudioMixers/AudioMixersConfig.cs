using UnityEngine;
using UnityEngine.Audio;

namespace SouthBasement
{
    [CreateAssetMenu]
    public sealed class AudioMixersConfig : ScriptableObject
    {
        public AudioMixer SoundsMixer;
        public AudioMixer MusicMixer;
        public AudioMixerSnapshot DefaultSoundsSnapshot;
        public AudioMixerSnapshot PauseSoundsSnapshot;
        public AudioMixerSnapshot DefaultMusicSnapshot;
        public AudioMixerSnapshot PauseMusicSnapshot;
    }
}