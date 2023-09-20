using UnityEngine;
using UnityEngine.UI;

namespace SouthBasement
{
    [RequireComponent(typeof(Button))]
    public class ButtonAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private void OnValidate()
        {
            if(_audioSource == null)
                GetAudioSource();
        }

        private void Start()
        {
            if(_audioSource == null) 
                GetAudioSource();
            
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void GetAudioSource()
            => _audioSource = GetComponentInChildren<AudioSource>();

        private void OnClick()
            => _audioSource.Play();
    }
}
