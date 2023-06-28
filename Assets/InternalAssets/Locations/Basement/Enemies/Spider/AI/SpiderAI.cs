using NTC.ContextStateMachine;
using SouthBasement.AI;
using UnityEngine;

namespace SouthBasement
{
    [RequireComponent(typeof(SpiderMovement))]
    public class SpiderAI : Enemy
    {
        [field: SerializeField] public Transform[] MovePoints { get; private set; }
        public SpiderComponentContainer Components { get; private set; }

        public bool CurrentlyHiding { get; set; }
        public bool CurrentlyAttacking { get; set; }
        public int AttackStrike { get; set; }

        private readonly StateMachine<SpiderAI> _spiderStateMachine = new();

        private void Awake()
        {
            Components = new SpiderComponentContainer(gameObject);
            
            InitStates();
            Enable();
        }

        private void InitStates()
        {
            _spiderStateMachine.AddStates(new SpiderMoveState(this), new SpiderAttackState(this));
            
            _spiderStateMachine.AddTransition<SpiderAttackState, SpiderMoveState>(CanEnterMoveState);
            _spiderStateMachine.AddTransition<SpiderMoveState, SpiderAttackState>(CanEnterWeaveState);
            
            _spiderStateMachine.TransitionsEnabled = true;
        }

        private bool CanEnterWeaveState()
            => Enabled
               && Components.TargetSelector.Target != null 
               && !CurrentlyHiding 
               && AttackStrike < 3;
        
        private bool CanEnterMoveState() 
            => Enabled 
               && Components.TargetSelector.Target == null 
               && !CurrentlyAttacking 
               || AttackStrike >=3;

        private void Update() => _spiderStateMachine.Run();

        public sealed class SpiderComponentContainer
        {
            public SpiderMovement SpiderMovement { get; private set; }
            public SpiderWeaver SpiderWeaver { get; private set; }
            public TargetSelector TargetSelector { get; private set; }
            public SpiderAnimator SpiderAnimator { get; private set; }

            public SpiderComponentContainer(GameObject masterObject)
            {
                SpiderMovement = masterObject.GetComponent<SpiderMovement>();
                TargetSelector = masterObject.GetComponentInChildren<TargetSelector>();
                SpiderWeaver = masterObject.GetComponentInChildren<SpiderWeaver>();
                SpiderAnimator = new SpiderAnimator(masterObject.GetComponentInChildren<Animator>());
            }
        }
    }
}
