using SouthBasement.Characters.Components;
using SouthBasement.InventorySystem;
using SouthBasement.Characters;
using UnityEngine;
using System;
using Zenject;

namespace SouthBasement.BaseScripts.Tests
{
    [CreateAssetMenu]
    public sealed class BoneWeaponsAmulet : PassiveItem
    {
        [SerializeField] private int extraDamage = 3;
        private Character _character;

        [Inject]
        private void Construct(Character character) 
            => _character = character;

        public override void OnPutOn()
            => _character.Components.Get<ICharacterAttacker>().OnAttacked += ExtraDamage;

        public override void OnPutOut()
            =>  _character.Components.Get<ICharacterAttacker>().OnAttacked -= ExtraDamage;
        
        private void ExtraDamage(IDamagable[] hitted)
        {
            if (_character.Components.Get<ICharacterAttacker>().Weapon == null || !_character.Components.Get<ICharacterAttacker>().Weapon.ItemTags.Contains("bone"))
                return;
            
            foreach (var hit in hitted)
            {
                extraDamage = 3;
                hit.Damage(extraDamage, _character.Components.Get<ICharacterAttacker>().Weapon.ItemTags.ToArray());
            }
        }

        public override string GetStatsDescription()
        {
            return "Increases bone weapons damage on 3 units";
        }

        public override Type GetItemType()
            => typeof(PassiveItem);
    }
}