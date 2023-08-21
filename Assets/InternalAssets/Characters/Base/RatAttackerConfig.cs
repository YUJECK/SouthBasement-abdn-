using System;
using SouthBasement.Helpers.Rotator;
using UnityEngine;

namespace SouthBasement.Characters
{
    [Serializable]
    public sealed class RatAttackerConfig : MonoBehaviour
    {
        [field: SerializeField] public ObjectRotator AttackPoint { get; private set; }
        [field: SerializeField] public GameObject HitEffectPrefab { get; private set; }
    }
}