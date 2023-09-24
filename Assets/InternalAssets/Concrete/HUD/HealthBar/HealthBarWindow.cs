using SouthBasement.HUD.Base;
using UnityEngine;

namespace SouthBasement.HUD
{
    public sealed class HealthBarWindow : HUDWindow
    {
        protected override Vector2 GetClosedPosition()=> new(0, 200);

        protected override void OnNPC() => Open();
        protected override void OnDied() => Close();
        protected override void OnFight() => Open();
        protected override void OnIdle() => Open();
    }
}