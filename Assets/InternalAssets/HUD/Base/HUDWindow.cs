using DG.Tweening;
using NTC.GlobalStateMachine;
using UnityEngine;

namespace SouthBasement.HUD.Base
{
    public abstract class HUDWindow : StateMachineUser
    {
        public bool CurrentlyOpened { get; protected set; } = true;

        public virtual Vector2 GetOpenedPosition() => transform.position; 
        public virtual float GetMoveSpeed() => 0.27f; 
        public virtual Vector2 GetClosedPosition() => transform.position;
        
        public virtual void Open()
        {
            if(CurrentlyOpened)
                return;

            transform.DOMove(GetOpenedPosition(), GetMoveSpeed());
            CurrentlyOpened = true;
        }

        public virtual void Close()
        {
            if(!CurrentlyOpened)
                return;

            transform.DOMove(GetClosedPosition(), GetMoveSpeed());
            CurrentlyOpened = false;
        }

        public virtual void UpdateWindow() { }

        protected abstract override void OnFight();
        protected abstract override void OnIdle();
        protected abstract override void OnNPC();
        protected abstract override void OnDied();
    }
}