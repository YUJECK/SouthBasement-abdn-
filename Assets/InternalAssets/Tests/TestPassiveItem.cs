using SouthBasement.Characters.Components;
using SouthBasement.InventorySystem;
using SouthBasement.Characters;
using UnityEngine;
using System;

namespace SouthBasement.BaseScripts.Tests
{
    [CreateAssetMenu]
    public sealed class TestPassiveItem : PassiveItem
    {
        [SerializeField] private int damage = 3;
        private Character _character;

        public override void Init()
        {
            _character = FindObjectOfType<Character>();
        }

        public override void OnPutOn()
            => _character.Components.Get<IAttacker>().OnAttacked += ExtraDamage;

        public override void OnPutOut()
            =>  _character.Components.Get<IAttacker>().OnAttacked -= ExtraDamage;
        
        private void ExtraDamage(IDamagable[] hitted)
        {
            if (_character.Components.Get<IAttacker>().Weapon != null && !_character.Components.Get<IAttacker>().Weapon.ItemTags.Contains("bone"))
                return;
            
            foreach (var hit in hitted)
            {
                damage = 3;
                hit.Damage(damage, _character.Components.Get<IAttacker>().Weapon.ItemTags.ToArray());
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