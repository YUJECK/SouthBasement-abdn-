using System;
using SouthBasement.Characters.Components;
using SouthBasement.Enums;
using SouthBasement.InputServices;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters.Rat.Hole
{
    public sealed class RatInHole : Character
    {
        private ComponentFactory _componentsFactory;
        public Animator Animator { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        public IInputService Inputs { get; private set; }
        [field: SerializeField] public AudioSource WalkSource { get; private set; }

        [Inject]
        private void Construct(IInputService inputs, CharacterStats characterStats, DiContainer diContainer)
        {
            Inputs = inputs;
            Stats = characterStats;
            _componentsFactory = new(diContainer);
        }
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponentInChildren<Animator>();

            Components.Add<ICharacterMovable>(new RatInHoleMovement(this));
            Components.Add<IFlipper>(new MouseFlipper<RatInHole>(this, Animator.gameObject, FacingDirections.Left));
            
            Components.Get<ICharacterMovable>().CanMove = false;

            Components.Get<ICharacterMovable>().OnMoved += (_) => Animator.Play("RatWalk");
            Components.Get<ICharacterMovable>().OnMoveReleased += () => Animator.Play("RatIdle");
            
            _componentsFactory.InitializeComponent(Components.Get<IFlipper>() as ICharacterComponent);
        }

        private void Update()
        {
            Components.UpdateALl();
        }

        private void OnDestroy()
            => Components.DisposeAll();
    }
}