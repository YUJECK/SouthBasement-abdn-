using SouthBasement.HUD.Base;
using UnityEngine;

namespace SouthBasement.HUD
{
    public sealed class InventoryPanelWindow : HUDWindow
    {
        public override Vector2 GetClosedPosition()
            => StartPosition + new Vector2(400, 0);

        protected override void OnNPC() { }
        protected override void OnDied() { }
        protected override void OnFight() { }
        protected override void OnIdle() { }
    }
}