using SouthBasement.Characters.Components;
using SouthBasement.Enums;
using SouthBasement.InputServices;
using SouthBasement.InternalAssets.Scripts.Characters;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters.Rat
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class RatCharacter : Character, IInitializable
    {
        [field: SerializeField] public DefaultAttacker Attacker { get; private set; }
        [field: SerializeField] public CharacterAudioPlayer AudioPlayer { get; private set; }
        [field: SerializeField] public AttackRangeAnimator AttackRangeAnimator { get; private set; }
        
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public IInputService Inputs { get; private set; }
        public WeaponsUsage WeaponsUsage { get; private set; }
        public StaminaController StaminaController { get; private set; }

        private ComponentFactory _componentFactory;
        
        [Inject]
        private void Construct(IInputService inputs, CharacterStats characterStats, WeaponsUsage weaponsUsage, StaminaController staminaController, DiContainer container)
        {
            _componentFactory = new ComponentFactory(container);
            
            Stats = characterStats;
            
            Inputs = inputs;
            WeaponsUsage = weaponsUsage;
            StaminaController = staminaController;
        }

        private void Update() => Components.UpdateALl();
        private void OnDestroy() => Components.DisposeAll();

        public void Initialize() => CreateComponents();

        private void CreateComponents()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponentInChildren<Animator>();

            var playerAnimator = new PlayerAnimator(this);

            Components
                .Add<PlayerAnimator>(playerAnimator)
                .Add<IAttackable>(new RatAttack(this))
                .Add<ICharacterMovable>(new RatMovement(this))
                .Add<IDashable>(new RatCharacterDashable(this));
            
            var flipper = new CharacterMouseFlipper(this, FacingDirections.Left);
            
            _componentFactory.InitializeComponent(flipper);
            Components.Add<IFlipper>(flipper);

            Components.Get<IAttackable>().OnAttacked += _ => playerAnimator.PlayAttack();
            
            Components.Get<ICharacterMovable>().OnMoved += _ => playerAnimator.PlayWalk();
            Components.Get<ICharacterMovable>().OnMoveReleased += () => playerAnimator.PlayIdle();
            
            Components.Get<IDashable>().OnDashed += () => playerAnimator.PlayDash();
        }
    }
}