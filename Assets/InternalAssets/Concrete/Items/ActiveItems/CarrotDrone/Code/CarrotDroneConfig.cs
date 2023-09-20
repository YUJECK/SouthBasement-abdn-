using SouthBasement.Items;
using UnityEngine;

namespace SouthBasement
{
    [System.Serializable]
    public struct CarrotDroneConfig
    {
        [SerializeField] public int damage;
        [SerializeField] public float moveSpeed;
        [SerializeField] public float timeToExplode;
        [SerializeField] public AttackTags[] attackTags;
    }
}