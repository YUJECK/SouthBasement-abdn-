using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SouthBasement
{
    [RequireComponent(typeof(Slider))]
    public sealed class SliderAudioMixerConnector : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup audioMixer;
        [SerializeField] private string floatName;
        
        private void Awake()
            => GetComponent<Slider>().onValueChanged.AddListener(UpdateVolume);

        private void UpdateVolume(float value)
            => audioMixer.audioMixer.SetFloat(floatName, value);
    }
}