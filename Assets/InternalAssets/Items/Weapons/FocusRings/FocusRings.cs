using System;
using SouthBasement.Characters;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Weapon + "FocusRings")]
    public sealed class FocusRings : WeaponItem, IAttackOverridable
    {
        public FocusFireController prefab;
        private Character _character;

        private FocusFireController _prefabInstance;
        
        [Inject]
        private void Construct(Character character)
        {
             _character = character;
        }
        
        public override void OnEquip()
        {
            _prefabInstance = Instantiate(prefab, _character.GameObject.transform);
        }

        public override void OnRemoved()
        {
            Destroy(_prefabInstance);
        }

        public override Type GetItemType()
            => typeof(WeaponItem);

        public IDamagable[] Attack()
        {
            _prefabInstance.Create(); 
                

            return Array.Empty<IDamagable>();
        }
    }
}
