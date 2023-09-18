using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.Characters;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using System;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.ActiveItem + nameof(CarrotDrone))]
    public sealed class CarrotDrone : ActiveItem
    {
        [SerializeField] private CarrotDroneConfig carrotDroneConfig;
        [SerializeField] private CarrotDroneObject carrotPrefab;

        private CarrotDroneObject _instance;
        private Character _character;

        [Inject]
        private void Construct(Character character)
        {
            _character = character;
        }
        
        public override string GetStatsDescription()
            => "Spawn a carrot";

        public override Type GetItemType()
            => typeof(ActiveItem);

        public override void Use()
        {
            _instance 
                = Instantiate(carrotPrefab, _character.GameObject.transform.position, quaternion.identity);
            _instance.SetConfig(carrotDroneConfig);
        }
    }
}