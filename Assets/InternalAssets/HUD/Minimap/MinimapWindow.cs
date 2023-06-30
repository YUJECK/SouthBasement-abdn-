using DG.Tweening;
using SouthBasement.HUD.Base;
using UnityEngine;

namespace SouthBasement.HUD.Minimap
{
    public sealed class MinimapWindow : HUDWindow
    {
        public override Vector2 GetOpenedPosition() => new(transform.position.x - 400, transform.position.y);

        public override Vector2 GetClosedPosition()=> new(transform.position.x + 400, transform.position.y);
        
        protected override void OnDied() => Close();
        protected override void OnNPC() => Close();
        protected override void OnIdle() => Open();
        protected override void OnFight() => Close();
    }
}