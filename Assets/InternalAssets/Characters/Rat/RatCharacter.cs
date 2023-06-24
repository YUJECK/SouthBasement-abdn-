using SouthBasement.Characters.Components;
using SouthBasement.InputServices;
using SouthBasement.InternalAssets.Scripts.Characters;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters.Rat
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class RatCharacter : Character
    {
        [field: SerializeField] public DefaultAttacker Attacker { get; private set; }
        [field: SerializeField] public AttackRangeAnimator AttackRangeAnimator { get; private set; }
        
        public Rigidbody2D Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public IInputService Inputs { get; private set; }
        public WeaponsUsage WeaponsUsage { get; private set; }
        public StaminaController StaminaController { get; private set; }

        [Inject]
        private void Construct(IInputService inputs, CharacterStats characterStats, WeaponsUsage weaponsUsage, StaminaController staminaController)
        {
            Stats = characterStats;
            
            Inputs = inputs;
            WeaponsUsage = weaponsUsage;
            StaminaController = staminaController;
        }

        private void Awake() => CreateComponents();

        private void Start() => ComponentContainer.StartAll();
        private void Update() => ComponentContainer.UpdateALl();
        private void OnDestroy() => ComponentContainer.DisposeAll();

        private void CreateComponents()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponentInChildren<Animator>();

            var playerAnimator = new PlayerAnimator(this);

            ComponentContainer.AddComponent<PlayerAnimator>(playerAnimator, false);
            ComponentContainer.AddComponent<IAttackable>(new RatAttack(this), false);
            ComponentContainer.AddComponent<IMovable>(new RatMovement(this), false);
            ComponentContainer.AddComponent<IDashable>(new RatCharacterDashable(this), false);

            ComponentContainer.GetCharacterComponent<IAttackable>().OnAttacked += _ => playerAnimator.PlayAttack();
            
            ComponentContainer.GetCharacterComponent<IMovable>().OnMoved += _ => playerAnimator.PlayWalk();
            ComponentContainer.GetCharacterComponent<IMovable>().OnMoveReleased += () => playerAnimator.PlayIdle();
            
            ComponentContainer.GetCharacterComponent<IDashable>().OnDashed += () => playerAnimator.PlayDash();
        }
    }
}