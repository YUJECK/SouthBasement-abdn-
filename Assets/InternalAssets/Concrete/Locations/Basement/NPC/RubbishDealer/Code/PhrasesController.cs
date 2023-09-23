using SouthBasement.Extensions;
using TMPro;
using UnityEngine;
using SouthBasement.InventorySystem;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.Localization;
using Zenject;

namespace SouthBasement.NPC
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class PhrasesController : MonoBehaviour
    {
        private const string DealerTable = "RubbishDealerPhrases";

        [SerializeField] private LocalizedString withRubbishPhrase;
        [SerializeField] private LocalizedString withoutRubbishPhrase;

        private TMP_Text _text; 
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory)
            => _inventory = inventory;

        private void Awake()
            => _text = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            string phrase;

            if (_inventory.ItemsContainer.GetAllInContainer<RubbishItem>().Length > 0)
            {
                phrase = withRubbishPhrase.GetLocalized();
            }
            else
            {
                phrase = withoutRubbishPhrase.GetLocalized();
            }

            _text.text = phrase;
            _text.Show(phrase, 0.05f);
        }
    }
}
