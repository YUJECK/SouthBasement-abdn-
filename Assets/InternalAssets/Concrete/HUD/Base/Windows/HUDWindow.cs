using DG.Tweening;
using NTC.GlobalStateMachine;
using UnityEngine;

namespace SouthBasement.HUD.Base
{
    public abstract class HUDWindow : StateMachineUser, IWindow
    {
        public bool CurrentlyOpened { get; protected set; } = false;

        public virtual Vector2 GetClosedPosition() => Vector2.zero;
        public virtual float GetMoveSpeed() => 0.1f;
        public virtual GameObject Window => gameObject;

        protected Vector2 StartPosition;

        protected override void OnAwake() 
            => StartPosition = Window.transform.position;

        public void SetOpened(bool opened)
        {
            if(opened) Open();
            else Close();
        }
        public virtual void Open()
        {
            if(CurrentlyOpened)
                return;

            Window.SetActive(true);
            Window.transform.DOMove(StartPosition, GetMoveSpeed());
            CurrentlyOpened = true;
        }
        public virtual void Close()
        {
            if(!CurrentlyOpened)
                return;

            Window.transform
                .DOMove(StartPosition + GetClosedPosition(), GetMoveSpeed())
                .OnComplete(() => Window.SetActive(false));
            
            CurrentlyOpened = false;
        }

        public virtual void UpdateWindow() { }

        protected abstract override void OnFight();
        protected abstract override void OnIdle();
        protected abstract override void OnNPC();
        protected abstract override void OnDied();
    }
}