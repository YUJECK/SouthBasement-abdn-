using UnityEngine;

namespace TheRat.AI
{
    public sealed class SingleAnimationService : MonoBehaviour
    {
        [SerializeField] private string animationName;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Play() => _animator.Play(animationName);
    }
}