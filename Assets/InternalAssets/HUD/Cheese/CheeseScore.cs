using Cysharp.Threading.Tasks;
using SouthBasement.Economy;
using SouthBasement.HUD.Base;
using TMPro;
using UnityEngine;
using Zenject;

namespace SouthBasement.HUD
{
    [AddComponentMenu("HUD/CheeseScore")]
    public sealed class CheeseScore : HUDWindow
    {
        private CheeseService _cheeseService;
        private TMP_Text _cheeseScore;

        public override Vector2 GetOpenedPosition() => new(transform.position.x + 300, transform.position.y);
        public override Vector2 GetClosedPosition() => new(transform.position.x - 300, transform.position.y);

        [Inject]
        private void Construct(CheeseService cheeseService)
        {
            _cheeseService = cheeseService;
            _cheeseService.OnCheeseAmountChanged += UpdateCheeseScore;
        }

        protected override void OnAwake()
        {
            _cheeseScore = GetComponentInChildren<TMP_Text>();
            
            Open();
            UpdateCheeseScore(_cheeseService.CheeseAmount);
        }

        private async void UpdateCheeseScore(int cheeseAmount)
        {
            _cheeseScore.text = $"{cheeseAmount}";

            if (!CurrentlyOpened)
            {
                Open();
                await UniTask.Delay(800);
                Close();
            }
        }

        protected override void OnNPC() => Open();
        protected override void OnDied() => Close();
        protected override void OnFight() => Close();
        protected override void OnIdle() => Open();
    }
}