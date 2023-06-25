using SouthBasement.Characters;
using SouthBasement.Characters.Components;
using UnityEngine;

namespace SouthBasement.InternalAssets.Scripts.Characters
{
    [RequireComponent(typeof(Animator))]
    public sealed class AttackRangeAnimator : MonoBehaviour
    {
        private Animator _animator;

        private readonly int _rangeAnimation = Animator.StringToHash("AttackRange");

        [SerializeField] private bool asPlayer;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();

            if(asPlayer)
                FindObjectOfType<Character>()
                    .ComponentContainer
                    .GetComponent<IAttackable>()
                    .OnAttacked += (_) => Play();
        }

        public void Play()
        {
            _animator.Play(_rangeAnimation);
        } 
    }
}