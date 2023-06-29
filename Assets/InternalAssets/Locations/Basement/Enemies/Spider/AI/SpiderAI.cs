using NTC.ContextStateMachine;
using SouthBasement.AI;
using SouthBasement.Generation;
using UnityEngine;

namespace SouthBasement
{
    [RequireComponent(typeof(SpiderMovement))]
    public class SpiderAI : Enemy, IEnemiesHandlerRequire, IMovePointsRequire
    {
        [field: SerializeField] public MovePoint[] MovePoints { get; private set; }
        public SpiderComponentContainer Components { get; private set; }

        public bool CurrentlyHiding { get; set; }
        public bool CurrentlyAttacking { get; set; } 
        public int AttackStrike { get; set; }

        private readonly StateMachine<SpiderAI> _spiderStateMachine = new();
        private EnemiesHandler _enemiesHandler;

        public void Initialize(EnemiesHandler handler) => _enemiesHandler = handler;
        public void Initialize(MovePoint[] movePoints) => MovePoints = movePoints;

        private void Awake()
        {
            Components = new SpiderComponentContainer(gameObject);
            
            InitStates();
            Enable();
        }

        private void InitStates()
        {
            _spiderStateMachine.AddStates(new SpiderMoveState(this), new SpiderAttackState(this), new SpiderAFKState(this));
            
            _spiderStateMachine.AddAnyTransition<SpiderAFKState>(CanEnterAFK);
            _spiderStateMachine.AddAnyTransition<SpiderMoveState>(CanEnterMoveState);
            
            _spiderStateMachine.AddTransition<SpiderMoveState, SpiderAttackState>(CanEnterWeaveState);
            _spiderStateMachine.AddTransition<SpiderAFKState, SpiderAttackState>(CanEnterWeaveState);
            
            _spiderStateMachine.TransitionsEnabled = true;
        }

        public bool CanEnterAFK() => !Enabled;

        private bool CanEnterWeaveState()
        {
            return Enabled
                   && Components.TargetSelector.Target != null
                   && !CurrentlyHiding
                   && AttackStrike < 3;
        }

        private bool CanEnterMoveState() 
            => Enabled 
               && (Components.TargetSelector.Target == null 
               && !CurrentlyAttacking) 
               || AttackStrike >=3;

        private bool CanEnterFallDownState()
            => Enabled
               && _enemiesHandler.IsEnemyCategoryAlone<SpiderAI>()
               && !CurrentlyAttacking
               && !CurrentlyHiding;

        private void Update()
        {
            if (Enabled)
            {
                Debug.Log("Run");
                _spiderStateMachine.Run();
            }
        }
    }
}
