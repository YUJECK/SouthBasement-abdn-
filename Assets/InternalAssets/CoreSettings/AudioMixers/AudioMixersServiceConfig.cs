using UnityEngine;
using UnityEngine.Audio;

namespace SouthBasement
{
    [CreateAssetMenu]
    public sealed class AudioMixersServiceConfig : ScriptableObject
    {
        public AudioMixer MasterMixer;
        public AudioMixerSnapshot CurrentSnapshot;
        public AudioMixerSnapshot PauseSnapshot;
    }
}