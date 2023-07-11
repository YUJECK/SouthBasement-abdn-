using System;
using SouthBasement.Characters;
using SouthBasement.Characters.Components;
using SouthBasement.InventorySystem;
using Zenject;

namespace SouthBasement.BaseScripts.Tests
{
    public sealed class EffectsWeaponsAmulet : PassiveItem
    {
        private Character _character;
        
        [Inject]
        private void Construct(Character character) 
            => _character = character;

        public override Type GetItemType()
            => typeof(PassiveItem);

        public override void OnPutOn()
        {
            _character.Components.Get<ICharacterAttacker>().OnAttacked += ExtraEffect;
        }

        private void ExtraEffect(IDamagable[] obj)
        {
            if (!_character.Components.Get<ICharacterAttacker>().Weapon.ItemTags.Contains("effect")) 
                return;

            foreach (var damagable in obj)
                damagable.EffectsHandler.Add(new ExtraDamageEffect(1, 0.75f, 3, damagable));
        }
    }
}