using UnityEngine;
using UnityEngine.Audio;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Infrastructure + "AudioMixersServiceConfig")]
    public sealed class AudioMixersServiceConfig : ScriptableObject
    {
        public AudioMixer MasterMixer;
        public AudioMixerSnapshot CurrentSnapshot;
        public AudioMixerSnapshot PauseSnapshot;
    }
}