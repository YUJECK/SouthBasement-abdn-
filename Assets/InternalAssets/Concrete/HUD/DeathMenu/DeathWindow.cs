using SouthBasement.HUD.Base;
using UnityEngine;

namespace SouthBasement.HUD.DeathMenu
{
    public sealed class DeathWindow : HUDWindow
    {
        [SerializeField] private GameObject panel;
        
        public override float GetMoveSpeed() => 0.7f;
        public override GameObject Window => panel;
        public override Vector2 GetClosedPosition() => new(-400f, 0f);

        private void Start()
            => panel.SetActive(false);

        protected override void OnNPC() => Close();
        protected override void OnDied() => Open();
        protected override void OnFight() => Close();
        protected override void OnIdle() => Close();
    }
}