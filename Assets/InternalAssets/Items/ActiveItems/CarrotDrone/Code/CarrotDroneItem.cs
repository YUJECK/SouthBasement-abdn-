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

        private CarrotDrone _instance;
        private Character _character;

        [Inject]
        private void Construct(Character character)
        {
            _character = character;
        }
        
        public override string GetStatsDescription()
            => "Spawn a carrot";
        
        public override void Use()
        {
            if (GlobalStateMachine.LastGameState.State() == GameStates.Fight)
            {
                _instance 
                    = Instantiate(carrotPrefab, _character.GameObject.transform.position, quaternion.identity);
                _instance.SetConfig(carrotDroneConfig);
            }
        }
    }
}