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

        private void Update() => ComponentContainer.UpdateALl();
        private void OnDestroy() => ComponentContainer.DisposeAll();

        public void Initialize()
        {
            CreateComponents();
            ComponentContainer.StartAll();
        }

        private void CreateComponents()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponentInChildren<Animator>();

            var playerAnimator = new PlayerAnimator(this);

            ComponentContainer.AddComponent<PlayerAnimator>(playerAnimator);
            ComponentContainer.AddComponent<IAttackable>(new RatAttack(this));
            ComponentContainer.AddComponent<IMovable>(new RatMovement(this));
            ComponentContainer.AddComponent<IDashable>(new RatCharacterDashable(this));
            
            var flipper = new CharacterMouseFlipper(this, FacingDirections.Left);
            _componentFactory.InitializeComponent(flipper);
            
            ComponentContainer.AddComponent<IFlipper>(flipper);

            ComponentContainer.GetComponent<IAttackable>().OnAttacked += _ => playerAnimator.PlayAttack();
            
            ComponentContainer.GetComponent<IMovable>().OnMoved += _ => playerAnimator.PlayWalk();
            ComponentContainer.GetComponent<IMovable>().OnMoveReleased += () => playerAnimator.PlayIdle();
            
            ComponentContainer.GetComponent<IDashable>().OnDashed += () => playerAnimator.PlayDash();
        }
    }
}