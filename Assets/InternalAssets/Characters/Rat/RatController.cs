using System;
using SouthBasement.InputServices;
using SouthBasement.InternalAssets.Scripts.Characters;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters.Rat
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class RatController : Character
    {
        [SerializeField] private DefaultAttacker attacker;
        [SerializeField] private AttackRangeAnimator attackRangeAnimator;
        
        private Rigidbody2D _rigidbody;
        private IInputService _inputs;

        private WeaponsUsage _weaponsUsage;
        private StaminaController _staminaController;

        [Inject]
        private void Construct(IInputService inputs, CharacterStats characterStats, WeaponsUsage weaponsUsage, StaminaController staminaController)
        {
            this._inputs = inputs;
            this.Stats = characterStats;
            _weaponsUsage = weaponsUsage;
            _staminaController = staminaController;
        }

        private void Awake()
        {
            CreateComponents();
            AddAnimationsPlayCallbacks();
        }

        private void CreateComponents()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            PlayerAnimator = new(GetComponentInChildren<Animator>(), attackRangeAnimator, _weaponsUsage);

            Movable = new RatMovable(_inputs, _rigidbody, Stats.MoveStats);
            Attackable = new RatAttack(_inputs, Stats.AttackStats, attacker, _weaponsUsage, _staminaController);
            Dashable = new RatDashable(_inputs, Movable, transform,  _staminaController, Stats.MoveStats, this);
        }

        private void AddAnimationsPlayCallbacks()
        {
            Movable.OnMoved += _ => PlayerAnimator.PlayWalk();
            Movable.OnMoveReleased += () => PlayerAnimator.PlayIdle();
            Attackable.OnAttacked += (_) => PlayerAnimator.PlayAttack();
            Dashable.OnDashed += () => PlayerAnimator.PlayDash();
        }

        private void OnDestroy()
        {
            Movable.Dispose();
            Dashable.Dispose();
            Attackable.Dispose();
        }
    }
}