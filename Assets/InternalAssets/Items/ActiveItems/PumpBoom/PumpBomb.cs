using System;
using Cysharp.Threading.Tasks;
using SouthBasement.Characters;
using SouthBasement.Characters.Base;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.Weapons;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.ActiveItem + "PumpBomb")]
    public class PumpBomb : ActiveItem
    {
        public CombatStats stats;
        public PumpBoom prefab;
        public Sprite defaultSprite;
        public Sprite disabledSprite;
        
        private CharacterGameObject _characterGameObject;

        private bool _blocked = false;

        [Inject]
        private void Construct(Character character)
        {
            _characterGameObject = character.GameObject;

            _blocked = false;
        }
        
        public override string GetStatsDescription()
        {
            return $"Spawn a bomb than deal {stats.Damage}";
        }

        public override Type GetItemType()
        {
            return typeof(ActiveItem);
        }

        public override void Use()
        {
            if(_blocked)
                return;
            
            var pump = Instantiate(prefab, _characterGameObject.transform.position, Quaternion.identity);
            pump.Set(stats);
            
            Culldown();
        }

        private async void Culldown()
        {
            _blocked = true;
            UpdateSprite(disabledSprite);
                
            await UniTask.Delay(TimeSpan.FromSeconds(3f));
            
            UpdateSprite(defaultSprite);
            _blocked = false;
        }
    }
}
