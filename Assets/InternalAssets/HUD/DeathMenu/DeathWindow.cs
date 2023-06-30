using SouthBasement.HUD.Base;
using UnityEngine;

namespace SouthBasement.HUD.DeathMenu
{
    public sealed class DeathWindow : HUDWindow
    {
        public override float GetMoveSpeed() => 0.7f;
        public override Vector2 GetOpenedPosition() => new(transform.position.x + 400f, transform.position.y);
        public override Vector2 GetClosedPosition() => new(transform.position.x - 400f, transform.position.y);

        protected override void OnNPC() => Close();
        protected override void OnDied() => Open();
        protected override void OnFight() => Close();
        protected override void OnIdle() => Close();
    }
}