using TheRat.Player;
using UnityEngine;
using Zenject;

namespace TheRat.InternalAssets.Scripts.Characters
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
                FindObjectOfType<Character>().Attackable.OnAttacked += Play;
        }

        public void Play(float time)
        {
            _animator.Play(_rangeAnimation);
        } 
    }
}