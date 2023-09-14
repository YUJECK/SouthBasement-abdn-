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

        private int _currentCheese;

        public override Vector2 GetClosedPosition() => new(-300, 0f);

        [Inject]
        private void Construct(CheeseService cheeseService)
        {
            _cheeseService = cheeseService;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            _cheeseScore = GetComponentInChildren<TMP_Text>();
            _cheeseService.OnCheeseAmountChanged += UpdateCheeseScore;
            
            Open();
            UpdateCheeseScore(_cheeseService.CheeseAmount);
        }

        protected override void OnDestroyOverridable()
        {
            _cheeseService.OnCheeseAmountChanged -= UpdateCheeseScore;
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

            _currentCheese = cheeseAmount;
        }

        protected override void OnNPC() => Open();
        protected override void OnDied() => Close();
        protected override void OnFight() => Close();
        protected override void OnIdle() => Open();
    }
}