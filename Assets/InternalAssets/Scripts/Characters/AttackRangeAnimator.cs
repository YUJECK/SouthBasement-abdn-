using System;
using TheRat.InputServices;
using UnityEngine;
using Zenject;

namespace TheRat.InternalAssets.Scripts.Characters
{
    public sealed class AttackRangeAnimator : MonoBehaviour
    {
        private Animator _animator;

        private readonly int _rangeAnimation = Animator.StringToHash("AttackRange");

        [Inject]
        private void Construct(IInputService inputService) => inputService.OnAttack += Play;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Play() => _animator.Play(_rangeAnimation);
    }
}