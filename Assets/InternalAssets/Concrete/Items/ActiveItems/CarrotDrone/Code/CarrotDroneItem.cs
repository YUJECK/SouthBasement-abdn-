using System;
using Cysharp.Threading.Tasks;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.Characters;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using NTC.GlobalStateMachine;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.ActiveItem + nameof(CarrotDroneItem))]
    public sealed class CarrotDroneItem : ActiveItem
    {
        [SerializeField] private CarrotDroneConfig carrotDroneConfig;
        [SerializeField] private CarrotDrone carrotPrefab;
        [SerializeField] private float useRate;
        [SerializeField] private Sprite blockedSprite;

        private CarrotDrone _instance;
        private Character _character;

        private bool _blocked;

        [Inject]
        private void Construct(Character character)
        {
            _character = character;
        }
        
        public override string GetStatsDescription()
            => "Spawn a carrot drone";
        
        public override void Use()
        {
            if (!CanSpawn()) 
                return;
            
            SpawnCarrotDrone();

            Culldown();
        }
        
        private bool CanSpawn() => !_blocked;

        private void SpawnCarrotDrone()
        {
            _instance
                = Instantiate(carrotPrefab, _character.GameObject.transform.position, quaternion.identity);
            _instance.SetConfig(carrotDroneConfig);
        }
        
        private async void Culldown()
        {
            _blocked = true;
            UpdateSprite(blockedSprite);
            await UniTask.Delay(TimeSpan.FromSeconds(useRate));
            UpdateSprite(ItemSprite);
            _blocked = false;
        }
    }
}