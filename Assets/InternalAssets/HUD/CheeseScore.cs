using SouthBasement.Economy;
using TMPro;
using UnityEngine;
using Zenject;

namespace SouthBasement.HUD
{
    [AddComponentMenu("HUD/CheeseScore")]
    [RequireComponent(typeof(TMP_Text))]
    public sealed class CheeseScore : MonoBehaviour
    {
        private CheeseService _cheeseService;
        private TMP_Text _cheeseScore;
        
        [Inject]
        private void Construct(CheeseService cheeseService)
        {
            _cheeseService = cheeseService;
            _cheeseService.OnCheeseAmountChanged += UpdateCheeseScore;
        }

        private void Awake()
        {
            _cheeseScore = GetComponent<TMP_Text>();
            UpdateCheeseScore(_cheeseService.CheeseAmount);
        }

        private void UpdateCheeseScore(int cheeseAmount)
        {
            _cheeseScore.text = $"{cheeseAmount}";
        }
    }
}