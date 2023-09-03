using System;
using SouthBasement.Characters;
using SouthBasement.Helpers;
using SouthBasement.InternalAssets.InventorySystem.ItemBase;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Weapon + "CaptainRedbellysSaber")]
    public sealed class CaptainRedbellysSaber : WeaponItem
    {
        public int bubbleSpawnChance;
        public int bubbleDamage = 7;
        public FishBubble FishBubblePrefab;

        private Character _character;
        
        [Inject]
        private void Construct(Character character)
        {
            _character = character;
        }
        
        public override Type GetItemType()
            => typeof(WeaponItem);

        public override void OnAttack(AttackResult damaged)
        {
            if (ChanceSystem.FitsInChance(ChanceSystem.GetRandomChance(), bubbleSpawnChance))
            {
                var bubble = Instantiate(FishBubblePrefab, GetPosition(), Quaternion.identity);
                bubble.SetDamage(bubbleDamage);
            }
        }

        private Vector3 GetPosition()
        {
            return new Vector3(_character.GameObject.transform.position.x + UnityEngine.Random.Range(-0.7f, 0.7f),
            _character.GameObject.transform.position.y + UnityEngine.Random.Range(-0.7f, 0.7f),
                0f);
        }
    }
}