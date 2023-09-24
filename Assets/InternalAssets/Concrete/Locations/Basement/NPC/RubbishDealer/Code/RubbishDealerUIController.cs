using SouthBasement.HUD.Base;
using UnityEngine;

namespace SouthBasement
{
    public sealed class RubbishDealerUIController : HUDWindow
    {
        [SerializeField] private Transform uiMaster;

        protected override GameObject Window => uiMaster.gameObject;

        protected override Vector2 GetClosedPosition()
            => new(0, -200);

        protected override void OnNPC() { }
        protected override void OnDied()
            => uiMaster.gameObject.SetActive(false);
        protected override void OnFight()
            => uiMaster.gameObject.SetActive(false);
        protected override void OnIdle()
            => uiMaster.gameObject.SetActive(false);
    }
}