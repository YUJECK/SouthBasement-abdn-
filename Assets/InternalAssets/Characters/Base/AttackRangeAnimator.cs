using UnityEngine;

namespace SouthBasement.Scripts.Characters
{
    [RequireComponent(typeof(Animator))]
    public sealed class AttackRangeAnimator : MonoBehaviour
    {
        private Animator _animator;
        private readonly int _rangeAnimation = Animator.StringToHash("AttackRange");
        
        private void Start() => _animator = GetComponent<Animator>();

        public void Play() => _animator.Play(_rangeAnimation);
    }
}