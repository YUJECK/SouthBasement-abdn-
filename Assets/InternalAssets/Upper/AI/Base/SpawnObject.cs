using System;
using NaughtyAttributes;
using UnityEngine;

namespace SouthBasement
{
    [Serializable]
    public sealed class SpawnObject<TObject>
        where TObject : class
    {
        [Min(5f), MaxValue(100f)] public float SpawnChance;
        [Min(5f), MaxValue(100f)] public TObject Prefab;
    }
}