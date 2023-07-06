using SouthBasement.Characters.Components;
using SouthBasement.InventorySystem;
using SouthBasement.Characters;
using UnityEngine;
using Zenject;
using System;

namespace SouthBasement.BaseScripts.Tests
{
    [CreateAssetMenu]
    public sealed class TestPassiveItem : PassiveItem
    {
        private Character _character;

        [Inject]
        private void Construct(Character character) 
            => _character = character;

        public override void OnPutOn()
        {
            _character = FindObjectOfType<Character>();
            _character.Components.Get<IAttacker>().OnAttacked += ExtraDamage;
        }

        public override void OnPutOut()
            =>  FindObjectOfType<Character>().Components.Get<IAttacker>().OnAttacked -= ExtraDamage;
        
        private void ExtraDamage(IDamagable[] hitted)
        {
            if (!_character.Components.Get<IAttacker>().Weapon.ItemTags.Contains("bone")) return;
            
            foreach (var hit in hitted)
                hit.Damage(3, _character.Components.Get<IAttacker>().Weapon.ItemTags.ToArray());
        }

        public override Type GetItemType()
            => typeof(PassiveItem);
    }
}