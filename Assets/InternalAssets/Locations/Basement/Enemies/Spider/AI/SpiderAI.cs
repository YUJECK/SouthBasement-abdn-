using NTC.ContextStateMachine;
using SouthBasement.AI;
using SouthBasement.AI.MovePoints;
using SouthBasement.Generation;
using UnityEngine;

namespace SouthBasement
{
    [RequireComponent(typeof(SpiderMovement))]
    public class SpiderAI : Enemy, IEnemiesHandlerRequire, IMovePointsRequire
    {
        [field: SerializeField] public MovePointsHandler MovePoints { get; private set; }
        
        public SpiderComponentContainer Components { get; private set; }

        public bool CurrentlyHiding { get; set; }
        public bool FallenDown { get; private set; }
        public bool CurrentlyAttacking { get; set; } 
        public int AttackStrike { get; set; }

        private readonly StateMachine<SpiderAI> _spiderStateMachine = new();
        private EnemiesHandler _enemiesHandler;

        public void Initialize(EnemiesHandler handler) => _enemiesHandler = handler;
        public void Initialize(MovePointsHandler movePoints) => MovePoints = movePoints;

        public void Fall()
        {
            FallenDown = true;
            Components.SpiderMovement.ActivateNavMesh();
        }
        
        private void Awake()
        {
            Components = new SpiderComponentContainer(gameObject);
            
            InitStates();
            Enable();
        }

        private void InitStates()
        {
            _spiderStateMachine.AddStates(new SpiderMoveState(this), new SpiderWeaveState(this), new SpiderAFKState(this), new SpiderFloorMoveState(this));
            
            _spiderStateMachine.AddAnyTransition<SpiderAFKState>(CanEnterAFK);
            _spiderStateMachine.AddAnyTransition<SpiderMoveState>(CanEnterMoveState);
            _spiderStateMachine.AddAnyTransition<SpiderFloorMoveState>(CanEnterFallDownState);
            
            _spiderStateMachine.AddTransition<SpiderMoveState, SpiderWeaveState>(CanEnterWeaveState);
            _spiderStateMachine.AddTransition<SpiderAFKState, SpiderWeaveState>(CanEnterWeaveState);
            
            _spiderStateMachine.TransitionsEnabled = true;
        }

        public bool CanEnterAFK() => !Enabled;

        private bool CanEnterWeaveState()
        {
            return Enabled
                   && !FallenDown
                   && Components.TargetSelector.Target != null
                   && !CurrentlyHiding
                   && AttackStrike < 3;
        }

        private bool CanEnterMoveState() 
            => Enabled 
               && !FallenDown
               && (Components.TargetSelector.Target == null 
               && !CurrentlyAttacking) 
               || AttackStrike >=3;

        private bool CanEnterFallDownState()
            => Enabled
               && !CurrentlyAttacking
               && _enemiesHandler.IsEnemyCategoryAlone<SpiderAI>();

        private void Update()
        {
            if (Enabled)
                _spiderStateMachine.Run();
        }
    }
}
