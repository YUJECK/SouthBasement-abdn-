using UnityEngine;

namespace SouthBasement.Garden.Interactions
{
    public sealed class WellAudioPlayer : MonoBehaviour
    {        
        [SerializeField] private AudioSource fallenSource;
        [SerializeField] private AudioSource ropeSource;

        private void PlayFallenSound() => fallenSource.Play();

        private void PlayRopeSound() => ropeSource.Play();
    }
}