using NTC.ContextStateMachine;
using SouthBasement.AI;
using UnityEngine;

namespace TheRat
{
    [RequireComponent(typeof(SpiderMovement))]
    public class SpiderAI : Enemy
    {
        [field: SerializeField] public Transform[] MovePoints { get; private set; }
        [field: SerializeField] public SpiderMovement SpiderMovement { get; private set; }
        [field: SerializeField] public SpiderAttacker SpiderAttacker { get; private set; }
        [field: SerializeField] public TargetSelector TargetSelector { get; private set; }
        
        [field: SerializeField] public bool CurrentlyHiding { get; set; }
        [field: SerializeField] public bool CurrentlyAttacking { get; set; }

        public SpiderAnimator SpiderAnimator { get; private set; }
        private readonly StateMachine<SpiderAI> _spiderStateMachine = new();
        private Animator _animator;

        private void Awake()
        {
            SpiderMovement = GetComponent<SpiderMovement>();
            TargetSelector = GetComponentInChildren<TargetSelector>();
            SpiderAnimator = new SpiderAnimator(GetComponentInChildren<Animator>());
            
            InitStates();
            Enable();
        }

        private void InitStates()
        {
            _spiderStateMachine.AddStates(new SpiderMoveState(this), new SpiderAttackState(this));
            
            _spiderStateMachine.AddAnyTransition<SpiderMoveState>(() => Enabled && TargetSelector.Target == null && !CurrentlyAttacking);
            _spiderStateMachine.AddAnyTransition<SpiderAttackState>(() => TargetSelector.Target != null && !CurrentlyHiding);
            
            _spiderStateMachine.TransitionsEnabled = true;
        }

        private void Update()
        {
            _spiderStateMachine.Run();
        }
    }
}
