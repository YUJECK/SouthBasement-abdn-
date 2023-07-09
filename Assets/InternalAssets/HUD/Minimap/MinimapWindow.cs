using SouthBasement.HUD.Base;
using UnityEngine;

namespace SouthBasement.HUD.Minimap
{
    public sealed class MinimapWindow : HUDWindow
    {
        public override Vector2 GetClosedPosition() => new Vector2(200, 0);
        
        protected override void OnDied() => Close();
        protected override void OnNPC() => Close();
        protected override void OnIdle() => Open();
        protected override void OnFight() => Close();
    }
}