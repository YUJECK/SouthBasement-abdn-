using SouthBasement.HUD.Base;
using UnityEngine;
using Zenject;

namespace SouthBasement.MonologueSystem
{
    public sealed class MonologueHUD : HUDWindow
    {
        [SerializeField] private MonologuePanel panel;
        private MonologueManager _monologueManager;

        public override GameObject Window => panel.gameObject;
        public override bool CurrentlyOpened { get; protected set; } = true;
        public override Vector2 GetClosedPosition() => new(0, -400);

        [Inject]
        private void Construct(MonologueManager monologueManager)
            => _monologueManager = monologueManager;

        private void Start()
        {
            _monologueManager.OnStarted += OnMonologueStarted;
            _monologueManager.OnSentence += UpdateText;
            _monologueManager.OnStopped += OnMonologueStopped ;
            
            Close();
        }

        private void OnMonologueStopped(Monologue obj)
        {
            Close();
        }

        protected override void OnDestroyOverridable()
        {
            _monologueManager.OnStarted -= OnMonologueStarted;
            _monologueManager.OnSentence -= UpdateText;
        }

        private void OnMonologueStarted(Monologue monologue)
            => Open();

        private void UpdateText(string text)
            => panel.UpdateText(text);

        protected override void OnNPC() { }
        protected override void OnDied() => Close();
        protected override void OnFight() { }
        protected override void OnIdle() { }
    }
}