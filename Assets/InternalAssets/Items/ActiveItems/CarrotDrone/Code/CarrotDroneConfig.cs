using SouthBasement.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace SouthBasement
{
    [System.Serializable]
    public struct CarrotDroneConfig
    {
        [SerializeField] public int damage;
        [SerializeField] public float moveSpeed;
        [SerializeField] public AttackTags[] attackTags;
    }
}