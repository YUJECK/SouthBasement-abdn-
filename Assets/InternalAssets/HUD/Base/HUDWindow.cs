using DG.Tweening;
using NTC.GlobalStateMachine;
using UnityEngine;

namespace SouthBasement.HUD.Base
{
    public abstract class HUDWindow : StateMachineUser, IWindow
    {
        public bool CurrentlyOpened { get; protected set; } = true;

        public virtual Vector2 GetClosedPosition() => Vector2.zero;
        
        public virtual float GetMoveSpeed() => 0.27f;

        protected Vector2 StartPosition;

        protected override void OnAwake() => StartPosition = transform.position;

        public virtual void Open()
        {
            if(CurrentlyOpened)
                return;

            transform.DOMove(StartPosition, GetMoveSpeed());
            CurrentlyOpened = true;
        }

        public virtual void Close()
        {
            if(!CurrentlyOpened)
                return;

            transform.DOMove(StartPosition + GetClosedPosition(), GetMoveSpeed());
            CurrentlyOpened = false;
        }

        public virtual void UpdateWindow() { }

        protected abstract override void OnFight();
        protected abstract override void OnIdle();
        protected abstract override void OnNPC();
        protected abstract override void OnDied();
    }
}