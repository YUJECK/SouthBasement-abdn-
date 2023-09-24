using SouthBasement.HUD.Base;
using UnityEngine;
using Zenject;

namespace SouthBasement.MonologueSystem
{
    public sealed class MonologueHUD : HUDWindow
    {
        [SerializeField] private MonologuePanel panel;
        
        private MonologueManager _monologueManager;
        private MonologuePanelConfig _monologuePanelConfig;

        public override bool CurrentlyOpened { get; protected set; } = true;
        protected override GameObject Window => panel.gameObject;
        protected override Vector2 GetClosedPosition() => new(0, -400);
        protected override float GetMoveSpeed() => _monologuePanelConfig.PanelMoveSpeed;

        [Inject]
        private void Construct(MonologueManager monologueManager, MonologuePanelConfig config)
        {
            _monologueManager = monologueManager;
            _monologuePanelConfig = config;
        }

        private void Start()
            => Close();

        private void OnEnable()
            => SubscribeOnEvents();

        private void OnDisable()
            => UnsubscribeOnEvents();

        private void SubscribeOnEvents()
        {
            _monologueManager.OnStarted += OnMonologueStarted;
            _monologueManager.OnSentence += UpdateText;
            _monologueManager.OnStopped += OnMonologueStopped;
        }
        private void UnsubscribeOnEvents()
        {
            _monologueManager.OnStarted += OnMonologueStarted;
            _monologueManager.OnSentence += UpdateText;
            _monologueManager.OnStopped += OnMonologueStopped;
        }

        private void OnMonologueStopped(Monologue monologue)
            => Close();

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