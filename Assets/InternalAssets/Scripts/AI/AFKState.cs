﻿using NTC.ContextStateMachine;

namespace TheRat.AI
{
    public sealed class AFKState : State<DefaultRatStateMachine>
    {
        public AFKState(DefaultRatStateMachine stateInitializer) : base(stateInitializer) { }

        public override void OnEnter() => Initializer.EnemyAnimator.PlayAFK();
    }
}