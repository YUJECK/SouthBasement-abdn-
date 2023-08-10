using UnityEngine;
using UnityEngine.Audio;

namespace TheRat
{
    [CreateAssetMenu]
    public sealed class AudioMixersServiceConfig : ScriptableObject
    {
        public AudioMixer SoundsMixer;
        public AudioMixer MusicMixer;
    }
}