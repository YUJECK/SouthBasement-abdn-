using SouthBasement.InternalAssets.Upper.Infrastructure.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using SouthBasement.InventorySystem;
using SouthBasement.InventorySystem.ItemBase;
using Zenject;

namespace SouthBasement.NPC
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class PhrasesController : MonoBehaviour
    {
        public const string TableReference = "RubbishDealerPhrases";
        
        public const string WithoutRubbishPhrase = "rubbish_has_not";
        public const string WithRubbishPhrase = "rubbish_has";

        private TMP_Text _text;
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            string phrase = "";

            if (_inventory.ItemsContainer.GetAllInContainer<RubbishItem>().Length > 0)
            {
                var localize = new LocalizedString(TableReference, WithRubbishPhrase);
                phrase = localize.GetLocalizedString();
            }
            else
            {
                var localize = new LocalizedString(TableReference, WithoutRubbishPhrase);
                phrase = localize.GetLocalizedString();
            }

            _text.text = phrase;
            _text.Show(phrase, 0.05f);
        }
    }
}
