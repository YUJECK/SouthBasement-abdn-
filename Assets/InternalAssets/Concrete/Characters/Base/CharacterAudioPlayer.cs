using UnityEngine;

namespace SouthBasement
{
    public class CharacterAudioPlayer : MonoBehaviour
    {
        public AudioSource walkSound;
        [SerializeField] private AudioSource dashSound;
        [SerializeField] private AudioSource attackSound;
        [SerializeField] private AudioSource hittedSound;

        public void PlayWalk()
        {
            if(!walkSound.isPlaying)
                walkSound.Play();
        }

        public void PlayDash()
        {
            if(walkSound.isPlaying)
                walkSound.Stop();

            dashSound.pitch = Random.Range(0.8f, 1.2f);
            dashSound.Play();
        }

        
        public void StopWalk()
        {
            walkSound.Stop();
        }

        public void PlayAttack()
        {            
            attackSound.pitch = Random.Range(0.8f, 1.2f);
            attackSound.Play();
        }

        public void PlayHitted()
        {
            hittedSound.Play();
        }
    }
}
