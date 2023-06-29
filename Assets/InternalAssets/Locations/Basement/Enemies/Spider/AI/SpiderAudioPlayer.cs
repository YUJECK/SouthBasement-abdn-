using UnityEngine;

namespace SouthBasement
{
    public sealed class SpiderAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource weaveSound;
        [SerializeField] private AudioSource walkSound;

        public void PlayWeave() => weaveSound.Play();
        public void StopWeave() => weaveSound.Stop();
        
        public void PlayWalk() => walkSound.Play();
        public void StopWalk() => walkSound.Stop();
    }
}