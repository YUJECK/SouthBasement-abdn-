using System;
using SouthBasement.Characters.Base;
using SouthBasement.Characters.Components;
using SouthBasement.Enums;
using SouthBasement.Extensions;
using SouthBasement.InputServices;
using SouthBasement.Scripts.Characters;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters.Rat
{
    public sealed class RatCharacter : Character, ITickable
    {
        public DefaultAttacker Attacker { get; private set; }
        public CharacterAudioPlayer AudioPlayer { get; private set; }
        public AttackRangeAnimator AttackRangeAnimator { get; private set; }
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
        ~RatCharacter() => Components.DisposeAll();

        public void Tick() => Components.UpdateALl();

        public override void OnCharacterPrefabSpawned(CharacterGameObject gameObject)
        {            
            base.OnCharacterPrefabSpawned(gameObject);
            
            Rigidbody = gameObject.gameObject.Get<Rigidbody2D>();
            Animator = gameObject.gameObject.Get<Animator>();
            AudioPlayer = gameObject.gameObject.Get<CharacterAudioPlayer>();
            AttackRangeAnimator = gameObject.gameObject.Get<AttackRangeAnimator>();
            Attacker = gameObject.gameObject.Get<DefaultAttacker>();
            
            CreateComponents();
        }

        private void CreateComponents()
        {
            var playerAnimator = new PlayerAnimator(this);

            Components
                .Add<PlayerAnimator>(playerAnimator)
                .Add<ICharacterAttacker>(new RatAttack(this))
                .Add<ICharacterMovable>(new RatMovement(this))
                .Add<IDashable>(new RatCharacterDashable(this));
            
            var flipper = new CharacterMouseFlipper(this, FacingDirections.Left);
            
            _componentFactory.InitializeComponent(flipper);
            Components.Add<IFlipper>(flipper);

            Components.Get<ICharacterAttacker>().OnAttacked += _ => playerAnimator.PlayAttack();
            
            Components.Get<ICharacterMovable>().OnMoved += _ => playerAnimator.PlayWalk();
            Components.Get<ICharacterMovable>().OnMoveReleased += () => playerAnimator.PlayIdle();
            
            Components.Get<IDashable>().OnDashed += () => playerAnimator.PlayDash();
        }
    }
}