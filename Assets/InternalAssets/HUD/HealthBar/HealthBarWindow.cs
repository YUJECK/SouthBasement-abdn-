using SouthBasement.HUD.Base;
using UnityEngine;

namespace SouthBasement.HUD
{
    public sealed class HealthBarWindow : HUDWindow
    {
        public override Vector2 GetOpenedPosition() => new(transform.position.x, transform.position.y - 200);
        public override Vector2 GetClosedPosition()=> new(transform.position.x, transform.position.y + 200);

        protected override void OnNPC() => Open();
        protected override void OnDied() => Close();
        protected override void OnFight() => Open();
        protected override void OnIdle() => Open();
    }
}