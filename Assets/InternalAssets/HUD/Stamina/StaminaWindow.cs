using SouthBasement.HUD.Base;

namespace SouthBasement.HUD
{
    public sealed class StaminaWindow : HUDWindow
    {
        public override void Open() => gameObject.SetActive(true);
        public override void Close() => gameObject.SetActive(false);

        protected override void OnNPC() => Close();
        protected override void OnDied() => Close();
        protected override void OnFight() => Open();
        protected override void OnIdle() => Open();
    }
}