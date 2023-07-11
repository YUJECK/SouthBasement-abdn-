using SouthBasement.Characters.Components;
using SouthBasement.Enums;
using SouthBasement.InputServices;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters.Hole.Rat
{
    public sealed class RatDummy : CharacterDummy
    {
        [field: SerializeField] public CharacterConfig RatConfig { get; set; }
        [field: SerializeField] public AudioSource WalkSource { get; set; }
        
        private ComponentFactory _componentsFactory;
        public Animator Animator { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        public IInputService Inputs { get; private set; }

        [Inject]
        private void Construct(IInputService inputs, DiContainer diContainer)
        {
            Inputs = inputs;
            _componentsFactory = new ComponentFactory(diContainer);
        }
        public void Awake()
        {
            GetComponents();
            
            Components.Add<ICharacterMovable>(new RatInHoleMovement(this));
            Components.Add<IFlipper>(new MouseFlipper<RatDummy>(this, Animator.gameObject, FacingDirections.Left));
            
            Components.Get<ICharacterMovable>().CanMove = false;

            Components.Get<ICharacterMovable>().OnMoved += (_) => Animator.Play("RatWalk");
            Components.Get<ICharacterMovable>().OnMoveReleased += () => Animator.Play("RatIdle");
            
            _componentsFactory.InitializeComponent(Components.Get<IFlipper>() as ICharacterComponent);
        }

        private void Update() => Components.UpdateALl();
        private void OnDestroy() => Components.DisposeAll();

        public void GetComponents()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponentInChildren<Animator>();
        }

        public override CharacterConfig GetConfig() => RatConfig;
    }
}